using UnityEngine;
using UnityEngine.SceneManagement;

public class BlackHole2D : MonoBehaviour
{
    public float pullRadius = 4f;
    public float pullStrength = 5000f;
    public float maxSpeed = 10f;
    public float spinForce = 140f;

    public float rotationSpeed = 100f;

    private LineRenderer lineRenderer;
    private int segments = 120;

    public float waveAmplitude = 0.2f;
    public float waveFrequency = 2f;

    private void Start()
    {
        lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.positionCount = segments + 1;
        lineRenderer.useWorldSpace = false;
        lineRenderer.startWidth = 0.05f;
        lineRenderer.endWidth = 0.05f;
        lineRenderer.loop = true;
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        lineRenderer.startColor = Color.black;
        lineRenderer.endColor = Color.gray;
        DrawWavyCircle();
    }

    private void FixedUpdate()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, pullRadius);

        foreach (Collider2D col in colliders)
        {
            if (col.CompareTag("Player"))
            {
                Rigidbody2D ballRb = col.GetComponent<Rigidbody2D>();

                if (ballRb != null)
                {
                    float distance = Vector2.Distance(transform.position, col.transform.position);
                    if (distance < 0.1f) return;

                    float forceMagnitude = pullStrength / (distance * distance);
                    forceMagnitude = Mathf.Clamp(forceMagnitude, 100f, pullStrength);

                    Vector2 direction = (transform.position - col.transform.position).normalized;

                    ballRb.AddForce(direction * forceMagnitude * Time.fixedDeltaTime, ForceMode2D.Impulse);

                    ballRb.AddTorque(spinForce * Time.fixedDeltaTime, ForceMode2D.Impulse);

                    if (ballRb.velocity.magnitude > maxSpeed)
                    {
                        ballRb.velocity = ballRb.velocity.normalized * maxSpeed;
                    }
                }
            }
        }
    }

    private void Update()
    {
        transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);
        DrawWavyCircle();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Ball touched the black hole! Restarting...");
            Invoke("RestartScene", 1f);
        }
    }

    private void DrawWavyCircle()
    {
        float angleStep = 360f / segments;
        float baseRadius = pullRadius * 0.5f;
        float maxRadius = pullRadius * 0.5f;

        for (int i = 0; i <= segments; i++)
        {
            float angle = i * angleStep * Mathf.Deg2Rad;
            float wave = Mathf.Sin(Time.time * waveFrequency + angle * 4) * waveAmplitude;
            float radius = Mathf.Lerp(baseRadius, maxRadius, (wave + 1) / 2);

            float x = Mathf.Cos(angle) * radius;
            float y = Mathf.Sin(angle) * radius;

            lineRenderer.SetPosition(i, new Vector3(x, y, 0));
        }
    }

    private void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
