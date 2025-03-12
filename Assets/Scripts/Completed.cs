using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Completed : MonoBehaviour
{
    public Button next;

    private void Start()
    {
        string currentLevel = PlayerPrefs.GetString("CurrentLevel", "Level-1");
        string nextLevel = GetNextLevelName(currentLevel);

        if (!IsLevelAvailable(nextLevel))
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

        if (IsLevelAvailable(nextLevel))
        {
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
        int levelNumber;
        if (int.TryParse(currentLevel.Replace("Level-", ""), out levelNumber))
        {
            return "Level-" + (levelNumber + 1);
        }
        return "";
    }

    private bool IsLevelAvailable(string levelName)
    {
        for (int i = 0; i < SceneManager.sceneCountInBuildSettings; i++)
        {
            if (SceneUtility.GetScenePathByBuildIndex(i).Contains(levelName))
            {
                return true;
            }
        }
        return false;
    }
}
