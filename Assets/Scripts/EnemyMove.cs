using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Rigidbody2D rb;

    [Header("Attributes")]
    [SerializeField] private float moveSpeed = 2f;

    private Transform target; 
    private int wayIndex = 0;


    private void Start()
    {
        target = LevelManager.main.way[wayIndex];
    }

    private void Update()
    {
        if (Vector2.Distance(target.position, transform.position) <= 0.1f) //vector2.distance возвращает расстояние между a и b
        {
            wayIndex++;


            if (wayIndex == LevelManager.main.way.Length)
            {
                Destroy(gameObject);
                return;
            } else
            {
                target = LevelManager.main.way[wayIndex];
            }
        }
    }

    private void FixedUpdate()
    {
        Vector2 direction = (target.position - transform.position);
        float distanceToTarget = direction.magnitude;

        if (distanceToTarget < 0.1f)
        {
            transform.position = target.position;
            rb.velocity = Vector2.zero;
        }
        else
        {
            rb.velocity = direction.normalized * moveSpeed;
        }
    }
}
