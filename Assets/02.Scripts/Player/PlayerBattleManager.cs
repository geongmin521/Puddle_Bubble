using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerBattleManager : MonoBehaviour
{
    [Header("Battle key Bindings")]
    [SerializeField] private KeyCode attackKey;  // 공격키

    [Header("ReLoading")]
    [SerializeField] int loadingCheakTime = 3;
    private float currentCheakTime;
    public bool isLoadable;
    public Animator ani;

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
        if (Player.Instance.isDie == true)
            return;
        if (EventSystem.current.IsPointerOverGameObject() == false)
            AttackCall();
    }

    // 탄창 변경
    public void WeaponSwitch()
    {
        // 장착무기 변경
        Player.WeaponType mainWeapon = Player.Instance.mainWeaponType;
        Player.Instance.mainWeaponType = Player.Instance.subWeaponType;
        Player.Instance.subWeaponType = mainWeapon;

        // UI
        switch (Player.Instance.mainWeaponType)
        {
            case Player.WeaponType.Bomb:
                PlayerUIManager.Instance.UpdateMainAmmoUI(bomb.currentAmmo, bomb.maxAmmo);
                PlayerUIManager.Instance.UpdateCurrentWeaponUI("폭탄");
                break;
            case Player.WeaponType.Water:
                PlayerUIManager.Instance.UpdateMainAmmoUI(water.currentAmmo, water.maxAmmo);
                PlayerUIManager.Instance.UpdateCurrentWeaponUI("대물총");
                break;
            case Player.WeaponType.Getling:
                PlayerUIManager.Instance.UpdateMainAmmoUI(getling.currentAmmo, getling.maxAmmo);
                PlayerUIManager.Instance.UpdateCurrentWeaponUI("게틀링");
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

    // 공격 명령
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

    // 재장전 명령 (탄창체크 -> 장전)
    private void LoadingCall(PubbleContorl pubble)
    {
        PubbleState state = pubble.state;
        switch (state)
        {
            case PubbleState.bomb: //물웅덩이의 상태인거같고//음 이거 생각이랑 많이 다른데? 
                if (Player.Instance.mainWeaponType != Player.WeaponType.Bomb) // 현재 메인무기의 종류에 따라 다르게 처리하는듯? 
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
                    PlayerUIManager.Instance.UpdateCurrentWeaponUI("폭탄");
                }
                bomb.Loading(pubble);
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
                    PlayerUIManager.Instance.UpdateCurrentWeaponUI("대물총");
                }
                water.Loading(pubble);
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
                    PlayerUIManager.Instance.UpdateCurrentWeaponUI("게틀링");
                }
                getling.Loading(pubble);
                break;
            default:
                Debug.Log("여기오면 안되어용");
                break;
        }
    }

    // 재장전 가능 여부 체크
    // 속도가 0이고 물웅덩이에 loadingCheakTime 이상 있었을 때
    // 이제 어떤 물웅덩이인지 체크하고 탄창 변환하는거 추가 필요
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Puddle"))
        {
            currentCheakTime += Time.deltaTime;
            if (currentCheakTime >= loadingCheakTime)
            {
                if (Player.Instance.currentPlayerState == Player.PlayerState.Idle)
                {
                    //isLoadable = true;  // 이거 안씀
                    Player.Instance.isLoading = true;
                    ani.SetBool("isLoading", true); ;
                    LoadingCall(collision.gameObject.GetComponent<PubbleContorl>());
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        currentCheakTime = 0;
        isLoadable = false; // 이거안씀
        Player.Instance.isLoading = false;
        ani.SetBool("isLoading", false);
    }
}
