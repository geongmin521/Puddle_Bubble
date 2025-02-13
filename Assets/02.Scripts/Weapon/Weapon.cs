using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [Header("WeaponType")]
    [SerializeField] public Player.WeaponType weaponType;

    [Header("Weapon Stats")]
    public float damage;             // 기본 데미지(총알에 붙일거임)
    public int ammoPerShot;          // 탄약 소모량
    public int currentAmmo;          // 잔여 탄약 수
    public int maxAmmo;              // 최대 탄약 수
    public float attackCoolTime;     // 발사 주기
    public int ramainSpeed = 1;        // 장전 속도

    [Header("Assets")]
    public GameObject bulletPrefab;      // 총알 프리팹
    public Camera mainCamera;
    public Transform playerTrans;        // 총알 생성 위치
    //public AudioClip fireSFX;          // 발사 사운드

    private Vector3 mousePos;            // 마우스 위치
    private float lastAttackTime = 0f;   // 마지막 공격 시간

    private void Start()
    {
        ramainSpeed = 1;
    }
    // 공격
    public virtual void Attack()
    {
        // 쿨타임 제어
        if (Time.time - lastAttackTime < attackCoolTime)
            return;

        // 탄약체크 후 총알 생성
        if (currentAmmo >= ammoPerShot)
        {
            currentAmmo -= ammoPerShot;

            mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0f;
            Vector2 shootDirection = ((Vector2)mousePos - (Vector2)playerTrans.position).normalized;

            GameObject bullet = Instantiate(bulletPrefab, playerTrans.position, Quaternion.identity);
            if(Player.Instance.mainWeaponType == Player.WeaponType.Bomb)
            {
                bullet.GetComponent<BombBullet>().moveVec = shootDirection;
                SoundManager.Instance.PlaySFX("SFX_Bomb");
            }
            else if(Player.Instance.mainWeaponType == Player.WeaponType.Water)
            {
                bullet.GetComponent<WaterBullet>().moveVec = shootDirection;
                SoundManager.Instance.PlaySFX("SFX_Water");
            }
            else
            {
                bullet.GetComponent<GatlingBullet>().moveVec = shootDirection;
                SoundManager.Instance.PlaySFX("SFX_Getling");
            }
            
            lastAttackTime = Time.time;
        }
        else
        {
            SoundManager.Instance.PlaySFX("SFX_NonAmmo");
        }
        PlayerUIManager.Instance.UpdateMainAmmoUI(currentAmmo,maxAmmo);
    }

    // 장전
    public void Loading(PubbleContorl pubble)
    {
        StartCoroutine(LoadingCo(pubble));
        SoundManager.Instance.PlaySFX("SFX_Loading");
    }

    IEnumerator LoadingCo(PubbleContorl pubble)
    {
        if (pubble.curBubble >= ramainSpeed)         
        {
            if (currentAmmo <= maxAmmo)
            {
                pubble.curBubble -= ramainSpeed;
                currentAmmo += ramainSpeed;
                PlayerUIManager.Instance.UpdateMainAmmoUI(currentAmmo, maxAmmo);
            }
        } 
        yield return new WaitForSeconds(0.05f);
        if (Player.Instance.isLoading == true)
            StartCoroutine(LoadingCo(pubble));
    }

    // 탄창버림
    public void InitAmmo()
    {
        currentAmmo = 0;
        PlayerUIManager.Instance.UpdateMainAmmoUI(currentAmmo, maxAmmo);
    }
}
