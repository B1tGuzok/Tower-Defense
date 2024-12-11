using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class NewEnemySpawner : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject[] enemyPrefabs;

    [Header("Attributes")]
    [SerializeField] private int baseEnemies = 5;
    [SerializeField] private float enemiesPerSecond = 0.5f;
    [SerializeField] private float timeBetweenWaves = 5f;
    [SerializeField] private float difficultyScalingFactor = 0.65f;
    [SerializeField] private float enemiesPerSecondCap = 15f;

    [Header("Events")]
    public static UnityEvent onEnemyDestroy = new UnityEvent();

    public int currentWave = 1;
    private float timeSinceLastSpawn;
    private int enemiesAlive;
    private int enemiesLeftToSpawn;
    private float eps; //enemies per second
    private bool isSpawning = false;

    //MINE
    private int waveCount = 0;
    private int[] enemiesCountPerWave = new int[] { 1 };
    private int enemiesPerLvl = 0; //количество врагов за весь уровень
    private int destroyedEnemies = 0; //количество уничтоженных врагов (либо прошли до конца, либо убили защитники)

    private void Awake()
    {
        onEnemyDestroy.AddListener(EnemyDestroyed);
    }

    private void Start()
    {
        //StartCoroutine(StartWave());
        StartCoroutine(MineStartWave());
    }

    private void Update()
    {
        //if (!isSpawning) return;
        //timeSinceLastSpawn += Time.deltaTime;

        //if (timeSinceLastSpawn >= (1f / eps) && enemiesLeftToSpawn > 0)
        //{
        //    SpawnEnemy();
        //    enemiesLeftToSpawn--;
        //    enemiesAlive++;
        //    timeSinceLastSpawn = 0f;
        //}

        //if (enemiesAlive == 0 && enemiesLeftToSpawn == 0)
        //{
        //    EndWave();
        //}
    }

    private void EnemyDestroyed()
    {
        destroyedEnemies++;
        if (destroyedEnemies == enemiesPerLvl)
        {
            Debug.Log($"Вы убили последнего!");
            Invoke("LoadScene", 5f);
        }
    }

    private void LoadScene()
    {
        SceneManager.LoadScene("Menu");
    }

    //private IEnumerator StartWave()
    //{
    //    yield return new WaitForSeconds(timeBetweenWaves);
    //    isSpawning = true;
    //    enemiesLeftToSpawn = EnemiesPerWave();
    //    eps = EnemiesPerSecond();
    //}

    private IEnumerator MineStartWave()
    {
        while (waveCount != enemiesCountPerWave.Length)
        {
            Debug.Log($"Волна номер - {waveCount + 1}");
            yield return new WaitForSeconds(5f);

            for (int i = 0; i < enemiesCountPerWave[waveCount]; i++)
            {
                int index = Random.Range(0, 100);
                if (index >= 95)
                {
                    GameObject prefabToSpawn = enemyPrefabs[1];
                    Instantiate(prefabToSpawn, LvlManager.main.startPoint.position, Quaternion.Euler(0f, 0f, 180f));
                }
                else
                {
                    GameObject prefabToSpawn = enemyPrefabs[0];
                    Instantiate(prefabToSpawn, LvlManager.main.startPoint.position, Quaternion.Euler(0f, 0f, 180f));
                }
                enemiesPerLvl++;
                yield return new WaitForSeconds(1f);
            }
            waveCount++;
        }
        StopCoroutine(MineStartWave());
    }

    //private void EndWave()
    //{
    //    isSpawning = false;
    //    timeSinceLastSpawn = 0f;
    //    currentWave++;
    //    LevelManager.main.Record();
    //    StartCoroutine(StartWave());
    //}

    //private void SpawnEnemy()
    //{
    //    int index = Random.Range(0, 100);
    //    if (index >= 95)
    //    {
    //        GameObject prefabToSpawn = enemyPrefabs[1];
    //        Instantiate(prefabToSpawn, LevelManager.main.startPoint.position, Quaternion.Euler(0f, 0f, 180f));
    //    } else
    //    {
    //        GameObject prefabToSpawn = enemyPrefabs[0];
    //        Instantiate(prefabToSpawn, LevelManager.main.startPoint.position, Quaternion.Euler(0f, 0f, 180f));
    //    }
    //}

    //private int EnemiesPerWave()
    //{
    //    return Mathf.RoundToInt(baseEnemies * Mathf.Pow(currentWave, difficultyScalingFactor));
    //}

    //private float EnemiesPerSecond()
    //{
    //    return Mathf.Clamp(enemiesPerSecond * Mathf.Pow(currentWave, difficultyScalingFactor), 0f, enemiesPerSecondCap);
    //}

    //-------
    public void Stop()
    {
        isSpawning = false;
        Debug.Log("You Lose!");
    }
}
