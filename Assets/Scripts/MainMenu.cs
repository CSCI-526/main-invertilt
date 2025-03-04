using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public void Playgame(){
        SceneManager.LoadSceneAsync("Levels");
    }
    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
