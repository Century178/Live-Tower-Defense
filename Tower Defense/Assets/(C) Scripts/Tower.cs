using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Tower : MonoBehaviour
{
    #region Variables
    public static Tower Instance;

    [SerializeField] private Camera mainCamera;
    private Vector2 worldMousePos;

    [SerializeField] private LayerMask enemyLayer;

    [Header("Health")]
    [SerializeField] private float maxHealth;
    private float health = 1;
    [SerializeField] private int healCost;
    [SerializeField] private TextMeshProUGUI healthText;

    [Header("Mana")]
    [SerializeField] private int initialMana;
    private int mana;
    [SerializeField] private TextMeshProUGUI manaText;

    [Header("Burn Spell")]
    [SerializeField] private int burnCost;
    [SerializeField] private float burnRadius;
    [SerializeField] private GameObject burnEffect;

    [Header("Freeze Spell")]
    [SerializeField] private int freezeCost;
    [SerializeField] private float freezeRadius;
    [SerializeField] private int freezeDuration;
    [SerializeField] private GameObject freezeEffect;
    #endregion

    #region Unity Methods
    private void Awake()
    {
        if (Instance != null) Destroy(gameObject);
        else Instance = this;

        health = maxHealth;
        Mana(initialMana);
    }

    private void Update()
    {
        worldMousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);

        if (Input.GetMouseButtonDown(0)) BurnSpell();

        if (Input.GetMouseButtonDown(1)) FreezeSpell();

        if (Input.GetKeyDown(KeyCode.Space)) HealSpell();
    }
    #endregion

    #region Resources
    public void Health(int value)
    {
        health += value;
        healthText.text = "Health: " + health.ToString();

        if (health <= 0) SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Mana(int value)
    {
        mana += value;
        manaText.text = "Mana: " + mana.ToString();
    }
    #endregion

    #region Spells
    public void BurnSpell()
    {
        if (mana >= burnCost)
        {
            Mana(-burnCost);

            Instantiate(burnEffect, worldMousePos, Quaternion.identity);
            if (burnEffect.GetComponent<SpellEffect>() != null)
            {
                SpellEffect effect = burnEffect.GetComponent<SpellEffect>();
                effect.Resize(burnRadius);
            }

            Collider2D[] enemies = Physics2D.OverlapCircleAll(worldMousePos, burnRadius, enemyLayer);
            foreach (Collider2D enemy in enemies)
            {
                if (enemy.gameObject.GetComponent<Enemy>() != null)
                {
                    Enemy enemyScript = enemy.gameObject.GetComponent<Enemy>();
                    enemyScript.Damage();
                }
            }
        }
    }

    public void FreezeSpell()
    {
        if (mana >= freezeCost)
        {
            Mana(-freezeCost);

            Instantiate(freezeEffect, worldMousePos, Quaternion.identity);
            if (freezeEffect.GetComponent<SpellEffect>() != null)
            {
                SpellEffect effect = freezeEffect.GetComponent<SpellEffect>();
                effect.Resize(freezeRadius);
            }

            Collider2D[] enemies = Physics2D.OverlapCircleAll(worldMousePos, freezeRadius, enemyLayer);
            foreach (Collider2D enemy in enemies)
            {
                if (enemy.gameObject.GetComponent<Enemy>() != null)
                {
                    Enemy enemyScript = enemy.gameObject.GetComponent<Enemy>();
                    enemyScript.StartCoroutine(enemyScript.Freeze(freezeDuration));
                }
            }
        }
    }

    public void HealSpell()
    {
        if (mana >= healCost)
        {
            if (health == maxHealth) return;
            Mana(-healCost);
            health++;
        }
    }
    #endregion
}
