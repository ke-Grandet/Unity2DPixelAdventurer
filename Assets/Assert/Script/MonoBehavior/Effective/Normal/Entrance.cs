using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Entrance : MonoBehaviour
{

    [Header("下一关卡")]
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
            // 开启协程
            //StartCoroutine(LoadYourAsyncScene()); 
        }
    }

    IEnumerator LoadYourAsyncScene()
    {
        string levelName = "Level2";
        // 获取当前场景，以便之后卸载
        Scene currentScene = SceneManager.GetActiveScene();
        // 禁用当前摄像机的AudioListener
        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<AudioListener>().enabled = false;
        GameObject.Find("EventSystem").SetActive(false);
        // 在后台加载新场景
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(levelName, LoadSceneMode.Additive);
        // 直到方法完成前先返回null
        while (!asyncOperation.isDone)
        {
            yield return null;
        }
        // 移动物体到新场景
        SceneManager.MoveGameObjectToScene(gameObject, SceneManager.GetSceneByName(levelName));
        // 卸载当前场景
        SceneManager.UnloadSceneAsync(currentScene);
    }

}
