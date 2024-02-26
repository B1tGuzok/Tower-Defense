/*using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Enemy : MonoBehaviour
{
    private int startPoint; //��������� �����

    [SerializeField]
    public Transform[] moveSpots; //������ �����
    [SerializeField]
    public Transform exit; //����� ������

    [SerializeField]
    float speed; //�������� �����

    private Vector2 direction; //������, ����������� ����������� ��������� �����
    private int changeCheck; //�������� ��������� �� �����

    private void Start()
    {
        startPoint = 0;
        changeCheck = 0; //���������� ���� ����� ���
    }

    private void Update()
    {

        if (startPoint < moveSpots.Length) //���� ������, ��� ���-�� �����
        {
            transform.position = Vector2.MoveTowards(transform.position, moveSpots[startPoint].position, speed * Time.deltaTime); //�������� �� ����� � ����� �����
            if (startPoint + 1 != moveSpots.Length) //��� �������� �������
            {
                direction = moveSpots[startPoint].position - moveSpots[startPoint + 1].position;
            } else
            {
                direction = moveSpots[startPoint].position - exit.position;
            }
        }
        else if (startPoint == moveSpots.Length) //��� � ������
        {
            transform.position = Vector2.MoveTowards(transform.position, exit.position, speed * Time.deltaTime);
        }

        if (startPoint != moveSpots.Length) //����� �������� ������ ������ �� ������� �������
        {
            if (Vector2.Distance(transform.position, moveSpots[startPoint].position) < 0.1f) //���� �����, ��������� �����
            {
                startPoint++;
                changeCheck = 1; //��������� ����� �����
            }
        }
        if (changeCheck == 1) //���� ��������� ����� �����, ������� ��� ������ ������� �������
        {
            if (direction.y > 0) //�������� ����
            {
                transform.rotation = Quaternion.Euler(0f, 0f, 180f);
            }
            else if (direction.x < 0) //�������� ������
            {
                transform.rotation = Quaternion.Euler(0f, 0f, -90f);
            }
            else if (direction.x > 0) //�������� �����
            {
                transform.rotation = Quaternion.Euler(0f, 0f, 90f);
            }
            else //�������� �����
            {
                transform.rotation = Quaternion.Euler(0f, 0f, 0f);
            }
            changeCheck = 0; //����� ��������
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Finish")
        {
            Manager.Instance.removeEnemyFromScreen(); //������ ���� instance
            Destroy(gameObject);
        }
    }
}*/
