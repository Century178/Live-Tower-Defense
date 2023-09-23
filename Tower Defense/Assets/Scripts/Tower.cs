using UnityEngine;
using TMPro;

public class Tower : MonoBehaviour
{
    public float health = 10f;
    private float maxHealth;
    [SerializeField] private TMP_Text healthUI;

    private void Awake()
    {
        maxHealth = health;
    }

    public void Damage(float value)
    {
        health -= value;
        healthUI.text = health.ToString();

        if (health <= 0)
        {
            Env.Instance.Defeat();
        }
    }

    public void Heal(float value)
    {
        health += value;
        
        if (health > maxHealth)
        {
            health = maxHealth;
        }

        healthUI.text = health.ToString();
    }
}
