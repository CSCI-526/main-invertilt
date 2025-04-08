using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MechanicHint : MonoBehaviour
{

    public String GameObjectName;
    private Vector3 originalPosition; // Original position of the ball

    void Start()
    {
        // get Object by name
        GameObject gameObject = GameObject.Find(GameObjectName);
        if (gameObject != null)
        {
            originalPosition = gameObject.transform.position; // Save the original position of the ball
        }
        else
        {
            Debug.LogWarning("GameObject with name " + GameObjectName + " not found.");
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.name == GameObjectName)
        {
            // Reset the location of the ball
            other.transform.position = originalPosition;
            // only keep the gravity direction
            other.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        }
    }

}
