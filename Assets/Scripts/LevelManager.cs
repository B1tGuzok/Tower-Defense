using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public static LevelManager main;

    public Image loseWindow;
    public GameObject repeat;

    public Transform startPoint;
    public Transform[] path;

    public int currency;
    [HideInInspector] public int lives;
    public int maxLives; //for EnemySpawner

    private int check1;
    private int check2;

    private void Awake()
    {
        main = this;
    }

    private void Start()
    {
        currency = 1000;
        loseWindow.enabled = false;
        repeat.SetActive(false);

        if (GameData.ModeChoice == -1) //butchery
        {
            lives = 10;
            StartCoroutine(StartMoney());
        }
        else if (GameData.ModeChoice == 1) //campaign
        {
            switch (GameData.LvlChoice)
            {
                case 1:
                    lives = 1;
                    break;
                case 2:
                    lives = 15;
                    break;
                case 3:
                    lives = 20;
                    break;
            }
        }
        maxLives = lives;
    }

    private IEnumerator StartMoney()
    {
        yield return new WaitForSeconds(10f);
        currency += 5;
        StartCoroutine(StartMoney());
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
        }
        else
        {
            Debug.Log("Ïîäêîïè äåíüæàò");
            return false;
        }
    }

    public void MinusLive(int type)
    {
        if (lives > 1 && type == 1)
        {
            lives--;
        }
        else if (lives > 2 && type == 2)
        {
            lives -= 3;
        }
        else
        {
            EnemySpawner enemySpawner = FindObjectOfType<EnemySpawner>();
            enemySpawner.Stop();
            StopCoroutine(StartMoney());
            SceneManager.LoadScene("Menu");
        }
    }

    //from new LevelManager Script (new hurt logic)
    public bool BoolMinusLive(int type)
    {
        if (lives > 1 && type == 1)
        {
            lives--;
            Debug.Log($"Минус жизнь!");
            return false; //жизни ещё остались
        }
        else if (lives > 2 && type == 2)
        {
            lives -= 3;
            Debug.Log($"Минус жизнь!");
            return false;
        }
        else
        {
            EnemySpawner enemySpawner = FindObjectOfType<EnemySpawner>();
            enemySpawner.Stop();
            loseWindow.enabled = true;
            repeat.SetActive(true);
            Debug.Log($"Жизни всё!");
            return true;
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
        }
    }

    public void WriteLvlProgress(int gotStar) //3 - gold
    {
        int openedLvl = PlayerPrefs.GetInt("openedLvl");
        int newLvl = GameData.LvlChoice;
        int star = PlayerPrefs.GetInt($"star{GameData.LvlChoice}");
        if (newLvl > openedLvl)
        {
            PlayerPrefs.SetInt("openedLvl", newLvl);
        }
        if (gotStar > star)
        {
            PlayerPrefs.SetInt($"star{GameData.LvlChoice - 1}", gotStar);
        }
    }
}