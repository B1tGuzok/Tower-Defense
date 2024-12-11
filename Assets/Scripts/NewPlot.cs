using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewPlot : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private SpriteRenderer sr;
    [SerializeField] private Color hoverColor;
    
    private GameObject towerObj;
    public NewTurret turret;
    private Color startColor;

    private void Start()
    {
        startColor = sr.color;
    }

    private void OnMouseEnter()
    {
        sr.color = hoverColor;
    }

    private void OnMouseExit()
    {
        sr.color = startColor;
    }

    private void OnMouseDown()
    {
        if (NewUIManager.main.IsHoveringUI()) return;

        if (towerObj != null)
        {
            turret.OpenUpgradeUI();
            return;
        }

        Tower towerToBuild = NewBuildManager.main.GetSelectedTower();

        if (towerToBuild.cost > LvlManager.main.currency)
        {
            Debug.Log("Не можешь");
            return;
        }

        LvlManager.main.SpendCurrency(towerToBuild.cost);

        towerObj = Instantiate(towerToBuild.prefab, transform.position, Quaternion.identity);
        turret = towerObj.GetComponent<NewTurret>();
    }
}
