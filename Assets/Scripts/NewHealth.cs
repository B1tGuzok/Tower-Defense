using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewHealth : MonoBehaviour
{
    [Header("Attributes")]
    [SerializeField] public int hitPoints = 2;
    [SerializeField] private int currencyWorth = 10;

    private bool isDestroyed = false;

    public void TakeDamage(int dmg)
    {
        hitPoints -= dmg;

        if (hitPoints <= 0 && !isDestroyed)
        {
            NewEnemySpawner.onEnemyDestroy.Invoke();
            LvlManager.main.IncreaseCurrency(currencyWorth);
            isDestroyed = true;
            Destroy(gameObject);
        }
    }
}
