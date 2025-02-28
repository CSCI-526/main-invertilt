using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallShadowEffect : MonoBehaviour
{
    public GameObject shadowPrefab; 
    public float spawnRate = 0.05f;
    public float fadeSpeed = 3f; 

    void Start()
    {
        StartCoroutine(SpawnShadows());
    }

    IEnumerator SpawnShadows()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnRate);

            
            GameObject shadow = Instantiate(shadowPrefab, transform.position, transform.rotation);
            StartCoroutine(FadeAndDestroy(shadow));
        }
    }

    IEnumerator FadeAndDestroy(GameObject shadow)
    {
        SpriteRenderer sr = shadow.GetComponent<SpriteRenderer>();
        Color color = sr.color;

        while (color.a > 0)
        {
            color.a -= fadeSpeed * Time.deltaTime;
            sr.color = color;
            yield return null;
        }

        Destroy(shadow);
    }
}
