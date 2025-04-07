using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public void Playgame(){
        SceneManager.LoadSceneAsync("Levels");
    }
    public void Playgame2(){
        SceneManager.LoadSceneAsync("Tut-1");
    }
    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
