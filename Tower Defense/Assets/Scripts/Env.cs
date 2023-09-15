using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Env : MonoBehaviour
{
    public static Env Instance { get; private set; }

    public int mana = 10;
    [SerializeField] private TMP_Text manaUI;
    [SerializeField] private GameObject tower;
    private const string gameScene = "Game";
    
    #region Enemies
    [SerializeField] private GameObject enemy;
    private const float startDelay = 2f;
    private const float enemySpawnRate = 2f;
    private const float xPositionLimit = 11f;
    private const float yPositionLimit = 5f;
    #endregion

    #region Spells
    [SerializeField] private ParticleSystem burnSpellEffect;
    [SerializeField] private ParticleSystem freezeSpellEffect;
    [SerializeField] private ParticleSystem healSpellEffect;
    private const KeyCode burnSpellKey = KeyCode.Q;
    private const KeyCode freezeSpellKey = KeyCode.W;
    private const KeyCode healSpellKey = KeyCode.E;
    private const float burnSpellDuration = 5f;
    private const float freezeSpellDuration = 5f;
    private const float healSpellAmount = 3f;
    private const int burnSpellCost = 10;
    private const int freezeSpellCost = 10;
    private const int healSpellCost = 10;
    public bool burnSpellOn = false;
    public bool freezeSpellOn = false;
    public float burnSpellDamage = 1f;
    #endregion

    public void GainMana(int value) {
        mana += value;
        UpdateManaUI();
    }

    public void UseMana(int value) {
        mana -= value;
        UpdateManaUI();
    }

    private void UpdateManaUI() {
        manaUI.text = "Mana: " + mana;
    }

    private void DisableBurnSpell() {
        burnSpellOn = false;
    }

    public void BurnSpell() {
        if (mana >= burnSpellCost) {
            UseMana(burnSpellCost);
            burnSpellOn = true;
            Invoke("DisableBurnSpell", burnSpellDuration);
            burnSpellEffect.Play();
        }
    }

    private void DisableFreezeSpell() {
        freezeSpellOn = false;
    }

    public void FreezeSpell() {
        if (mana >= freezeSpellCost) {
            UseMana(freezeSpellCost);
            freezeSpellOn = true;
            Invoke("DisableFreezeSpell", freezeSpellDuration);
            freezeSpellEffect.Play();
        }
    }

    public void HealSpell() {
        if (tower && mana >= healSpellCost) {
            UseMana(healSpellCost);
            tower.GetComponent<Tower>().Heal(healSpellAmount);
            healSpellEffect.Play();
        }
    }

    private void SpawnEnemy() {
        int xRand = Random.Range(-1, 2);
        int yRand = Random.Range(-1, 2);

        if (xRand == 0 && yRand == 0) {
            xRand = 1;
        }

        Vector2 position = new Vector2(xRand * xPositionLimit, yRand * yPositionLimit);

        Instantiate(enemy, position, Quaternion.identity);
    }

    public void Defeat() {
        SceneManager.LoadScene(gameScene, LoadSceneMode.Single);
    }

    private void Controls() {
        if (Input.GetKeyDown(burnSpellKey)) {
            BurnSpell();
        }
        if (Input.GetKeyDown(freezeSpellKey)) {
            FreezeSpell();
        }
        if (Input.GetKeyDown(healSpellKey)) {
            HealSpell();
        }
    }

    private void Awake() {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        InvokeRepeating("SpawnEnemy", startDelay, enemySpawnRate);
    }

    private void Update() {
        Controls();
    }
}
