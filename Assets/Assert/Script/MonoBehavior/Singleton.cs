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
                // ���ö��������Ϊ�ű���
                obj.name = typeof(T).Name;
                DontDestroyOnLoad(obj);
                instance = obj.AddComponent<T>();
            }
            return instance; 
        }
    }

}
