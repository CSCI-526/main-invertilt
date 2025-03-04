using UnityEngine;
using UnityEngine.SceneManagement;

public class Completed : MonoBehaviour
{
    public void LoadLevelsScene()
    {
        SceneManager.LoadScene("Levels");
    }
}
