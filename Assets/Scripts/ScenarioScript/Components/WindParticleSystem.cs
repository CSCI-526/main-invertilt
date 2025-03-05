using System;
using UnityEngine;

public class WindParticleSystem : MonoBehaviour
{

    public new ParticleSystem particleSystem;
    private Vector3 windSize;


    // Start is called before the first frame update
    void Start()
    {
        // get wind block direction and size
        if (transform.childCount == 0)
        {
            return;
        }
        windSize = transform.GetChild(0).localScale;

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



    void initParticleSystem()
    {
        // shape size
        var shape = particleSystem.shape;
        shape.scale = new Vector3(windSize.x, 1.0f, 1.0f);

        // start speed
        var main = particleSystem.main;
        // main.startSpeed = windForce / 3.0f;
        main.startSpeed = (ParticleSystem.MinMaxCurve)(Math.Log(Wind.windForce) * 2.0f);
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
