using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Entrance : MonoBehaviour
{

    [Header("��һ�ؿ�")]
    public string nextLevelName = "Level1";

    // Start is called before the first frame update
    void Start()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GameController.Instance.StartLevel(nextLevelName);
            // ����Э��
            //StartCoroutine(LoadYourAsyncScene()); 
        }
    }

    IEnumerator LoadYourAsyncScene()
    {
        string levelName = "Level2";
        // ��ȡ��ǰ�������Ա�֮��ж��
        Scene currentScene = SceneManager.GetActiveScene();
        // ���õ�ǰ�������AudioListener
        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<AudioListener>().enabled = false;
        GameObject.Find("EventSystem").SetActive(false);
        // �ں�̨�����³���
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(levelName, LoadSceneMode.Additive);
        // ֱ���������ǰ�ȷ���null
        while (!asyncOperation.isDone)
        {
            yield return null;
        }
        // �ƶ����嵽�³���
        SceneManager.MoveGameObjectToScene(gameObject, SceneManager.GetSceneByName(levelName));
        // ж�ص�ǰ����
        SceneManager.UnloadSceneAsync(currentScene);
    }

}
