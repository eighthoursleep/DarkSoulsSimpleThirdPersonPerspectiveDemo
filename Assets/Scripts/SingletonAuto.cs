using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingletonAuto<T> : MonoBehaviour where T:MonoBehaviour
{
    private static T instance;

    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject gameObject = new GameObject();
                gameObject.name = typeof(T).ToString();
                instance = gameObject.AddComponent<T>();
                DontDestroyOnLoad(gameObject);
            }
            return instance;
        }
    }
}
