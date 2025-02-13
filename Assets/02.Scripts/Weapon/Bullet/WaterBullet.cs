using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterBullet : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float damage;
    [SerializeField] int life;
    private float remainDamage;

    public Vector3 moveVec = Vector3.zero;

    // conponets
    private Animator animator;
    private CircleCollider2D collider2D;

    private void Start()
    {
        animator = GetComponentInChildren<Animator>();
        collider2D = GetComponentInChildren<CircleCollider2D>();

        remainDamage = damage;
        Destroy(gameObject, 10f);
    }

    private void Update()
    {
        Move();
    }

    // 총알이동
    void Move()
    {
        transform.Translate(moveVec * speed * Time.deltaTime, Space.Self);
    }

    // 공격
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("SandMonster"))
        {
            SandMonster sandMonster = collision.gameObject.GetComponent<SandMonster>();
            int mosterHP = sandMonster.GetHealth();
            if(remainDamage >= mosterHP)
            {
                sandMonster.TakeDamage(mosterHP);
                remainDamage -= mosterHP;
            }
            else
            {
                sandMonster.TakeDamage(remainDamage);
                remainDamage = 0;
            }
            life--;
        }
        
        if (collision.gameObject.CompareTag("MudMonster"))
        {
            MudMonster mudMonster = collision.gameObject.GetComponent<MudMonster>();
            int mosterHP = mudMonster.GetHealth();
            if (remainDamage >= mosterHP)
            {
                mudMonster.TakeDamage(mosterHP);
                remainDamage -= mosterHP;
            }
            else
            {
                mudMonster.TakeDamage(remainDamage);
                remainDamage = 0;
            }
            life--;
        }

        if (collision.gameObject.CompareTag("StoneMonster"))
        {
            StoneMonster mudMonster = collision.gameObject.GetComponent<StoneMonster>();
            int mosterHP = mudMonster.GetHealth();
            if (remainDamage >= mosterHP)
            {
                mudMonster.TakeDamage(mosterHP);
                remainDamage -= mosterHP;
            }
            else
            {
                mudMonster.TakeDamage(remainDamage);
                remainDamage = 0;
            }
            life--;
        }

        if (life <= 0 || remainDamage == 0)
        {
            collider2D.enabled = false;
            animator.SetBool("isBurst", true);
            Destroy(gameObject, 0.15f);
        }
    }
}
