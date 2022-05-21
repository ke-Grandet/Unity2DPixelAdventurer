using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthMask : MonoBehaviour
{

    private RectTransform _rectTransform;

    private float originalWidth;

    // Start is called before the first frame update
    void Start()
    {
        _rectTransform = GetComponent<RectTransform>();
        originalWidth = _rectTransform.rect.width;
    }
    
    public void ChangeValue(float percent)
    {
        _rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, percent * originalWidth);
    }

}
