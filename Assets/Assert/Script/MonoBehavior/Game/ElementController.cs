using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ElementController : MonoBehaviour
{

    [Header("空元素Sprite")]
    public Sprite defaultSprite;
    [Header("冰元素Sprite")]
    public Sprite quasSprite;
    [Header("雷元素Sprite")]
    public Sprite wexSprite;
    [Header("火元素Sprite")]
    public Sprite exortSprite;
    [Header("UI元素位置1")]
    public Image image1;
    [Header("UI元素位置2")]
    public Image image2;
    [Header("UI元素位置3")]
    public Image image3;
    [Header("提示信息")]
    public TextMeshProUGUI tipText;

    public static ElementController Instance;

    private readonly Queue<ElementEnum> elementQueue = new();

    private const int elementCount = 3;

    // Start is called before the first frame update
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(Instance);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            Debug.Log("释放法术");
            Release();
        }
    }

    public void GainElement(ElementEnum element)
    {
        // 获得元素，最多三个，先进先出
        image1.sprite = image2.sprite;
        image2.sprite = image3.sprite;
        image3.sprite = element switch
        {
            ElementEnum.QUAS => quasSprite,
            ElementEnum.WEX => wexSprite,
            ElementEnum.EXORT => exortSprite,
            _ => defaultSprite,
        };
        elementQueue.Enqueue(element);
        if (elementQueue.Count > elementCount)
        {
            elementQueue.Dequeue();
        }
        // 显示或隐藏提示信息
        Debug.Log("当前元素数量：" + elementQueue.Count);
        tipText.gameObject.SetActive(elementQueue.Count == elementCount);
    }

    // 释放技能
    public void Release()
    {
        if (elementQueue.Count < elementCount)
        {
            Debug.Log("没有足够的元素");
            return;
        }

        int spell = 0;
        foreach (ElementEnum element in elementQueue)
        {
            spell += (int)element;
        }
        switch (spell)
        {
            // 急速冷却 Y
            case 3:
                Debug.Log("塞卓昂的无尽战栗！");
                break;
            // 幽灵漫步 V
            case 4:
                Debug.Log("米瑞特之阻碍！");
                break;
            // 寒冰之墙 G
            case 7:
                Debug.Log("科瑞克斯的杀戮之墙！");
                break;
            // 强袭飓风 X
            case 5:
                Debug.Log("托纳鲁斯之爪！");
                break;
            // 超震声波 B
            case 8:
                Debug.Log("布鲁冯特之无力声波！");
                break;
            // 熔炉精灵 F
            case 11:
                Debug.Log("卡尔维因的至邪造物！");
                break;
            // 电磁脉冲 C
            case 6:
                Debug.Log("恩多利昂的恶之混动！");
                break;
            // 灵动迅捷 Z
            case 9:
                Debug.Log("盖斯特的猛战号令！");
                break;
            // 混沌陨石 D
            case 12:
                Debug.Log("塔拉克的天坠之火！");
                break;
            // 阳炎冲击 T
            case 15:
                Debug.Log("哈雷克之火葬魔咒！");
                break;
            default:
                Debug.Log("倒背如流的咒语");
                break;
        }
        elementQueue.Clear();
        image1.sprite = defaultSprite;
        image2.sprite = defaultSprite;
        image3.sprite = defaultSprite;
        tipText.gameObject.SetActive(false);
    }

}
