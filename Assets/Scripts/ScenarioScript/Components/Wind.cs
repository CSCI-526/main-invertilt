using System;
using System.Collections;
using System.Collections.Generic;
using System.IO.Compression;
using UnityEngine;

public class Wind : MonoBehaviour
{

    public new ParticleSystem particleSystem;
    private Vector3 windSize;
    private Vector3 windPosition;

    public Vector2 windDirection = new Vector2(1.0f, 0.0f);
    public float windForce = 10.0f;
    // Start is called before the first frame update
    void Start()
    {

        windSize = transform.childCount > 0 ? transform.GetChild(0).localScale : Vector3.one;
        windPosition = transform.position;

        if (particleSystem == null)
        {
            return;
        }
        initParticleSystem();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void LateUpdate()
    {
        destroyParticle();
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

    void initParticleSystem()
    {
        // shape size
        var shape = particleSystem.shape;
        shape.scale = new Vector3(windSize.x, 1.0f, 1.0f);

        // start speed
        var main = particleSystem.main;
        // main.startSpeed = windForce / 3.0f;
        main.startSpeed = (ParticleSystem.MinMaxCurve)(Math.Log(windForce) * 2.0f);
    }

    // destroy the particle out of the wind area
    void destroyParticle()
    {
        if (particleSystem == null) return;

        int numParticles = particleSystem.particleCount;
        if (numParticles == 0) return;

        ParticleSystem.Particle[] particles = new ParticleSystem.Particle[numParticles];

        particleSystem.GetParticles(particles);

        for (int i = 0; i < numParticles; i++)
        {
            // The position of the particle is local to the wind object
            // destroy the particle out of the wind area
            if (particles[i].position.z > windSize.y)
            {
                particles[i].remainingLifetime = 0.0f;
            }
        }

        particleSystem.SetParticles(particles, numParticles);

    }


}
