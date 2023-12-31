using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Environment : MonoBehaviour
{
    #region Variables
    public static Environment Instance;

    [SerializeField] private GameObject[] enemies;

    [SerializeField] private float startDelay = 2f;
    [SerializeField] private float spawnRate = 2f;
    [SerializeField] private float rampUp = 0.01f;
    private float spawnTime;

    [SerializeField] private float xPositionLimit = 11f;
    [SerializeField] private float yPositionLimit = 5f;

    private bool spawningStarted;
    #endregion

    private void Awake()
    {
        if (Instance != null) Destroy(gameObject);
        else Instance = this;
    }

    private IEnumerator Start()
    {
        yield return new WaitForSecondsRealtime(startDelay);
        SpawnEnemy();
    }

    private void Update()
    {
        if (spawningStarted)
        {
            spawnTime -= Time.deltaTime;

            if (spawnTime <= 0)
            {
                SpawnEnemy();
                spawnTime = spawnRate;
                spawnRate -= rampUp;
            }
        }
    }

    private void SpawnEnemy()
    {
        Vector2 position = new Vector2(Mathf.Sign(Random.Range(-1, 1)) * xPositionLimit, Mathf.Sign(Random.Range(-1, 1)) * yPositionLimit);

        GameObject enemy = enemies[Random.Range(0, enemies.Length)];
        Instantiate(enemy, position, Quaternion.identity);

        if (!spawningStarted) spawningStarted = true;
    }
}
