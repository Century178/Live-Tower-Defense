using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 100f;
    public float health = 5f;
    [SerializeField] private ParticleSystem damageEffect;
    public float damage = 1f;
    public int mana = 10;
    private bool burning = false;

    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        if (Env.Instance.freezeSpellOn)
        {
            rb.velocity = Vector2.zero;
        }
        else
        {
            Move();
        }

        if (Env.Instance.burnSpellOn && !burning)
        {
            Burn();
        }

        CheckHealth();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other)
        {
            if (other.gameObject.CompareTag("Tower"))
            {
                other.GetComponent<Tower>().Damage(damage);

                Destroy(gameObject);
                Env.Instance.GainMana(mana);
            }
        }
    }

    private void Burn()
    {
        burning = true;
        health -= Env.Instance.burnSpellDamage;
        damageEffect.Play();
        print("burned");

        if (Env.Instance.burnSpellOn)
        {
            Invoke("Burn", 1);
        }
        else
        {
            burning = false;
        }
    }

    private void Move()
    {
        Vector2 direction = Vector3.zero - transform.position;

        rb.velocity = direction.normalized * speed * Time.fixedDeltaTime;
    }

    private void CheckHealth()
    {
        if (health <= 0)
        {
            Destroy(gameObject);
            Env.Instance.GainMana(mana);
        }
    }
}
