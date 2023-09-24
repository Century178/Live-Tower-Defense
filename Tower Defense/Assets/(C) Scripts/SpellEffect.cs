using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellEffect : MonoBehaviour
{
    private float timeUntilDestroyed = 1;

    private SpriteRenderer sr;
    private float alpha;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        alpha = sr.color.a;
        Destroy(gameObject, timeUntilDestroyed);
    }

    private void Update()
    {
        alpha -= Time.deltaTime / timeUntilDestroyed;
        sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, alpha);
    }

    public void Resize(float radius)
    {
        transform.localScale = new Vector3(radius * 2, radius * 2, radius * 2);
    }
}
