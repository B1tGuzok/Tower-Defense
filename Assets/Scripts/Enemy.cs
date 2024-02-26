/*using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Enemy : MonoBehaviour
{
    private int startPoint; //стартовая точка

    [SerializeField]
    public Transform[] moveSpots; //массив точек
    [SerializeField]
    public Transform exit; //точка выхода

    [SerializeField]
    float speed; //скорость врага

    private Vector2 direction; //вектор, вычисляющий направление следующей точки
    private int changeCheck; //проверка сменилась ли точка

    private void Start()
    {
        startPoint = 0;
        changeCheck = 0; //изначально смен точек нет
    }

    private void Update()
    {

        if (startPoint < moveSpots.Length) //цель меньше, чем кол-во точек
        {
            transform.position = Vector2.MoveTowards(transform.position, moveSpots[startPoint].position, speed * Time.deltaTime); //движение от точки к новой точке
            if (startPoint + 1 != moveSpots.Length) //для поворота спрайта
            {
                direction = moveSpots[startPoint].position - moveSpots[startPoint + 1].position;
            } else
            {
                direction = moveSpots[startPoint].position - exit.position;
            }
        }
        else if (startPoint == moveSpots.Length) //идём к выходу
        {
            transform.position = Vector2.MoveTowards(transform.position, exit.position, speed * Time.deltaTime);
        }

        if (startPoint != moveSpots.Length) //чтобы избежать ошибки выхода за пределы массива
        {
            if (Vector2.Distance(transform.position, moveSpots[startPoint].position) < 0.1f) //если дошёл, следующая точка
            {
                startPoint++;
                changeCheck = 1; //произошла смена точки
            }
        }
        if (changeCheck == 1) //если произошла смена точки, смотрим как менять поворот объекта
        {
            if (direction.y > 0) //движение вниз
            {
                transform.rotation = Quaternion.Euler(0f, 0f, 180f);
            }
            else if (direction.x < 0) //движение вправо
            {
                transform.rotation = Quaternion.Euler(0f, 0f, -90f);
            }
            else if (direction.x > 0) //движение влево
            {
                transform.rotation = Quaternion.Euler(0f, 0f, 90f);
            }
            else //движение вверх
            {
                transform.rotation = Quaternion.Euler(0f, 0f, 0f);
            }
            changeCheck = 0; //сброс проверки
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Finish")
        {
            Manager.Instance.removeEnemyFromScreen(); //раньше было instance
            Destroy(gameObject);
        }
    }
}*/
