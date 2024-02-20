using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damn : MonoBehaviour
{
    public int target = 0; //на какой точке враг
    public Transform exit; //точка выхода
    public Transform[] wayPoints; //массив точек
    public float navigation; //как просчитывается перемещение нашего персонажа

    private Transform enemy; //считывание противника
    private float navigationTime = 0; //обновление положения в пространстве

    private void Start()
    {
        enemy = GetComponent<Transform>();
    }

    private void Update()
    {
        if (wayPoints != null) //есть ли ещё точки
        {
            Debug.Log("lol - 1");
            navigationTime += Time.deltaTime; //движение к следующей точке
            if (navigationTime > navigation) //
            {
                Debug.Log("lol - 2");
                if (target < wayPoints.Length) //цель меньше, чем кол-во точек
                {
                    Debug.Log("lol - 3");
                    enemy.position = Vector2.MoveTowards(enemy.position, wayPoints[target].position, navigationTime); //перемещение 
                }
                else
                {
                    Debug.Log("lol - 4");
                    enemy.position = Vector2.MoveTowards(enemy.position, exit.position, navigationTime); //идём к точке выхода
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
            Manager.Instance.removeEnemyFromScreen(); //до этого было instance
            Debug.Log("Lol - 6");
            Destroy(gameObject);
        }
    }
}
