using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public void GameScene()
    {
        SceneManager.LoadScene("Game");
    }

    public void ExitScene()
    {
        SceneManager.LoadScene("BeforeEnd");
    }

    public void MenuScene()
    {
        SceneManager.LoadScene("Menu");
    }
}
