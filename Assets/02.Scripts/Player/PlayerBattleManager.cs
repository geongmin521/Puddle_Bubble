using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBattleManager : MonoBehaviour
{
    [Header("Battle key Bindings")]
    [SerializeField] private KeyCode attackKey;  // ����Ű
    [SerializeField] private KeyCode loadingKey; // źâ ����Ű

    [Header("ReLoading")]
    [SerializeField] int loadingCheakTime = 3;
    private float currentCheakTime;
    private bool isLoadable;

    // component
    private Gun gun;

    /// <summary>
    /// ������ üũ �� �����ΰ� �װ� üũ�ϴ°� �����ߴ�. ��ȹ�� �̵� �ѹ� �� �ٽ� �о����
    /// </summary>

    private void Start()
    {
        gun = GetComponent<Gun>();
    }

    private void Update()
    {
        WeaponLoading();
        BattleInPut();
    }

    // źâ ����
    private void WeaponLoading()
    {
        if (Input.GetKeyDown(loadingKey))
        {
            Player.WeaponType mainWeapon = Player.Instance.mainWeaponType;
            Player.Instance.mainWeaponType = Player.Instance.subWeaponType;
            Player.Instance.subWeaponType = mainWeapon;
        }
    }

    // ���� ���
    private void BattleInPut()
    {
        if (Input.GetKey(attackKey)) 
        {
            switch (Player.Instance.mainWeaponType)
            {
                case Player.WeaponType.Gun:
                    gun.Attack();
                    break;
                case Player.WeaponType.Getling:
                    // ���� Ŭ������ ���� ȣ��(��������)
                    break;
                case Player.WeaponType.Sniper:
                    // ���� Ŭ������ ���� ȣ��(��������)
                    break;
                default:
                    break;
            }
        };
    }

    // ������ ���
    private void ReLoading()
    {
        switch (Player.Instance.mainWeaponType)
        {
            case Player.WeaponType.Gun:
                // ���� Ŭ������ ������ ȣ��
                break;
            case Player.WeaponType.Getling:
                // ���� Ŭ������ ������ ȣ��
                break;
            case Player.WeaponType.Sniper:
                // ���� Ŭ������ ������ ȣ��
                break;
            default:
                break;
        }
    }

    // ������ ���� ���� üũ
    // �ӵ��� 0�̰� �������̿� loadingCheakTime �̻� �־��� ��
    // ���� � ������������ üũ�ϰ� źâ ��ȯ�ϴ°� �߰� �ʿ�
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "�������¹�������1" || collision.gameObject.tag == "�������¹�������2")
        {
            currentCheakTime += Time.deltaTime;
            if (currentCheakTime > loadingCheakTime)
            {
                if (Player.Instance.currentPlayerState == Player.PlayerState.Idle)
                {
                    isLoadable = true;
                    switch (collision.gameObject.tag)
                    {
                        case "dfs":
                            break;
                    }
                    ReLoading();
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        currentCheakTime = 0;
        isLoadable = false;
    }
}
