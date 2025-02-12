using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Player Stat")]
    [SerializeField] public int currentHealth;
    [SerializeField] private float maxHealth = 100f;

    [Header("State")]
    public PlayerState currentPlayerState;  // �÷��̾� �ӵ� ���� ����
    public WeaponType mainWeaponType;       // ���� ����źâ
    public WeaponType subWeaponType;        // ���� ���� źâ

    public enum PlayerState { Idle, Move, Shoot }
    public enum WeaponType { Bomb, Water, Getling }
    public bool isDie { get; set; }

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
}
