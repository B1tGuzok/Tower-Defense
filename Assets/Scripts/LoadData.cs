using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.U2D;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.U2D;
using UnityEngine.UI;

public class LoadData : MonoBehaviour
{
    [Header("References")]
    [SerializeField] TextMeshProUGUI recordUI;

    private Dictionary<string, Sprite> spriteDictionary;
    [SerializeField] int spriteQuantity; //for Debug

    [SerializeField] GameObject lvlButtons; //for open lvlButtons after "Ok" in campaign

    [SerializeField] GameObject[] lvls;

    private void Start()
    {
        string sceneName = SceneManager.GetActiveScene().name;
        if (sceneName == "Menu")
        {
            Sprite[] sprites = Resources.LoadAll<Sprite>("LvlButtons");
            spriteQuantity = sprites.Length;
            spriteDictionary = new Dictionary<string, Sprite>();
            foreach (var sprite in sprites)
            {
                spriteDictionary[sprite.name] = sprite;
            }
            LoadSprites();
            if (GameData.OpenLvls == true)
            {
                lvlButtons.GetComponent<ChangeScene>().ShowLvlButtons();
                GameData.OpenLvls = false;
            }
        }
        else
        {
            ShowWaves();
        }
    }

    public void LoadSprites()
    {
        for (int i = 0; i < lvls.Length; i++)
        {
            if (i <= PlayerPrefs.GetInt("openedLvl"))
            {
                int star = PlayerPrefs.GetInt($"star{i}");
                spriteDictionary.TryGetValue($"{i}" + $"{star}", out Sprite sprite);
                lvls[i].GetComponent<Image>().sprite = sprite;
                lvls[i].GetComponent<Button>().enabled = true;
            }
            else
            {
                spriteDictionary.TryGetValue($"Close", out Sprite sprite);
                lvls[i].GetComponent<Image>().sprite = sprite;
                lvls[i].GetComponent<Button>().enabled = false;
            }
        }
    }

    private void ShowWaves()
    {
        recordUI.text = PlayerPrefs.GetInt("Record").ToString();
    }

    public void DeletePlayerPrefs()
    {
        PlayerPrefs.DeleteAll();
    }
}
