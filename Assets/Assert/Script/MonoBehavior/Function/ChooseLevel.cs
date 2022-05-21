using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChooseLevel : MonoBehaviour
{

    public GameObject scrollView;

    public void ShowChooseLevelView()
    {
        scrollView.SetActive(true);
    }

    public void CloseChooseLevelView()
    {
        scrollView?.SetActive(false);
    }

}
