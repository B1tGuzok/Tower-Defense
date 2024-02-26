using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager main;

    public Transform startPoint;
    public Transform[] path; //массив точек

    public int currency;
    public int lives;

    private int check1;
    private int check2;

    private void Awake()
    {
        main = this;
    }

    private void Start()
    {
        currency = 100;
        lives = 3;
    }

    public void IncreaseCurrency(int amount)
    {
        currency += amount;
    }

    public bool SpendCurrency(int amount)
    {
        if (amount <= currency)
        {
            //buy
            currency -= amount;
            return true;
        } else
        {
            Debug.Log("Подкопи деньжат");
            return false;
        }
    }
    //----
    public void MinusLive()
    {
        if (lives > 1)
        {
            lives--;
        } else
        {
            EnemySpawner enemySpawner = FindObjectOfType<EnemySpawner>();
            enemySpawner.Stop();
            SceneManager.LoadScene("Menu");
        }
    }

    public void Record()
    {
        EnemySpawner enemySpawner = FindObjectOfType<EnemySpawner>();
        check1 = PlayerPrefs.GetInt("Record");
        check2 = enemySpawner.currentWave;
        if (check2 > check1)
        {
            PlayerPrefs.SetInt("Record", check2);
        } else
        {
            return;
        }
    }
}
