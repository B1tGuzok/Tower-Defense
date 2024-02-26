using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Menu : MonoBehaviour
{
    [Header("References")]
    [SerializeField] TextMeshProUGUI currencyUI;
    [SerializeField] TextMeshProUGUI waveUI;
    [SerializeField] TextMeshProUGUI livesUI;
    [SerializeField] Animator anim;

    private bool isMenuOpen = true;

    public void ToggleMenu()
    {
        isMenuOpen = !isMenuOpen;
        anim.SetBool("MenuOpen", isMenuOpen);
    }

    private void OnGUI()
    {
        EnemySpawner numberOfWave = FindObjectOfType<EnemySpawner>();
        currencyUI.text = LevelManager.main.currency.ToString();
        waveUI.text = numberOfWave.currentWave.ToString();
        livesUI.text = LevelManager.main.lives.ToString();
    }

    public void SetSelected()
    {

    }
}
