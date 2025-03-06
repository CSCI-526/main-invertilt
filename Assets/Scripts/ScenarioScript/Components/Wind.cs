using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wind : MonoBehaviour
{
    [SerializeField] private float windForce = 8.0f;
    public static float WindForce { get; private set; }

    private Vector2 windDirection;

    void Awake()
    {
        WindForce = windForce;
    }

    // Start is called before the first frame update
    void Start()
    {
        WindForce = windForce;
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
        windDirection = transform.up;
        collision.attachedRigidbody.AddForce(windDirection * windForce);
    }
}
