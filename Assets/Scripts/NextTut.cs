using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextTut : MonoBehaviour
{

    private Dictionary<string, string> sceneTransitions = new Dictionary<string, string>
    {
        { "Tut-1", "Play-1" },
        { "Play-1", "Tut-2" },
        { "Tut-2", "Play-2" },
        { "Play-2", "Tut-3" },
        { "Tut-3", "Play-3" },
        { "Play-3", "Level-1" },
        { "TutWind", "Level-2" },
        { "TutSandpaper", "Level-3" },
        { "TutPortal", "Level-5"}
    };

    public void GoToNextScene()
    {
        Debug.Log("Clicked!"); // Log the click event
        // Check if the current scene is in the dictionary
        if (sceneTransitions.ContainsKey(SceneManager.GetActiveScene().name))
        {
            string nextScene = sceneTransitions[SceneManager.GetActiveScene().name];
            Debug.Log("Transitioning to: " + nextScene); // Log the transition
            SceneManager.LoadScene(nextScene);
        }
        else
        {
            Debug.LogWarning("Current scene is not in the transition list. Not transitioning.");
        }

        // if (SceneManager.GetActiveScene().name == "Tut-1")
        // {
        //     SceneManager.LoadScene("Play-1");
        // }
        // else if (SceneManager.GetActiveScene().name == "Play-1")
        // {
        //     SceneManager.LoadScene("Tut-2");
        // }
        // else if (SceneManager.GetActiveScene().name == "Tut-2")
        // {
        //     SceneManager.LoadScene("Play-2");
        // }
        // else if (SceneManager.GetActiveScene().name == "Play-2")
        // {
        //     SceneManager.LoadScene("Tut-3");
        // }
        // else if (SceneManager.GetActiveScene().name == "Tut-3")
        // {
        //     SceneManager.LoadScene("Play-3");
        // }
        // else if (SceneManager.GetActiveScene().name == "Play-3")
        // {
        //     SceneManager.LoadScene("Level-1");
        // }
        // else
        // {
        //     Debug.LogWarning("Current scene is not Tut-1. Not transitioning.");
        // }
    }
}
