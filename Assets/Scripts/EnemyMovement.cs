using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Rigidbody2D rb;

    [Header("Attributes")]
    [SerializeField] private float moveSpeed = 2f;


    private Transform target;
    private int pathIndex = 0;

    private Transform safe; //ñìåíà íàïðàâëåíèÿ
    private int type; //äëÿ óìåíüøåíèÿ æèçíåé

    private float baseSpeed;

    private void Start()
    {
        baseSpeed = moveSpeed;
        target = LevelManager.main.path[pathIndex];
    }

    private void Update()
    {
        if (Vector2.Distance(target.position, transform.position) <= 0.1f)
        {
            safe = target; //ïðåäûäóùàÿ òî÷êà
            pathIndex++;

            if (pathIndex == LevelManager.main.path.Length)
            {
                if (this.gameObject.name == "Enemy(Clone)")
                {
                    type = 1;
                }
                else if (this.gameObject.name == "TankEnemy(Clone)")
                {
                    type = 2;
                }

                Debug.Log($"Минусую жизнь!");
                if (GameData.ModeChoice == 1) //campaign
                {
                    bool lose = LevelManager.main.BoolMinusLive(type);
                    if (!lose)
                    {
                        Debug.Log($"Уничтожаюсь!");
                        EnemySpawner.onEnemyDestroy.Invoke();
                    }
                }
                else if (GameData.ModeChoice == -1) //butchery
                {
                    LevelManager.main.MinusLive(type);
                    Debug.Log($"Уничтожаюсь!");
                    EnemySpawner.onEnemyDestroy.Invoke();
                }
                Destroy(gameObject);
                return;
            }
            else
            {
                target = LevelManager.main.path[pathIndex];
            }

            Vector2 check = (target.position - safe.position);
            if (check.y > 0) //äâèæåíèå ââåðõ
            {
                transform.rotation = Quaternion.Euler(0f, 0f, 0f);
                //Debug.Log("Óñëîâèå - 1");
            }
            else if (check.y < 0) //äâèæåíèå âëåâî
            {
                transform.rotation = Quaternion.Euler(0f, 0f, -180f);
                //Debug.Log("Óñëîâèå - 2");
            }
            else if (check.x < 0) //äâèæåíèå âïðàâî
            {
                transform.rotation = Quaternion.Euler(0f, 0f, 90f);
                //Debug.Log("Óñëîâèå - 3");
            }
            else if (check.x > 0) //äâèæåíèå âíèç
            {
                transform.rotation = Quaternion.Euler(0f, 0f, -90f);
                //Debug.Log("Óñëîâèå - 4");
            }

        }
    }

    private void FixedUpdate()
    {
        Vector2 direction = (target.position - transform.position).normalized;
        rb.velocity = direction * moveSpeed;
    }

    public void UpdateSpeed(float newSpeed)
    {
        moveSpeed = newSpeed;
    }

    public void ResetSpeed()
    {
        moveSpeed = baseSpeed;
    }
}