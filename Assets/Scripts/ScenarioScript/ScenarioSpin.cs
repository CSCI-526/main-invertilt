using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class ScenarioSpin : MonoBehaviour
{
    private float rotationAngle = 45f;
    public float rotationDuration = 0.8f;
    private bool isRotating = false;
    private bool canMove = true;

    public int movesLeft = 8;

    // Text Reference
    public TMP_Text movesCounterText;

    // Track gravity flip state
    private bool isGravityFlipped = false;

    private RestartGame restartScript;


    void Start()
    {
        restartScript = FindObjectOfType<RestartGame>();
        
        int currentLevel = GetCurrentLevelNumber();
        Debug.Log("Current Level: " + currentLevel + ", Moves Left: " + movesLeft);
        UpdateUI();
    }

    int GetCurrentLevelNumber()
    {
        string sceneName = SceneManager.GetActiveScene().name;
        string levelNumberStr = sceneName.Replace("Level-", "");

        int levelNumber;
        if (int.TryParse(levelNumberStr, out levelNumber))
        {
            return levelNumber;
        }

        return 1;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) && restartScript != null)
        {
            restartScript.Restart();
        }
        if (!canMove || movesLeft <= 0)
        {
            return;
        }

        // The scenario cannot move if it is rotating
        if (Input.GetKeyDown(KeyCode.A) && !isRotating && movesLeft > 0)
        {
            StartCoroutine(SmoothRotate(rotationAngle));
            movesLeft--;
            UpdateUI();
        }
        if (Input.GetKeyDown(KeyCode.D) && !isRotating && movesLeft > 0)
        {
            StartCoroutine(SmoothRotate(-rotationAngle));
            movesLeft--;
            UpdateUI();
        }

        if (Input.GetKeyDown(KeyCode.G))
        {
            if (movesLeft > 0)  // Ensure movesLeft must be positive before allowing gravity flip
            {
                Physics2D.gravity = isGravityFlipped ? new Vector2(0, -9.81f) : new Vector2(0, 9.81f);
                isGravityFlipped = !isGravityFlipped;
                movesLeft--;
                UpdateUI();
            }
        }
    }

    IEnumerator SmoothRotate(float angle)
    {
        // This code spin around the 0, 0 point.
        // FIXME: We may need to do some change for different level, if we only want to rotate one platform.
        isRotating = true;
        float elapsedTime = 0f;
        Quaternion startRotation = transform.rotation;
        Quaternion endRotation = Quaternion.Euler(0, 0, transform.eulerAngles.z + angle);

        // Animate the rotation
        while (elapsedTime < rotationDuration)
        {
            transform.rotation = Quaternion.Lerp(startRotation, endRotation, elapsedTime / rotationDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.rotation = endRotation;
        isRotating = false;

    }

    void UpdateUI()
    {
        if (movesCounterText != null)
        {
            movesCounterText.text = "Moves Left: " + movesLeft;

            if (movesLeft == 0)
            {
                movesCounterText.color = Color.red;
                movesCounterText.fontStyle = FontStyles.Bold;
            }
        }
    }

    public void DisableMoves()
    {
        canMove = false;
    }
    public void DisableRotation()
    {
        // canRotate = false;
    }


}
