using Unity.VisualScripting;
using UnityEngine;
/*using System.Collections;
using System.Collections.Generic;

public class Loader : MonoBehaviour
{
    public GameObject manager;

    private void Awake()
    {
        if (Manager.instance == null)
        {
            Instantiate(manager);
        }
    }
}*/
public class Loader<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T instance; //сначала Manager на сцене нет

    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<T>();
            }
            else if (instance != FindObjectOfType<T>())
            {
                Destroy(FindObjectOfType<T>());
            }
            DontDestroyOnLoad(FindObjectOfType<T>());
            return instance;
        }
    }
}
