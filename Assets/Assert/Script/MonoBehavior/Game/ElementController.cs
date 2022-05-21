using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ElementController : MonoBehaviour
{

    [Header("��Ԫ��Sprite")]
    public Sprite defaultSprite;
    [Header("��Ԫ��Sprite")]
    public Sprite quasSprite;
    [Header("��Ԫ��Sprite")]
    public Sprite wexSprite;
    [Header("��Ԫ��Sprite")]
    public Sprite exortSprite;
    [Header("UIԪ��λ��1")]
    public Image image1;
    [Header("UIԪ��λ��2")]
    public Image image2;
    [Header("UIԪ��λ��3")]
    public Image image3;
    [Header("��ʾ��Ϣ")]
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
            Debug.Log("�ͷŷ���");
            Release();
        }
    }

    public void GainElement(ElementEnum element)
    {
        // ���Ԫ�أ�����������Ƚ��ȳ�
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
        // ��ʾ��������ʾ��Ϣ
        Debug.Log("��ǰԪ��������" + elementQueue.Count);
        tipText.gameObject.SetActive(elementQueue.Count == elementCount);
    }

    // �ͷż���
    public void Release()
    {
        if (elementQueue.Count < elementCount)
        {
            Debug.Log("û���㹻��Ԫ��");
            return;
        }

        int spell = 0;
        foreach (ElementEnum element in elementQueue)
        {
            spell += (int)element;
        }
        switch (spell)
        {
            // ������ȴ Y
            case 3:
                Debug.Log("��׿�����޾�ս����");
                break;
            // �������� V
            case 4:
                Debug.Log("������֮�谭��");
                break;
            // ����֮ǽ G
            case 7:
                Debug.Log("�����˹��ɱ¾֮ǽ��");
                break;
            // ǿϮ쫷� X
            case 5:
                Debug.Log("����³˹֮צ��");
                break;
            // �������� B
            case 8:
                Debug.Log("��³����֮����������");
                break;
            // ��¯���� F
            case 11:
                Debug.Log("����ά�����а���");
                break;
            // ������� C
            case 6:
                Debug.Log("���������Ķ�֮�춯��");
                break;
            // �鶯Ѹ�� Z
            case 9:
                Debug.Log("��˹�ص���ս���");
                break;
            // ������ʯ D
            case 12:
                Debug.Log("�����˵���׹֮��");
                break;
            // ���׳�� T
            case 15:
                Debug.Log("���׿�֮����ħ�䣡");
                break;
            default:
                Debug.Log("��������������");
                break;
        }
        elementQueue.Clear();
        image1.sprite = defaultSprite;
        image2.sprite = defaultSprite;
        image3.sprite = defaultSprite;
        tipText.gameObject.SetActive(false);
    }

}
