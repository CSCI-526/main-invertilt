using UnityEngine;

public class Portal2D : MonoBehaviour
{
    public Portal2D linkedPortal;
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
                Vector2 newVelocity = linkedPortal.transform.right * ballRb.velocity.magnitude;
                ballRb.velocity = newVelocity;
                StartCoroutine(DisableTeleport());
                linkedPortal.StartCoroutine(linkedPortal.DisableTeleport());
            }
        }
    }
    private System.Collections.IEnumerator DisableTeleport()
    {
        canTeleport = false;
        yield return new WaitForSeconds(teleportCooldown);
        canTeleport = true;
    }
}
