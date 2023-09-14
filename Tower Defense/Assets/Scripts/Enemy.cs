using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 100f;
    public float damage = 1f;

    private Rigidbody2D rb;

    private void OnTriggerEnter2D(Collider2D other) {
        if (other) {
            if (other.tag == "Tower") {
                other.GetComponent<Tower>().Damage(damage);

                Destroy(gameObject);
            }
        }
    }

    private void Move() {
        Vector2 direction = Vector3.zero - transform.position;

        rb.velocity = direction.normalized * speed * Time.fixedDeltaTime;
    }

    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate() {
        Move();
    }
}
