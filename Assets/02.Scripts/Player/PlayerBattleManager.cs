using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBattleManager : MonoBehaviour
{
    [Header("Battle key Bindings")]
    [SerializeField] private KeyCode attackKey;  // ����Ű

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
        AttackCall();
    }

    // źâ ����
    public void WeaponSwitch()
    {
        // �������� ����
        Player.WeaponType mainWeapon = Player.Instance.mainWeaponType;
        Player.Instance.mainWeaponType = Player.Instance.subWeaponType;
        Player.Instance.subWeaponType = mainWeapon;

        // UI
        switch (Player.Instance.mainWeaponType)
        {
            case Player.WeaponType.Bomb:
                PlayerUIManager.Instance.UpdateMainAmmoUI(bomb.currentAmmo, bomb.maxAmmo);
                PlayerUIManager.Instance.UpdateCurrentWeaponUI("��ź");
                break;
            case Player.WeaponType.Water:
                PlayerUIManager.Instance.UpdateMainAmmoUI(water.currentAmmo, water.maxAmmo);
                PlayerUIManager.Instance.UpdateCurrentWeaponUI("�빰��");
                break;
            case Player.WeaponType.Getling:
                PlayerUIManager.Instance.UpdateMainAmmoUI(getling.currentAmmo, getling.maxAmmo);
                PlayerUIManager.Instance.UpdateCurrentWeaponUI("��Ʋ��");
                break;
            default:
                break;
        }

        switch (Player.Instance.subWeaponType)
        {
            case Player.WeaponType.Bomb:
                PlayerUIManager.Instance.UpdateSubAmmoUI(bomb.currentAmmo, bomb.maxAmmo);
                break;
            case Player.WeaponType.Water:
                PlayerUIManager.Instance.UpdateSubAmmoUI(water.currentAmmo, water.maxAmmo);
                break;
            case Player.WeaponType.Getling:
                PlayerUIManager.Instance.UpdateSubAmmoUI(getling.currentAmmo, getling.maxAmmo);
                break;
            default:
                break;
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
                    PlayerUIManager.Instance.UpdateCurrentWeaponUI("��ź");
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
                    PlayerUIManager.Instance.UpdateCurrentWeaponUI("�빰��");
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
                    PlayerUIManager.Instance.UpdateCurrentWeaponUI("��Ʋ��");
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
