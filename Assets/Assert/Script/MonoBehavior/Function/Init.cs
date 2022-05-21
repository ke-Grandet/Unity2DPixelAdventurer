using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Init : MonoBehaviour
{

    [Header("��Ϸ������")]
    public GameController gameControllerPrefab;
    [Header("���ֿ�����")]
    public AudioController audioControllerPrefab;

    // Start is called before the first frame update
    void Awake()
    {
        if (GameController.Instance == null)
        {
            Instantiate(gameControllerPrefab);
        }
        if (AudioController.Instance == null)
        {
            Instantiate(audioControllerPrefab);
        }
    }

}
