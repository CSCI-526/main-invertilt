using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleBox : MonoBehaviour
{

    public GameObject infoBox;
    // Start is called before the first frame update
    void Start()
    {
        if (infoBox != null)
            infoBox.SetActive(false); 
    }

    // Update is called once per frame
    public void ToggleUIBox()
    {
        if (infoBox != null)
            infoBox.SetActive(!infoBox.activeSelf);   
    }
}

