using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBattleManager : MonoBehaviour
{
    [Header("Battle key Bindings")]
    [SerializeField] private KeyCode attackKey;  // ����Ű
    [SerializeField] private KeyCode changeKey;  // źâ ����Ű

    [Header("ReLoading")]
    [SerializeField] int loadingCheakTime = 3;
    private float currentCheakTime;
    private bool isLoadable;

    // component
    private Weapon gun;

    /// <summary>
    /// ������ üũ �� �����ΰ� �װ� üũ�ϴ°� �����ߴ�. ��ȹ�� �̵� �ѹ� �� �ٽ� �о����
    /// </summary>

    private void Start()
    {
        gun = GetComponent<Weapon>();
    }

    private void Update()
    {
        WeaponLoading();
        AttackCall();
    }

    // źâ ����
    private void WeaponLoading()
    {
        if (Input.GetKeyDown(changeKey))
        {
            Player.WeaponType mainWeapon = Player.Instance.mainWeaponType;
            Player.Instance.mainWeaponType = Player.Instance.subWeaponType;
            Player.Instance.subWeaponType = mainWeapon;
        }
    }

    // ���� ���
    private void AttackCall()
    {
        if (Input.GetKey(attackKey)) 
        {
            switch (Player.Instance.mainWeaponType)
            {
                case Player.WeaponType.Bomb:
                    gun.Attack();
                    break;
                case Player.WeaponType.Water:
                    // ���� Ŭ������ ���� ȣ��(��������)
                    break;
                case Player.WeaponType.Getling:
                    // ���� Ŭ������ ���� ȣ��(��������)
                    break;
                default:
                    break;
            }
        };
    }

    // ������ ���
    private void LoadingCall(string loadingType)
    {
        switch (loadingType)
        {
            case "��ź������":
                if (Player.Instance.mainWeaponType != Player.WeaponType.Bomb)
                {
                    // ����źâ(����ź��)�� 0���� �ʱ�ȭ�ϰ�
                    Player.Instance.mainWeaponType = Player.WeaponType.Bomb;
                    // ��ź�� ���� ȣ��
                }
                // ��ź ���� Ŭ������ ���� ȣ��
                break;
            case "���ѿ�����":
                if (Player.Instance.mainWeaponType != Player.WeaponType.Water)
                {
                    // ����źâ(����ź��)�� 0���� �ʱ�ȭ�ϰ�
                    Player.Instance.mainWeaponType = Player.WeaponType.Water;
                    // ������ ���� ȣ��
                }
                // ���� ���� Ŭ������ ���� ȣ��
                break;
            case "��Ʋ��������":
                if (Player.Instance.mainWeaponType != Player.WeaponType.Getling)
                {
                    // ����źâ(����ź��)�� 0���� �ʱ�ȭ�ϰ�
                    Player.Instance.mainWeaponType = Player.WeaponType.Getling;
                    // ��Ʋ���� ���� ȣ��
                }
                // ��Ʋ�� ���� Ŭ������ ���� ȣ��           
                break;
            default:
                Debug.Log("������� �ȵǾ��");
                break;
        }

        switch (Player.Instance.mainWeaponType)
        {
            case Player.WeaponType.Bomb:
                // ���� Ŭ������ ������ ȣ��
                break;
            case Player.WeaponType.Water:
                // ���� Ŭ������ ������ ȣ��
                break;
            case Player.WeaponType.Getling:
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
        if(collision.gameObject.tag == "��ź������" ||       // TODO : ���Ķ�
            collision.gameObject.tag == "���ѿ�����" ||
            collision.gameObject.tag == "��Ʋ��������")
        {
            currentCheakTime += Time.deltaTime;
            if (currentCheakTime >= loadingCheakTime)
            {
                if (Player.Instance.currentPlayerState == Player.PlayerState.Idle)
                {
                    isLoadable = true;
                    
                    LoadingCall(collision.gameObject.tag);
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
