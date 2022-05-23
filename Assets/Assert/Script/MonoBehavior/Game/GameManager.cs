using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("��ʾ���ı�")]
    public RectTransform tipSignPanel;
    [Header("���Ѫ��")]
    public RectTransform playerHealthFill;
    [Header("��������")]
    public RectTransform scorePanel;
    [Header("��ͣ��ť")]
    public RectTransform pauseButton;
    [Header("��ͣ�˵�")]
    public RectTransform pausePanel;

    private int score = 0;
    private Text scoreText;
    private float playerHealthFillWidth;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(Instance);
    }

    private void Start()
    {
        scoreText = scorePanel.gameObject.GetComponentInChildren<Text>();
        playerHealthFillWidth = playerHealthFill.rect.width;
    }

    public void GainScore(int score)
    {
        this.score += score;
        scoreText.text = this.score.ToString();
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;
        pausePanel.gameObject.SetActive(true);
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
        pausePanel.gameObject.SetActive(false);
    }

    public void MainMenu()
    {
        //score = 0;
        //Time.timeScale = 1f;
        //pauseButton.gameObject.SetActive(false);
        //pausePanel.gameObject.SetActive(false);
        // todo ��ת��������
        Destroy(Instance.gameObject);
        Instance = null;
        Time.timeScale = 1f;
        StartCoroutine(MainMenuCoro());
    }
    IEnumerator MainMenuCoro()
    {
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(0);
        while (!asyncOperation.isDone)
        {
            yield return null;
        }
    }

    // ������ʾ���ı�
    public void TipSignText(bool isActive)
    {
        tipSignPanel.gameObject.SetActive(isActive);
    }

    // �������Ѫ��
    public void UpdatePlayerHealthUI(float percent)
    {
        Debug.Log("ʣ������ֵ����:" + percent);
        playerHealthFill.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, percent * playerHealthFillWidth);
    }

}
