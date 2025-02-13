using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MudMonster : Monster
{
    public Animator anim;

    protected override void Start()
    {
        anim = GetComponent<Animator>();

        Type = MonsterType.Mud;
        Speed = 600;
        Health = 300;
        Size = 2f;
        AttackPower = 1;
        base.Start();
    }

    public void TakeDamage(float damage)
    {
        Health -= (int)damage;
        Debug.Log(Health);

        if (Health <= 0)
        {
            anim.SetTrigger("Die");
            base.Death();
        }
    }

    public int GetHealth()
    {
        return Health;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Player.Instance.TakeDamage();
        }
    }
}


