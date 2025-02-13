using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MonsterType { Sand, Mud, Stone } // ���� ����
public enum MonsterGrade { Normal, Speed, Defense, Elite } // ���� ���

public class Monster : MonoBehaviour
{
    public MonsterType Type;      // ���� ����
    public MonsterGrade Grade;    // ���� ���
    public float Speed;           // �̵� �ӵ�
    public int Health;            // ü��
    public float Size;            // ũ��
    public int AttackPower;       // ���ݷ�
    public Sprite[] GradeSprite;  // ��޺� ��������Ʈ �迭 

    protected bool isDead = false;

    protected SpriteRenderer spriteRenderer;

    private Transform player;  // �÷��̾� ��ġ

    protected virtual void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    protected virtual void Start()
    {
        ApplyGradeModifiers();
        // UpdateSprite(Grade);
        transform.localScale = new Vector3(Size, Size, 1f); // ũ�� �ݿ�

        player = GameObject.FindGameObjectWithTag("Player").transform; // �÷��̾� ��ü (�±�) 
    }

    public void UpdateSprite(MonsterGrade grade)
    {
        int gradeIndex = (int)grade;
        if (GradeSprite != null && gradeIndex >= 0 && gradeIndex < GradeSprite.Length)
        {
            spriteRenderer.sprite = GradeSprite[gradeIndex];
        }
       
    }

    // ��޺� ����ġ ���� 
    public void ApplyGradeModifiers()
    {
        switch (Grade)
        {
            case MonsterGrade.Speed:
                Speed *= 1.2f;
                spriteRenderer.color = new Color(1f, 1f, 0f);
                break;
            case MonsterGrade.Defense:
                Health *= 2;
                spriteRenderer.color = new Color(0f, 0f, 1f);
                break;
            case MonsterGrade.Elite:
                Size *= 1.5f;
                Health = Mathf.RoundToInt(Health * 1.5f);
                AttackPower *= 2;
                spriteRenderer.color = new Color(1f, 0f, 0f);
                break;
        }
    }

    void Update()
    {
        if ((player != null) && (!isDead))
        {
            MoveTowardsPlayer();
        }
    }

    void MoveTowardsPlayer()
    {
        transform.position = Vector3.MoveTowards(transform.position, player.position, (Speed/300f) * Time.deltaTime);
    }

    protected void Death()
    {
        GameManager.instance.Score += (Health + (int)Speed) * AttackPower;
        print("GameManager�� Score = " + GameManager.instance.Score);
        Destroy(gameObject, 0.5f);
       
    }
}
