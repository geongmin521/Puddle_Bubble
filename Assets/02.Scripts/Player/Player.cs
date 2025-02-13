using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Player Stat")]
    [SerializeField] public int hp = 5;
    [SerializeField] public int invincibilityTime = 2;  // ����Ÿ��

    [Header("State")]
    public PlayerState currentPlayerState;  // �÷��̾� �ӵ� ���� ����
    public WeaponType mainWeaponType;       // ���� ����źâ
    public WeaponType subWeaponType;        // ���� ���� źâ
    public SpriteRenderer sprite;
    public Animator ani;
    public enum PlayerState { Idle, Move, Shoot }
    public enum WeaponType { Bomb, Water, Getling }
    public bool isLoading { get; set; }
    public bool isDie { get; set; }
    private bool isDamageing;

    // �̱���
    public static Player Instance { get; private set; }
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // �ǰ�
    public void TakeDamage()
    {
        if (isDamageing) return; // ���� ����

        isDamageing = true;
        hp--;
        PlayerUIManager.Instance.HpUiIconDown();    // ������ UI
        SoundManager.Instance.PlaySFX("SFX_PlayerDamaged");
        StartCoroutine(motion());
        if (hp <= 0)
        {
            hp = 0;
            isDie = true;
            Die();
            return;
        }

        StartCoroutine(InvincibilityCoroutine());
    }

    private IEnumerator InvincibilityCoroutine()
    {
        yield return new WaitForSeconds(invincibilityTime);
        isDamageing = false;
    }
    private IEnumerator motion()
    {
        for (int i = 0; i < 10; i++)
        {
            sprite.color = Color.white;
            yield return new WaitForSeconds(0.1f);
            sprite.color = new Color(1, 1, 1, 0.5f);
            yield return new WaitForSeconds(0.1f);
        }
        sprite.color = Color.white;
    }

    // Die
    public void Die()
    {
        Debug.Log("���");
        ani.SetTrigger("Die");
        GameManager.instance.GameOver();
        UIManager.Instance.GameOverUI();
    }
}
