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
    public bool isLoadable;

    // component
    private Bomb bomb;
    private Water water;
    private Getling getling;


    private void Start()
    {
        bomb = GetComponent<Bomb>();
        water = GetComponent<Water>();
        getling = GetComponent<Getling>();
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
                    bomb.Attack();
                    break;
                case Player.WeaponType.Water:
                    water.Attack();
                    break;
                case Player.WeaponType.Getling:
                    getling.Attack();
                    break;
                default:
                    break;
            }
        };
    }

    // ������ ��� (źâüũ -> ����)
    private void LoadingCall(PubbleState state)
    {
        switch (state)
        {
            case PubbleState.bomb:
                if (Player.Instance.mainWeaponType != Player.WeaponType.Bomb)
                {
                    if(Player.Instance.mainWeaponType == Player.WeaponType.Water)
                    {
                        water.InitAmmo();
                    }
                    else
                    {
                        getling.InitAmmo();
                    }
                    Player.Instance.mainWeaponType = Player.WeaponType.Bomb;
                }
                bomb.Loading();
                break;
            case PubbleState.water:
                if (Player.Instance.mainWeaponType != Player.WeaponType.Water)
                {
                    if (Player.Instance.mainWeaponType == Player.WeaponType.Bomb)
                    {
                        bomb.InitAmmo();
                    }
                    else
                    {
                        getling.InitAmmo();
                    }
                    Player.Instance.mainWeaponType = Player.WeaponType.Water;
                }
                water.Loading();
                break;
            case PubbleState.gatling:
                if (Player.Instance.mainWeaponType != Player.WeaponType.Getling)
                {
                    if (Player.Instance.mainWeaponType == Player.WeaponType.Bomb)
                    {
                        bomb.InitAmmo();
                    }
                    else
                    {
                        water.InitAmmo();
                    }
                    Player.Instance.mainWeaponType = Player.WeaponType.Getling;
                }
                getling.Loading();
                break;
            default:
                Debug.Log("������� �ȵǾ��");
                break;
        }
    }

    // ������ ���� ���� üũ
    // �ӵ��� 0�̰� �������̿� loadingCheakTime �̻� �־��� ��
    // ���� � ������������ üũ�ϰ� źâ ��ȯ�ϴ°� �߰� �ʿ�
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Puddle"))
        {
            currentCheakTime += Time.deltaTime;
            if (currentCheakTime >= loadingCheakTime)
            {
                if (Player.Instance.currentPlayerState == Player.PlayerState.Idle)
                {
                    isLoadable = true;  // �̰� �Ⱦ�
                    Player.Instance.isLoading = true;
                    LoadingCall(collision.gameObject.GetComponent<PubbleContorl>().state);
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        currentCheakTime = 0;
        isLoadable = false; // �̰žȾ�
        Player.Instance.isLoading = false;
    }
}
