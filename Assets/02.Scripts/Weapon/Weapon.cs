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

    private Vector2 mousPos;             // ���콺 ��ġ
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

            GameObject bullet = Instantiate(bulletPrefab, playerTrans.position, playerTrans.rotation);
            mousPos = mainCamera.ScreenToViewportPoint(Input.mousePosition);
            bullet.transform.LookAt(mousPos);

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
