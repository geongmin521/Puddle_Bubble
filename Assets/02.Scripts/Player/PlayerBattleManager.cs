using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBattleManager : MonoBehaviour
{
    [Header("Battle key Bindings")]
    [SerializeField] private KeyCode attackKey;  // ����Ű
    [SerializeField] private KeyCode loadingKey; // źâ ����Ű

    private void Update()
    {
        WeaponLoading();
        BattleInPut();
    }

    // źâ ��ȯ
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
        if (Input.GetKey(loadingKey)) 
        {
            switch (Player.Instance.mainWeaponType)
            {
                case Player.WeaponType.Gun:
                    // ���� Ŭ������ ���� ȣ��
                    break;
                case Player.WeaponType.Getling:
                    // ���� Ŭ������ ���� ȣ��
                    break;
                case Player.WeaponType.Sniper:
                    // ���� Ŭ������ ���� ȣ��
                    break;
                default:
                    break;
            }
        };


    }
}
