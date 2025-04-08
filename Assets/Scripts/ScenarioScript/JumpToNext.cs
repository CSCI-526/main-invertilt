using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpToNext : MonoBehaviour
{

    public float delay = 2f; // Delay in seconds
    public string nextSceneName; // Name of the next scene
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(LoadNextScene());
    }

    IEnumerator LoadNextScene()
    {
        // Wait for 2 seconds
        yield return new WaitForSeconds(delay);
        // Load the next scene
        UnityEngine.SceneManagement.SceneManager.LoadScene(nextSceneName);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
