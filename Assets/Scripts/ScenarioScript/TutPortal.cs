using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutPortal : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public TutPortal linkedPortal;
    public float teleportCooldown = 2f;

    private bool canTeleport = true;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (canTeleport && other.CompareTag("Player"))
        {
            Rigidbody2D ballRb = other.GetComponent<Rigidbody2D>();

            if (ballRb != null && linkedPortal != null)
            {
                other.transform.position = linkedPortal.transform.position;
                // Vector2 newVelocity = linkedPortal.transform.right * ballRb.velocity.magnitude;
                // ballRb.velocity = newVelocity;
                other.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
                StartCoroutine(DisableTeleport());
                linkedPortal.StartCoroutine(linkedPortal.DisableTeleport());
            }
        }
    }
    private IEnumerator DisableTeleport()
    {
        canTeleport = false;
        yield return new WaitForSeconds(teleportCooldown);
        canTeleport = true;
    }
}
