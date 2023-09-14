using UnityEngine;
using TMPro;

public class Tower : MonoBehaviour
{
    public float health = 10f;
    [SerializeField] private TMP_Text healthUI;

    public void Damage(float value) {
        health -= value;
        healthUI.text = health.ToString();

        if (health <= 0) {
            Env.Instance.Defeat();
        }
    }
}
