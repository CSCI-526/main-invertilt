using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class BallAnimation : MonoBehaviour
{
    private Vector3 originalScale;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        originalScale = transform.localScale;
    }

    void Update()
    {
       
        float stretchFactor = Mathf.Clamp(1 + Mathf.Abs(rb.velocity.y) * 0.05f, 1, 1.3f);
        transform.localScale = new Vector3(originalScale.x / stretchFactor, originalScale.y * stretchFactor, originalScale.z);
    }
}
