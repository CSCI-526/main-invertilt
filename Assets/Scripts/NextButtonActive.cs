using UnityEngine;
using UnityEngine.UI;

public class NextButtonActive : MonoBehaviour
{
    public GameObject nextButton;  // Drag your NextButton UI object here in Inspector

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "Bouncyball")
        {
            Debug.Log("Ball reached the goal! Showing Next button.");
            nextButton.SetActive(true);  // Show the button
        }
    }
}
