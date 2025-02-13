using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MonsterType { Sand, Mud, Stone } // 몬스터 종류
public enum MonsterGrade { Normal, Speed, Defense, Elite } // 몬스터 등급

public class Monster : MonoBehaviour
{
    public MonsterType Type;      // 몬스터 종류
    public MonsterGrade Grade;    // 몬스터 등급
    public float Speed;           // 이동 속도
    public int Health;            // 체력
    public float Size;            // 크기
    public int AttackPower;       // 공격력
    // public Sprite[] GradeSprite;  // 등급별 스프라이트 배열 

    protected bool isDead = false;

    protected SpriteRenderer spriteRenderer;

    private Transform player;  // 플레이어 위치

    protected virtual void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    protected virtual void Start()
    {
        ApplyGradeModifiers();
        transform.localScale = new Vector3(Size, Size, 1f); // 크기 반영

        player = GameObject.FindGameObjectWithTag("Player").transform; // 플레이어 객체 (태그) 
    }

    // 등급별 가중치 적용 
    public void ApplyGradeModifiers()
    {
        switch (Grade)
        {
            case MonsterGrade.Speed:
                Speed *= 2;
                spriteRenderer.color = new Color(1f, 1f, 0f);
                break;
            case MonsterGrade.Defense:
                Health *= 2;
                spriteRenderer.color = new Color(0.329f, 0.329f, 1f); // 5454FF 
                break;
            case MonsterGrade.Elite:
                Size *= 1.5f;
                Health = Mathf.RoundToInt(Health * 1.5f);
                AttackPower *= 2;
                spriteRenderer.color = new Color(1f, 0.278f, 0.278f); // FF4747
                break;
        }
    }

    void Update()
    {
        if ((player != null) && (!isDead))
        {
            MoveTowardsPlayer(); 
            FlipSprite();
        }
    }

    // 플레이어 향해 이동
    void MoveTowardsPlayer()
    {
        transform.position = Vector3.MoveTowards(transform.position, player.position, (Speed/500f) * Time.deltaTime);
    }

    // 플레이어 바라보도록 좌우반전
    void FlipSprite()
    {
        if (player != null) { spriteRenderer.flipX = transform.position.x < player.position.x; }
    }

    protected void Death()
    {
        isDead = true;
        GameManager.instance.score += (Health + (int)Speed) * AttackPower;
        print("GameManager의 Score = " + GameManager.instance.score);

        UIManager.Instance.UpdateScore(GameManager.instance.score); 
        Destroy(gameObject, 0.5f);
       
    }
}
