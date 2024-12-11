using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class NewMenu : MonoBehaviour
{
    [Header("References")]
    [SerializeField] TextMeshProUGUI currencyUI;
    [SerializeField] TextMeshProUGUI waveUI;
    [SerializeField] TextMeshProUGUI livesUI;
    [SerializeField] TextMeshProUGUI upgradeCostUI;
    [SerializeField] Animator anim;

    private bool isMenuOpen = true;

    public void ToggleMenu()
    {
        isMenuOpen = !isMenuOpen;
        anim.SetBool("MenuOpen", isMenuOpen);
    }

    private void OnGUI()
    {
        NewEnemySpawner numberOfWave = FindObjectOfType<NewEnemySpawner>();
        currencyUI.text = LvlManager.main.currency.ToString();
        waveUI.text = numberOfWave.currentWave.ToString();
        livesUI.text = LvlManager.main.lives.ToString();
    }

    public void SetCost(int cost)
    {
        upgradeCostUI.text = cost.ToString();
    }

    public void ResetCost()
    {
        upgradeCostUI.text = "choose tower";
    }

    public void Exit()
    {
        NewEnemySpawner enemySpawner = FindObjectOfType<NewEnemySpawner>();
        enemySpawner.Stop();
        SceneManager.LoadScene("Menu");
    }
}
