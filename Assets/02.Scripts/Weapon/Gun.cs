using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [Header("Weapon Stats")]
    public float damage;             // �⺻ ������
    public int ammoPerShot;          // ź�� �Ҹ�
    public int remainAmmo;           // �ܿ� ź�� ��
    public float attackCoolTime;     // �߻� �ֱ�

    [Header("Assets")]
    public GameObject bulletPrefab;      // �Ѿ� ������
    public Transform muzzlePoint;        // �Ѿ� ���� ��ġ
    public ParticleSystem muzzleFlash; // �ѱ� ȭ�� ����Ʈ
    public AudioClip fireSFX;          // �߻� ����

    private float lastAttackTime = 0f;   // ������ ���� �ð�

    // ����
    public void Attack()
    {
        // ��Ÿ�� ����
        if (Time.time - lastAttackTime < attackCoolTime)
            return;

        // ź�� Ȯ��
        if (remainAmmo >= ammoPerShot)
        {
            Instantiate(bulletPrefab, muzzlePoint.position, muzzlePoint.rotation);
            remainAmmo -= ammoPerShot;
            lastAttackTime = Time.time;
        }
    }
}
