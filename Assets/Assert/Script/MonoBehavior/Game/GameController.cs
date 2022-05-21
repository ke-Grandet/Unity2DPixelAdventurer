using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameController : MonoBehaviour
{

    [Header("��������")]
    public GameObject gameScorePanelPrefab;  // ��Ϸ���������Ԥ�Ƽ�
    [Header("ʧ�ܽ���")]
    public GameObject gameOverPanelPrefab;  // ��Ϸʧ�ܽ����Ԥ�Ƽ�
    [Header("ʤ������")]
    public GameObject gameVictoryPanelPrefab;  // ��Ϸʤ�������Ԥ�Ƽ�

    public static GameController Instance;

    private GameObject gameScorePanel;  // ��Ϸ��������
    private GameObject gameOverPanel;  // ��Ϸʧ�ܽ���
    private GameObject gameVictoryPanel;  // ��Ϸʤ������

    public GameObject GameScorePanel  // ��Ϸ��������ķ�����
    {
        get
        {
            if (gameScorePanel == null)
            {
                gameScorePanel = Instantiate(gameScorePanelPrefab, Instance.transform);
            }
            return gameScorePanel;
        }
    }
    public GameObject GameOverPanel  // ��Ϸʧ�ܽ���ķ�����
    {
        get
        {
            if (gameOverPanel == null)
            {
                gameOverPanel = Instantiate(gameOverPanelPrefab, Instance.transform);
            }
            return gameOverPanel;
        }
    }
    public GameObject GameVictoryPanel  // ��Ϸʤ������ķ�����
    {
        get
        {
            if (gameVictoryPanel == null)
            {
                gameVictoryPanel = Instantiate(gameVictoryPanelPrefab, Instance.transform);
            }
            return gameVictoryPanel;
        }
    }

    private int score = 0;  // ����
    private int finalScore = 0;  // ���շ���

    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(GameController.Instance);
    }

    // ��÷���
    public void GainScore(int value)
    {
        score += value;
        Instance.GameScorePanel.GetComponentInChildren<Text>().text = Instance.score.ToString();
    }

    // ��ʾ��Ϸʧ�ܽ���
    public void ShowGameOverPanel()
    {
        Instance.GameOverPanel.SetActive(true);
    }

    // ��ʾ��Ϸʤ������
    public void ShowGameVictoryPanel()
    {
        Instance.GameVictoryPanel.GetComponentInChildren<TextMeshProUGUI>().text = "Score: " + score;
        Instance.GameVictoryPanel.SetActive(true);
    }

    // ����ؿ�����ص�������
    public void StartLevel(string levelName)
    {
        // �����������
        if (levelName.Equals("Welcome"))
        {
            // �������㣬���ط�������
            Instance.finalScore = 0;
            Instance.score = 0;
            Instance.GainScore(0);
            Instance.GameScorePanel.SetActive(false);
            // �л�ΪĬ������
            AudioController.Instance.PlayDefaultMusic();
        }
        // ����ǹؿ�
        else
        {
            // ����ˢ��
            Instance.finalScore = Instance.score;
            Instance.GainScore(0);
            // �����BOSS��
            if (levelName.Equals("Level3"))
            {
                // ���ط�������
                Instance.GameScorePanel.SetActive(false);
                // �л�ΪBOSS����
                AudioController.Instance.PlayBossMusic();
            }
            // ����������ؿ�
            else
            {
                // ��ʾ��������
                Instance.GameScorePanel.SetActive(true);
                // ����Ĭ������
                AudioController.Instance.PlayDefaultMusic();
                // �л�ΪĬ������
                AudioController.Instance.PlayDefaultMusic();
            }
        }
        // ����ʤ�������ʧ�ܽ���
        Instance.GameOverPanel.SetActive(false);
        Instance.GameVictoryPanel.SetActive(false);

        SceneManager.LoadScene(levelName);
    }

    // ������ǰ�ؿ�
    public void RestartLevel()
    {
        // ����ʤ�������ʧ�ܽ���
        Instance.GameOverPanel.SetActive(false);
        Instance.GameVictoryPanel.SetActive(false);
        // ���÷���
        Instance.score = Instance.finalScore;
        Instance.GainScore(0);

        string levelName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(levelName);
    }

    // ������Ϸ
    public void EndGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

}