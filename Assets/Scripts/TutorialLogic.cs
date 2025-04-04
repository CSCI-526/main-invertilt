using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialLogic : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Entered trigger with: " + other.gameObject.name);  // 👈 Log what object was touched

        if (other.gameObject.name == "Bouncyball")
        {
            Debug.Log("Ending detected. Resetting scene...");
            Invoke("ResetScene", 0f);
        }
    }

    void ResetScene()
    {
        Debug.Log("Scene is resetting now.");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
