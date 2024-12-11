using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewEnemyMovement : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Rigidbody2D rb;

    [Header("Attributes")]
    [SerializeField] private float moveSpeed = 2f;


    private Transform target;
    private int pathIndex = 0;

    private Transform safe; //����� �����������
    private int type; //��� ���������� ������

    private float baseSpeed;

    private void Start()
    {
        baseSpeed = moveSpeed;
        target = LvlManager.main.path[pathIndex];
    }

    private void Update()
    {
        if (Vector2.Distance(target.position, transform.position) <= 0.1f)
        {
            safe = target; //���������� �����
            pathIndex++;

            if (pathIndex == LvlManager.main.path.Length)
            {
                if (this.gameObject.name == "NewEnemy(Clone)")
                {
                    type = 1;
                }
                else if (this.gameObject.name == "NewTankEnemy(Clone)")
                {
                    type = 2;
                }
                if (!LvlManager.main.MinusLive(type))
                {
                    NewEnemySpawner.onEnemyDestroy.Invoke();
                    Destroy(gameObject);
                }
                return;
            }
            else
            {
                target = LvlManager.main.path[pathIndex];
            }

            Vector2 check = (target.position - safe.position);
            if (check.y > 0) //�������� �����
            {
                transform.rotation = Quaternion.Euler(0f, 0f, 0f);
                //Debug.Log("������� - 1");
            }
            else if (check.y < 0) //�������� �����
            {
                transform.rotation = Quaternion.Euler(0f, 0f, -180f);
                //Debug.Log("������� - 2");
            }
            else if (check.x < 0) //�������� ������
            {
                transform.rotation = Quaternion.Euler(0f, 0f, 90f);
                //Debug.Log("������� - 3");
            }
            else if (check.x > 0) //�������� ����
            {
                transform.rotation = Quaternion.Euler(0f, 0f, -90f);
                //Debug.Log("������� - 4");
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
