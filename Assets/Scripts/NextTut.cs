using UnityEngine;
using UnityEngine.SceneManagement;

public class NextTut : MonoBehaviour
{
    public void GoToNextScene()
    {
        if (SceneManager.GetActiveScene().name == "Tut-1")
        {
            SceneManager.LoadScene("Play-1");
        }
        else if (SceneManager.GetActiveScene().name == "Play-1")
        {
            SceneManager.LoadScene("Tut-2");
        }
        else if (SceneManager.GetActiveScene().name == "Tut-2")
        {
            SceneManager.LoadScene("Play-2");
        }
        else if (SceneManager.GetActiveScene().name == "Play-2")
        {
            SceneManager.LoadScene("Tut-3");
        }
        else if (SceneManager.GetActiveScene().name == "Tut-3")
        {
            SceneManager.LoadScene("Play-3");
        }
        else if (SceneManager.GetActiveScene().name == "Play-3")
        {
            SceneManager.LoadScene("SampleScene");
        }
        else
        {
            Debug.LogWarning("Current scene is not Tut-1. Not transitioning.");
        }
    }
}
