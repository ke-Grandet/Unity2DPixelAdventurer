using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("玩家血条")]
    public RectTransform playerHealthFill;
    [Header("分数界面")]
    public RectTransform scorePanel;
    [Header("暂停按钮")]
    public RectTransform pauseButton;
    [Header("暂停菜单")]
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
        DontDestroyOnLoad(this);
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

    public void StartGame()
    {
        // todo 主界面点击开始游戏
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
        score = 0;
        Time.timeScale = 1f;
        pauseButton.gameObject.SetActive(false);
        pausePanel.gameObject.SetActive(false);
        // todo 跳转到主界面
    }

    public void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    public void UpdatePlayerHealthUI(float percent)
    {
        Debug.Log("剩余生命值比例:" + percent);
        playerHealthFill.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, percent * playerHealthFillWidth);
    }

}
