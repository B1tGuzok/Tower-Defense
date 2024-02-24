using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Manager : Loader<Manager>
{
    /*
    public class Manager : MonoBehaviour
    public static Manager instance = null; //сначала Manager на сцене нет
    удалено из-за нового подхода
     */
    [SerializeField]
    GameObject spawnPoint; //точка спавна
    [SerializeField]
    GameObject[] enemies; //массив врагов
    [SerializeField]
    int maxEnemiesOnScrene; //максимальное кол-во врагов на сцене
    [SerializeField]
    int totalEnemies; //максимальное кол-во врагов за уровень
    [SerializeField]
    int enemiesPerSpawn; //сколько противников спавнится за раз

    private int enemiesOnScrene = 0; //изначально врагов 0

    const float SpawnDelay = 0.5f; //задержка спавна

    /*private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    удалено из-за нового подхода
    }*/

    private void Start()
    {
        StartCoroutine(Spawn());
    }

    IEnumerator Spawn()
    {
        if (enemiesPerSpawn > 0 && enemiesOnScrene < totalEnemies)
        {
            for (int i = 0; i < enemiesPerSpawn; i++)
            {
                if (enemiesOnScrene < maxEnemiesOnScrene)
                {
                    GameObject newEnemy = Instantiate(enemies[0], transform) as GameObject;
                    newEnemy.transform.position = spawnPoint.transform.position;
                    enemiesOnScrene += 1;
                }
            }
            yield return new WaitForSeconds(SpawnDelay); //спавн каждые SpawnDelay
            StartCoroutine(Spawn());
        }
    }

    public void removeEnemyFromScreen() //удаление объекта
    {
        if (enemiesOnScrene > 0)
        {
            enemiesOnScrene -= 1;
        }
    }
}
