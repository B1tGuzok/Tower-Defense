using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBuildManager : MonoBehaviour
{
    public static NewBuildManager main;

    [Header("References")]
    [SerializeField] private Tower[] towers;

    private int selectedTower = 0;


    private void Awake()
    {
        main = this;
    }

    public Tower GetSelectedTower()
    {
        return towers[selectedTower];
    }

    public void SetSelectedTower(int _selectedTower)
    {
        selectedTower = _selectedTower;
        Debug.Log($"{towers.Length}");
    }
}
