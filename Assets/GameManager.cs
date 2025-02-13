using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int score;
    public int bestScore;

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
    }

    // �� ai�� ������ ȣ�� (��ȹ�ں��� ����ϰ���� ���� ����� ���� �մ��ؼ� �ϴ� �̰� �Ⱦ�)
    public void ScoreUp(int n)
    {
        score += n;
        UIManager.Instance.UpdateScore(score);
    }
}
