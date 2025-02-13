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
    public int remainAmmo;           // �ܿ� ź�� ��
    public int maxAmmo;              // �ִ� ź�� ��
    public float attackCoolTime;     // �߻� �ֱ�

    [Header("Assets")]
    public GameObject bulletPrefab;      // �Ѿ� ������
    public Camera mainCamera;
    public Transform playerTrans;        // �Ѿ� ���� ��ġ
    //public AudioClip fireSFX;          // �߻� ����

    private Vector3 mousePos;            // ���콺 ��ġ
    private float lastAttackTime = 0f;   // ������ ���� �ð�

    // ����
    public virtual void Attack()
    {
        // ��Ÿ�� ����
        if (Time.time - lastAttackTime < attackCoolTime)
            return;

        // ź��üũ �� �Ѿ� ����
        if (remainAmmo >= ammoPerShot)
        {
            remainAmmo -= ammoPerShot;

            mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0f;
            Vector2 shootDirection = ((Vector2)mousePos - (Vector2)playerTrans.position).normalized;

            GameObject bullet = Instantiate(bulletPrefab, playerTrans.position, Quaternion.identity);
            if(Player.Instance.mainWeaponType == Player.WeaponType.Bomb)
            {
                bullet.GetComponent<BombBullet>().moveVec = shootDirection;
            }
            else if(Player.Instance.mainWeaponType == Player.WeaponType.Water)
            {
                bullet.GetComponent<WaterBullet>().moveVec = shootDirection;
            }
            else
            {
                bullet.GetComponent<GatlingBullet>().moveVec = shootDirection;
            }
            
            lastAttackTime = Time.time;
        }
    }

    // ����
    public void Loading()
    {
        
    }

    // źâ����
    public void InitAmmo()
    {
        remainAmmo = 0;
    }
}
