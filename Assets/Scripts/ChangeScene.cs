using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChangeScene : MonoBehaviour
{
    [SerializeField] GameObject homeButton;
    [SerializeField] GameObject modeButtons;
    [SerializeField] GameObject lvlButtons;
    [SerializeField] GameObject topButton;

    public void OpenHome() { SceneManager.LoadScene("Menu"); }

    public void OpenTop() { SceneManager.LoadScene("Top"); }

    public void OpenLvl1() { SceneManager.LoadScene("Lvl1"); GameData.LvlChoice = 1; }

    public void OpenButchery() {
        SceneManager.LoadScene("Game");
        GameData.ModeChoice = -1;
    }

    public void OpenExit() { SceneManager.LoadScene("BeforeEnd"); }

    public void ShowLvlButtons()
    {
        GameData.ModeChoice = 1;
        modeButtons.SetActive(false);
        topButton.SetActive(false);
        lvlButtons.SetActive(true);
        homeButton.SetActive(true);
    }

    public void HideLvlButtons()
    {
        modeButtons.SetActive(true);
        topButton.SetActive(true);
        lvlButtons.SetActive(false);
        homeButton.SetActive(false);
    }

    public void Ok()
    {
        GameData.OpenLvls = true;
        OpenHome();
    }

    public void Exit()
    {
        Application.Quit();
        Debug.Log("�� �����!");
    }
}
