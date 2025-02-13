using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [Header("WeaponType")]
    [SerializeField] public Player.WeaponType weaponType;

    [Header("Weapon Stats")]
    public float damage;             // �⺻ ������(�Ѿ˿� ���ϰ���)
    public int ammoPerShot;          // ź�� �Ҹ�
    public int currentAmmo;          // �ܿ� ź�� ��
    public int maxAmmo;              // �ִ� ź�� ��
    public float attackCoolTime;     // �߻� �ֱ�
    public int ramainSpeed = 1;        // ���� �ӵ�

    [Header("Assets")]
    public GameObject bulletPrefab;      // �Ѿ� ������
    public Camera mainCamera;
    public Transform playerTrans;        // �Ѿ� ���� ��ġ
    //public AudioClip fireSFX;          // �߻� ����

    private Vector3 mousePos;            // ���콺 ��ġ
    private float lastAttackTime = 0f;   // ������ ���� �ð�

    private void Start()
    {
        ramainSpeed = 1;
    }
    // ����
    public virtual void Attack()
    {
        // ��Ÿ�� ����
        if (Time.time - lastAttackTime < attackCoolTime)
            return;

        // ź��üũ �� �Ѿ� ����
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

    // ����
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

    // źâ����
    public void InitAmmo()
    {
        currentAmmo = 0;
        PlayerUIManager.Instance.UpdateMainAmmoUI(currentAmmo, maxAmmo);
    }
}
