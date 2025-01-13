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

    public GameObject pause;
    public GameObject resume;

    public Transform startPoint;
    public Transform[] path;

    public int currency;
    [HideInInspector] public int lives;
    public int maxLives; //for EnemySpawner

    private int check1;
    private int check2;

    public AudioSource loseSound;

    private void Awake()
    {
        main = this;
    }

    private void Start()
    {
        loseWindow.enabled = false;
        repeat.SetActive(false);
        resume.SetActive(false);

        if (GameData.ModeChoice == -1) //butchery
        {
            currency = 100;
            lives = 10;
            StartCoroutine(StartMoney());
        }
        else if (GameData.ModeChoice == 1) //campaign
        {
            Time.timeScale = 1f;
            switch (GameData.LvlChoice)
            {
                case 1:
                    currency = 100;
                    lives = 10;
                    break;
                case 2:
                    currency = 150;
                    lives = 15;
                    break;
                case 3:
                    currency = 200;
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
            loseSound.Play();
            repeat.SetActive(true);
            pause.SetActive(false);
            resume.SetActive(false);
            Time.timeScale = 0f;
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
            PlayerPrefs.SetInt("Record", check2 - 1);
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

    public void Pause()
    {
        pause.SetActive(false);
        resume.SetActive(true);
        Time.timeScale = 0f;
    }

    public void Resume()
    {
        pause.SetActive(true);
        resume.SetActive(false);
        Time.timeScale = 1f;
    }
}