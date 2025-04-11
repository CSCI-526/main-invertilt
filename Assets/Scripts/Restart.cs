using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartGame : MonoBehaviour
{
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        switch(SceneManager.GetActiveScene().name)
        {
            case "Level-1":
                GameAnalytics.Instance.LevelStarted(1);
                break;
            case "Level-2":
                GameAnalytics.Instance.LevelStarted(2);
                break;
            case "Level-3":
                GameAnalytics.Instance.LevelStarted(3);
                break;
            case "Level-4":
                GameAnalytics.Instance.LevelStarted(4);
                break;
            case "Level-5":
                GameAnalytics.Instance.LevelStarted(5);
                break;
            case "Level-6":
                GameAnalytics.Instance.LevelStarted(6);
                break;
            case "Level-7":
                GameAnalytics.Instance.LevelStarted(7);
                break;
            case "Level-8":
                GameAnalytics.Instance.LevelStarted(8);
                break;
        }
    }
}
