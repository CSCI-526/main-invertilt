using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindArea : MonoBehaviour
{
    public float windStrength = 50f;  
    public Vector2 windDirection = new Vector2(-1f, 0f);  
    private ParticleSystem windParticles;  // Visual effect

    void Start()
    {
        // Particle System component attached to this GameObject
        windParticles = GetComponentInChildren<ParticleSystem>();

        // Disable the particles at the start
        if (windParticles != null)
        {
            var main = windParticles.main;
            main.loop = true; 
            windParticles.Stop();
        }
    }

    // Apply wind and show particles when player enters Wind Area
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            ApplyWindEffect(collision.gameObject);
            if (windParticles != null)
            {
                windParticles.Play();  
            }
        }
    }

    // Hide wind effect and particles when player exits the area
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            RemoveWindEffect(collision.gameObject);
            if (windParticles != null)
            {
                windParticles.Stop();  
            }
        }
    }

    private void ApplyWindEffect(GameObject player)
    {
        Rigidbody2D rb = player.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.AddForce(windDirection * windStrength, ForceMode2D.Force);
        }
    }

    private void RemoveWindEffect(GameObject player)
    {
        // Remove force or reset gravity when player exits
        Rigidbody2D rb = player.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = Vector2.zero; // Stop wind velocity
        }
    }
}