using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellEffect : MonoBehaviour
{
    private SpriteRenderer sr;
    private float alpha;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        alpha = sr.color.a;
    }

    private void Update()
    {
        alpha -= Time.deltaTime;
        sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, alpha);

        if (alpha <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void Resize(float radius)
    {
        transform.localScale = new Vector3(radius * 2, radius * 2, radius * 2);
    }
}
