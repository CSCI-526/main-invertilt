using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // For Text Mesh pro

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

    void Start()
    {
        UpdateUI();
    }

    void Update()
    {
        if (!canMove || movesLeft <= 0)
        {
            return;
        }

        // The scenario cannot move if the it is rotating
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

        if (Input.GetKeyDown(KeyCode.G) && movesLeft > 0)
        {
            Physics2D.gravity = isGravityFlipped ? new Vector2(0, -9.81f) : new Vector2(0, 9.81f);
            isGravityFlipped = !isGravityFlipped;
            movesLeft--;
            UpdateUI();
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
