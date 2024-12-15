using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EnemySpawner : MonoBehaviour
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

    //BUTCHERY
    public int currentWave = 1;
    private float timeSinceLastSpawn;
    private int enemiesAlive;
    private int enemiesLeftToSpawn;
    private float eps; //enemies per second
    private bool isSpawning = false;

    //CAMPAIGN
    private int waveCount = 0;
    private int[] enemiesCountPerWave = new int[] { 1 };
    private int enemiesPerLvl = 0; //количество врагов за весь уровень
    private int destroyedEnemies = 0; //количество уничтоженных врагов (либо прошли до конца, либо убили защитники)

    public Image winWindow;
    public Sprite win1, win2, win3; //win1 - gold
    public GameObject ok;

    private void Awake()
    {
        onEnemyDestroy.AddListener(EnemyDestroyed);
    }

    private void Start()
    {
        winWindow.enabled = false;
        ok.SetActive(false);
        if (GameData.ModeChoice == 1) //campaign
        {
            StartCoroutine(MineStartWave());
        }
        else if (GameData.ModeChoice == -1) //butchery
        {
            StartCoroutine(StartWave());
        }
    }

    private void Update()
    {
        if (GameData.ModeChoice == -1) //butchery
        {
            if (!isSpawning) return;
            timeSinceLastSpawn += Time.deltaTime;

            if (timeSinceLastSpawn >= (1f / eps) && enemiesLeftToSpawn > 0)
            {
                SpawnEnemy();
                enemiesLeftToSpawn--;
                enemiesAlive++;
                timeSinceLastSpawn = 0f;
            }

            if (enemiesAlive == 0 && enemiesLeftToSpawn == 0)
            {
                EndWave();
            }
        }
    }

    private void EnemyDestroyed()
    {
        if (GameData.ModeChoice == 1) //campaign
        {
            destroyedEnemies++;
            if (destroyedEnemies == enemiesPerLvl)
            {
                if (LevelManager.main.maxLives == LevelManager.main.lives)
                {
                    winWindow.sprite = win1;
                    LevelManager.main.WriteLvlProgress(3);
                }
                else if (LevelManager.main.lives >= LevelManager.main.maxLives / 2)
                {
                    winWindow.sprite = win2;
                    LevelManager.main.WriteLvlProgress(2);
                }
                else
                {
                    winWindow.sprite = win3;
                    LevelManager.main.WriteLvlProgress(1);
                }
                winWindow.enabled = true;
                ok.SetActive(true);
                Debug.Log($"Вы убили последнего!");
            }
        }
        else if (GameData.ModeChoice == -1) //butchery
        {
            enemiesAlive--;
        }
    }

    private IEnumerator StartWave()
    {
        yield return new WaitForSeconds(timeBetweenWaves);
        isSpawning = true;
        enemiesLeftToSpawn = EnemiesPerWave();
        eps = EnemiesPerSecond();
    }

    private void EndWave()
    {
        isSpawning = false;
        timeSinceLastSpawn = 0f;
        currentWave++;
        LevelManager.main.Record();
        StartCoroutine(StartWave());
    }

    private void SpawnEnemy()
    {
        int index = Random.Range(0, 100);
        if (index >= 95)
        {
            GameObject prefabToSpawn = enemyPrefabs[1];
            Instantiate(prefabToSpawn, LevelManager.main.startPoint.position, Quaternion.Euler(0f, 0f, 180f));
        }
        else
        {
            GameObject prefabToSpawn = enemyPrefabs[0];
            Instantiate(prefabToSpawn, LevelManager.main.startPoint.position, Quaternion.Euler(0f, 0f, 180f));
        }
    }

    private int EnemiesPerWave()
    {
        return Mathf.RoundToInt(baseEnemies * Mathf.Pow(currentWave, difficultyScalingFactor));
    }

    private float EnemiesPerSecond()
    {
        return Mathf.Clamp(enemiesPerSecond * Mathf.Pow(currentWave, difficultyScalingFactor), 0f, enemiesPerSecondCap);
    }

    public void Stop()
    {
        isSpawning = false;
        Debug.Log("You Lose!");
    }

    //from new EnemySpawner Script (new spawn logic)
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
                    Instantiate(prefabToSpawn, LevelManager.main.startPoint.position, Quaternion.Euler(0f, 0f, 180f));
                }
                else
                {
                    GameObject prefabToSpawn = enemyPrefabs[0];
                    Instantiate(prefabToSpawn, LevelManager.main.startPoint.position, Quaternion.Euler(0f, 0f, 180f));
                }
                enemiesPerLvl++;
                yield return new WaitForSeconds(1f);
            }
            waveCount++;
        }
        StopCoroutine(MineStartWave());
    }
}