using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;  // Add this

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
            SceneManager.LoadScene("Level-1");
        }
        else if (level == 2)
        {
            SceneManager.LoadScene("Level-1");
        }
        else if (level == 3)
        {
            SceneManager.LoadScene("Level-1");
        }
        else
        {
            SceneManager.LoadScene("DemoScene");
        }
    }

    void Update()
    {
    }
}
