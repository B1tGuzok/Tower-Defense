using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Manager : Loader<Manager>
{
    /*
    public class Manager : MonoBehaviour
    public static Manager instance = null; //������� Manager �� ����� ���
    ������� ��-�� ������ �������
     */
    [SerializeField]
    GameObject spawnPoint; //����� ������
    [SerializeField]
    GameObject[] enemies; //������ ������
    [SerializeField]
    int maxEnemiesOnScrene; //������������ ���-�� ������ �� �����
    [SerializeField]
    int totalEnemies; //������������ ���-�� ������ �� �������
    [SerializeField]
    int enemiesPerSpawn; //������� ����������� ��������� �� ���

    private int enemiesOnScrene = 0; //���������� ������ 0

    const float SpawnDelay = 0.5f; //�������� ������

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
    ������� ��-�� ������ �������
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
            yield return new WaitForSeconds(SpawnDelay); //����� ������ SpawnDelay
            StartCoroutine(Spawn());
        }
    }

    public void removeEnemyFromScreen() //�������� �������
    {
        if (enemiesOnScrene > 0)
        {
            enemiesOnScrene -= 1;
        }
    }
}
