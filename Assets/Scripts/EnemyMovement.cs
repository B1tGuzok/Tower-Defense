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

    private Transform safe; //смена направления

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
            safe = target; //предыдущая точка
            pathIndex++;
            
            if (pathIndex == LevelManager.main.path.Length)
            {
                EnemySpawner.onEnemyDestroy.Invoke();
                Destroy(gameObject);
                //-----
                LevelManager.main.MinusLive();
                return;
            } else
            {
                target = LevelManager.main.path[pathIndex];
            }

            Vector2 check = (target.position - safe.position);
            if (check.y > 0) //движение вверх
            {
                transform.rotation = Quaternion.Euler(0f, 0f, 0f);
                //Debug.Log("Условие - 1");
            }
            else if (check.y < 0) //движение влево
            {
                transform.rotation = Quaternion.Euler(0f, 0f, -180f);
                //Debug.Log("Условие - 2");
            }
            else if (check.x < 0) //движение вправо
            {
                transform.rotation = Quaternion.Euler(0f, 0f, 90f);
                //Debug.Log("Условие - 3");
            }
            else if (check.x > 0) //движение вниз
            {
                transform.rotation = Quaternion.Euler(0f, 0f, -90f);
                //Debug.Log("Условие - 4");
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
