using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncyBall : MonoBehaviour
{
    private Rigidbody2D rb;
    private bool gravityReversed = false; // Tracks the current gravity state
    private bool isOnSandpaper = false; // Tracks if the ball is on sandpaper
    public float shrinkRate = 0.1f; // decrease rate
    public float minScale = 0.2f; // minimum scale
    private Vector3 originalScale; // original scale of the ball

    // Reference to the ScenarioSpin script
    public ScenarioSpin scenarioSpin;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Get the Rigidbody2D component
        Physics2D.gravity = new Vector2(0, -9.81f); // Set default gravity

        // Find the ScenarioSpin script if not linked
        if (scenarioSpin == null)
        {
            scenarioSpin = FindObjectOfType<ScenarioSpin>();
        }

        originalScale = transform.localScale; // Save the original scale of the ball
    }

    void Update()
    {
        // Only allow gravity flip if there are flips remaining
        if (scenarioSpin != null && scenarioSpin.remainingGravityFlips > 0)
        {
            // Press G key to toggle gravity direction
            if (Input.GetKeyDown(KeyCode.G))
            {
                gravityReversed = !gravityReversed;

                // Change global gravity
                Physics2D.gravity = gravityReversed ? new Vector2(0, 9.81f) : new Vector2(0, -9.81f);

                // Reverse the ball's velocity instantly to reflect gravity change
                rb.velocity = new Vector2(rb.velocity.x, -rb.velocity.y);
            }
        }

        // Logic to decrease the size of the ball
        if (isOnSandpaper && IsMoving())
        {
            ShrinkPlayer();
        } else
        {
            // reset the scale of the ball
            transform.localScale = originalScale;
        }
    }

    // --------------------------------------------------------------
    // Decrease the size of the ball
    private void ShrinkPlayer()
    {
        if (transform.localScale.x > minScale && transform.localScale.y > minScale)
        {
            transform.localScale -= new Vector3(shrinkRate, shrinkRate, 0) * Time.deltaTime;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Sandpaper"))
        {
            isOnSandpaper = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Sandpaper"))
        {
            isOnSandpaper = false;
        }
    }

    // --------------------------------------------------------------
    // Check if the ball is moving
    private bool IsMoving()
    {
        return Mathf.Abs(rb.velocity.x) > 0.1f || Mathf.Abs(rb.velocity.y) > 0.1f;
    }
}

