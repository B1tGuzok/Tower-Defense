using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove1 : MonoBehaviour
{
    public float speed; //скорость врага
    public Transform[] moveSpots; //массив точек
    private int startPoint; //стартова€ точка

    private Vector2 direction; //вектор, вычисл€ющий направление следующей точки
    private int changeCheck; //проверка сменилась ли точка

    private void Start()
    {
        startPoint = 0; //задание первой точки
        changeCheck = 0; //изначально смен точек нет
    }

    private void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, moveSpots[startPoint].position, speed * Time.deltaTime); //движение от точки к новой точке

        Vector2 direction = moveSpots[startPoint].position - moveSpots[startPoint + 1].position; //вычисл€ем направление к следующей точке       

        if (Vector2.Distance(transform.position, moveSpots[startPoint].position) < 0.1f) //если дошЄл, следующа€ точка
        {
            startPoint++; //смена точки
            changeCheck = 1; //произошла смена точки
        }
        if (startPoint == moveSpots.Length - 1) //если точек нет, уничтожение объекта (-1 потому что "2" завершающие точки)
        {
            Destroy(gameObject);
            Debug.Log("¬раг дошЄл!");
        }
        if (changeCheck == 1) //если произошла смена точки, смотрим как мен€ть поворот объекта
        {
            if (direction.y > 0) //движение вниз
            {
                transform.rotation = Quaternion.Euler(0f, 0f, 180f);
                //Debug.Log("”словие - 1");
            }
            else if (direction.x < 0) //движение вправо
            {
                transform.rotation = Quaternion.Euler(0f, 0f, -90f);
                //Debug.Log("”словие - 2");
            }
            else if (direction.x > 0) //движение влево
            {
                transform.rotation = Quaternion.Euler(0f, 0f, 90f);
                //Debug.Log("”словие - 3");
            }
            else //движение вверх
            {
                transform.rotation = Quaternion.Euler(0f, 0f, 0f);
                //Debug.Log("”словие - 4");
            }
            changeCheck = 0; //сброс проверки
        }
    }
}
