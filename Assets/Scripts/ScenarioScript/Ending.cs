using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // Add this

public class Ending : MonoBehaviour
{
    private bool isEnding = false;
    private int level;
    private string levelName;

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
        
        levelName = SceneManager.GetActiveScene().name;
        switch(levelName)
        {
            case "Level-1":
                level = 1;
                break;
            case "Level-2":
                level = 2;
                break;
            case "Level-3":
                level = 3;
                break;
            case "Level-4":
                level = 4;
                break;
        }

        Debug.Log("Level "+ level + " completed.");
        GameAnalytics.Instance.LevelCompleted(level);
        SceneManager.LoadScene("Completed");
    }
}
