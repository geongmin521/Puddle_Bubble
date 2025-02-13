using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int score;
    public int bestScore;
    [SerializeField] private bool isGameOver;

    public static GameManager instance { get; private set; }
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    private void Start()
    {
        SoundManager.Instance.FadeInBGM();
        UIManager.Instance.FadeIn();
    }

    public void GameOver()
    {
        isGameOver = true;
        SoundManager.Instance.StopBGM();
        SoundManager.Instance.PlaySFX("SFX_GameOver");
    }

    public void ReStart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // �� ai�� ������ ȣ�� (��ȹ�ں��� ����ϰ���� ���� ����� ���� �մ��ؼ� �ϴ� �̰� �Ⱦ�)
    public void ScoreUp(int n)
    {
        score += n;
        UIManager.Instance.UpdateScore(score);
    }
}
