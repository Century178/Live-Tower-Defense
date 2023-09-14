using UnityEngine;
using UnityEngine.SceneManagement;

public class Env : MonoBehaviour
{
    public static Env Instance { get; private set; }

    [SerializeField] GameObject enemy;
    private const float startDelay = 2f;
    private const float enemySpawnRate = 2f;
    private const float xPositionLimit = 11f;
    private const float yPositionLimit = 5f;
    private const string gameScene = "Game";

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
}
