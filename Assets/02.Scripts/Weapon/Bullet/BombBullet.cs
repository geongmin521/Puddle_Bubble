using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombBullet : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float damage;
    [SerializeField] CircleCollider2D bombAraa;
    [SerializeField] CircleCollider2D damageArea;

    private void Start()
    {
        damageArea = GetComponent<CircleCollider2D>();
        bombAraa.enabled = true;
        damageArea.enabled = false;
        Destroy(gameObject, 10f);
    }

    private void Update()
    {
        Move();
    }

    // �Ѿ��̵�
    void Move()
    {
        transform.Translate(transform.right * speed * Time.deltaTime, Space.Self);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // if ���� �浹�ϸ�
        bombAraa.enabled = false;
        damageArea.enabled = true;
        DamageToEnemies();

        Destroy(gameObject);
    }

    // �浹�� �� get, �ǰ�ȣ��
    private void DamageToEnemies()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(transform.position, damageArea.radius);

        foreach (Collider2D enemy in hitEnemies)
        {
            //Enemy enemyScript = enemy.GetComponent<Enemy>();
            //enemyScript.TakeDamage(damage);
        }
    }
}
