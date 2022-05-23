using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{

    private static T instance;

    public static T Instance
    {
        get 
        {
            if (instance == null)
            {
                GameObject obj = new();
                // 设置对象的名字为脚本名
                obj.name = typeof(T).Name;
                DontDestroyOnLoad(obj);
                instance = obj.AddComponent<T>();
            }
            return instance; 
        }
    }

}
