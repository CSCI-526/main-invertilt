using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wind : MonoBehaviour
{

    public Vector2 windDirection = new Vector2(1.0f, 0.0f);
    public float windForce = 10.0f;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag != "Player")
        {
            return;
        }

        if (collision.attachedRigidbody == null)
        {
            return;
        }

        collision.attachedRigidbody.AddForce(windDirection * windForce);
    }
}
