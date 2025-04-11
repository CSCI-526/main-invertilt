using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Completed : MonoBehaviour
{
    public Button next;

    private Dictionary<string, string> sceneTransitions = new Dictionary<string, string>
    {
        { "Level-1", "TutWind" },
        { "Level-2", "Level-3" },
        { "Level-3", "TutSandpaper" },
        { "Level-4", "Level-5" },
        { "Level-5", "Level-6" },
        { "Level-6", "TutPortal" },
        { "Level-7", "Level-8" },
    };



    private void Start()
    {
        string currentLevel = PlayerPrefs.GetString("CurrentLevel", "Level-1");
        string nextLevel = GetNextLevelName(currentLevel);

        if (!IsLevelAvailable(nextLevel) && !IsTutAvailable(nextLevel))
        {
            next.gameObject.SetActive(false);
        }
    }

    public void LoadLevelsScene()
    {
        SceneManager.LoadScene("Levels");
    }

    public void LoadNextLevel()
    {
        string currentLevel = PlayerPrefs.GetString("CurrentLevel", "Level-1");
        string nextLevel = GetNextLevelName(currentLevel);



        if (currentLevel == "Level-4")
        {
            nextLevel = "Level-5";
            next.gameObject.SetActive(true);
        }
        else
        {
            if (!IsLevelAvailable(nextLevel) && !IsTutAvailable(nextLevel))
            {
                next.gameObject.SetActive(false);
            }
        }
        if (IsLevelAvailable(nextLevel))
        {
            GameAnalytics.Instance.LevelStarted(int.Parse(nextLevel.Replace("Level-", "")));
            SceneManager.LoadScene(nextLevel);
        } else if (IsTutAvailable(nextLevel)) {
            Debug.Log("Loading tutorial: " + nextLevel);
            SceneManager.LoadScene(nextLevel);
        }
        else
        {
            Debug.Log("No more levels! Returning to Level Selection.");
            SceneManager.LoadScene("Levels");
        }
    }

    private string GetNextLevelName(string currentLevel)
    {
        // int levelNumber;
        // if (int.TryParse(currentLevel.Replace("Level-", ""), out levelNumber))
        // {
        //     return "Level-" + (levelNumber + 1);
        // }
        if (sceneTransitions.ContainsKey(currentLevel))
        {
            return sceneTransitions[currentLevel];
        }
        return "";
    }

    private bool IsLevelAvailable(string levelName)
    {
        // for (int i = 0; i < SceneManager.sceneCountInBuildSettings; i++)
        // {
        //     if (SceneUtility.GetScenePathByBuildIndex(i).Contains(levelName))
        //     {
        //         return true;
        //     }
        // }
        // return false;
        // return true iff the next is beginning with "Level-"
        return levelName.StartsWith("Level-");
    }

    private bool IsTutAvailable(string levelName) {
        return levelName.StartsWith("Tut");
    }
}
