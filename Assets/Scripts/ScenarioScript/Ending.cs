using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // Add this

public class Ending : MonoBehaviour
{
    private bool isEnding = false;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (isEnding) return;
        if (collision.gameObject.tag != "Player") return;

        isEnding = true;

        collision.gameObject.SetActive(false);

        ScenarioSpin scenarioSpin = FindObjectOfType<ScenarioSpin>();
        if (scenarioSpin != null)
        {
            scenarioSpin.DisableRotation();
        }
        string currentLevel = SceneManager.GetActiveScene().name;
        PlayerPrefs.SetString("CurrentLevel", currentLevel);
        PlayerPrefs.Save();

        SceneManager.LoadScene("Completed");
    }
}
