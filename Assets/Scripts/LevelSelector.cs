using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class LevelSelector : MonoBehaviour
{
    public int level;
    public TextMeshProUGUI levelText;

    void Start()
    {
        levelText.text = level.ToString();
    }

    public void OpenScene()
    {
        if (level == 1)
        {
            GameAnalytics.Instance.LevelStarted(1);
            SceneManager.LoadScene("Tut-1");
        }
        else if (level == 2)
        {
            GameAnalytics.Instance.LevelStarted(2);
            SceneManager.LoadScene("Level-2");
        }
        else if (level == 3)
        {
            GameAnalytics.Instance.LevelStarted(3);
            SceneManager.LoadScene("Level-3");
        }
        else if (level == 4)
        {
            GameAnalytics.Instance.LevelStarted(4);
            SceneManager.LoadScene("Level-4");
        }
        else if (level == 5)
        {
            GameAnalytics.Instance.LevelStarted(5);
            SceneManager.LoadScene("Level-5");
        }
        else if (level == 6)
        {
            GameAnalytics.Instance.LevelStarted(6);
            SceneManager.LoadScene("Level-6");
        }
    }

    void Update()
    {
    }
}
