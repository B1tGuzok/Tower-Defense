using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove1 : MonoBehaviour
{
    public float speed; //�������� �����
    public Transform[] moveSpots; //������ �����
    private int startPoint; //��������� �����

    private Vector2 direction; //������, ����������� ����������� ��������� �����
    private int changeCheck; //�������� ��������� �� �����

    private void Start()
    {
        startPoint = 0; //������� ������ �����
        changeCheck = 0; //���������� ���� ����� ���
    }

    private void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, moveSpots[startPoint].position, speed * Time.deltaTime); //�������� �� ����� � ����� �����

        Vector2 direction = moveSpots[startPoint].position - moveSpots[startPoint + 1].position; //��������� ����������� � ��������� �����       

        if (Vector2.Distance(transform.position, moveSpots[startPoint].position) < 0.1f) //���� �����, ��������� �����
        {
            startPoint++; //����� �����
            changeCheck = 1; //��������� ����� �����
        }
        if (startPoint == moveSpots.Length - 1) //���� ����� ���, ����������� ������� (-1 ������ ��� "2" ����������� �����)
        {
            Destroy(gameObject);
            Debug.Log("���� �����!");
        }
        if (changeCheck == 1) //���� ��������� ����� �����, ������� ��� ������ ������� �������
        {
            if (direction.y > 0) //�������� ����
            {
                transform.rotation = Quaternion.Euler(0f, 0f, 180f);
                //Debug.Log("������� - 1");
            }
            else if (direction.x < 0) //�������� ������
            {
                transform.rotation = Quaternion.Euler(0f, 0f, -90f);
                //Debug.Log("������� - 2");
            }
            else if (direction.x > 0) //�������� �����
            {
                transform.rotation = Quaternion.Euler(0f, 0f, 90f);
                //Debug.Log("������� - 3");
            }
            else //�������� �����
            {
                transform.rotation = Quaternion.Euler(0f, 0f, 0f);
                //Debug.Log("������� - 4");
            }
            changeCheck = 0; //����� ��������
        }
    }
}
