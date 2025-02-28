using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ending : MonoBehaviour
{
    private bool isEnding = false;
    public GameObject endingText;

    // Start is called before the first frame update
    void Start()
    {
        endingText.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D collision)
    {

        // Debug.Log("Ending OnTriggerEnter2D");

        if (isEnding)
        {
            return;
        }

        if (collision.gameObject.tag != "Player")
        {
            return;
        }

        isEnding = true;

        collision.gameObject.SetActive(false);

        ScenarioSpin scenarioSpin = FindObjectOfType<ScenarioSpin>();
        if (scenarioSpin != null)
        {
            scenarioSpin.DisableRotation();
        }
        endingText.SetActive(true);
    }

}
