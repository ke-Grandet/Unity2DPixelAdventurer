using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Init : MonoBehaviour
{

    [Header("ÓÎÏ·¿ØÖÆÆ÷")]
    public GameController gameControllerPrefab;
    [Header("ÒôÀÖ¿ØÖÆÆ÷")]
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
