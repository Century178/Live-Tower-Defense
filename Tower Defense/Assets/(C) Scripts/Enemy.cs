using UnityEngine;
using System.Collections;
using TMPro;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private int health = 5;
    [SerializeField] private int damage = 1;
    [SerializeField] private int manaOnKill = 10;
    private bool isFrozen;

    private Rigidbody2D rb;
    [SerializeField] private TextMeshPro healthText;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        healthText.text = health.ToString();
    }

    void FixedUpdate()
    {
        if (!isFrozen) Move();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out Tower tower))
        {
            tower.Health(-damage);
            Death();
        }
    }

    public void Damage()
    {
        health--;
        healthText.text = health.ToString();

        if (health <= 0)
        {
            Death();
        }
    }

    private void Death()
    {
        Destroy(gameObject);
        Tower.Instance.Mana(manaOnKill);
    }

    public IEnumerator Freeze(float duration)
    {
        isFrozen = true;
        rb.velocity = Vector3.zero;
        yield return new WaitForSeconds(duration);
        isFrozen = false;
    }

    private void Move()
    {
        Vector2 direction = Vector3.zero - transform.position;

        rb.velocity = direction.normalized * moveSpeed;
    }
}
