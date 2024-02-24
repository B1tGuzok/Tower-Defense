using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerManager : Loader<TowerManager>
{
    TowerButton towerButtonPressed;

    public void SelectedTower(TowerButton towerSelected)
    {
        towerButtonPressed = towerSelected;
        Debug.Log("Pressed" + towerButtonPressed.gameObject);
    }
}
