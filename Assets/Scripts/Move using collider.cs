using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damn : MonoBehaviour
{
    public int target = 0; //�� ����� ����� ����
    public Transform exit; //����� ������
    public Transform[] wayPoints; //������ �����
    public float navigation; //��� �������������� ����������� ������ ���������

    private Transform enemy; //���������� ����������
    private float navigationTime = 0; //���������� ��������� � ������������

    private void Start()
    {
        enemy = GetComponent<Transform>();
    }

    private void Update()
    {
        if (wayPoints != null) //���� �� ��� �����
        {
            Debug.Log("lol - 1");
            navigationTime += Time.deltaTime; //�������� � ��������� �����
            if (navigationTime > navigation) //
            {
                Debug.Log("lol - 2");
                if (target < wayPoints.Length) //���� ������, ��� ���-�� �����
                {
                    Debug.Log("lol - 3");
                    enemy.position = Vector2.MoveTowards(enemy.position, wayPoints[target].position, navigationTime); //����������� 
                }
                else
                {
                    Debug.Log("lol - 4");
                    enemy.position = Vector2.MoveTowards(enemy.position, exit.position, navigationTime); //��� � ����� ������
                }
                navigationTime = 0;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Point")
        {
            Debug.Log("Lol - 5");
            target += 1;
        }
        else if (collision.tag == "Finish")
        {
            Manager.Instance.removeEnemyFromScreen(); //�� ����� ���� instance
            Debug.Log("Lol - 6");
            Destroy(gameObject);
        }
    }
}
