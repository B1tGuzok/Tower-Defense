using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Profiling;


public class UpgradeUIHandler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public bool mouse_over = false;

    public void OnPointerEnter(PointerEventData evenData)
    {
        mouse_over = true;
        if (PlayerPrefs.GetInt("ModeChoose") == 1)
            NewUIManager.main.SetHoveringState(true);
        else
            UIManager.main.SetHoveringState(true);
    }

    public void OnPointerExit(PointerEventData evenData)
    {
        if (PlayerPrefs.GetInt("ModeChoose") == 1)
        {
            mouse_over = false;
            NewUIManager.main.SetHoveringState(false);
            gameObject.SetActive(false);
            NewMenu cost2 = FindObjectOfType<NewMenu>();
            cost2.ResetCost();
        }
        else
        {
            mouse_over = false;
            UIManager.main.SetHoveringState(false);
            gameObject.SetActive(false);
            Menu cost1 = FindObjectOfType<Menu>();
            cost1.ResetCost();
        }
    }

}


