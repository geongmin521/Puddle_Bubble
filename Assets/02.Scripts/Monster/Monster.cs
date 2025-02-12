using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MonsterType { Sand, Mud } // ���� ����
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

    protected SpriteRenderer spriteRenderer;

    private Transform player;  // �÷��̾� ��ġ

    protected virtual void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    protected virtual void Start()
    {
        ApplyGradeModifiers();
        UpdateSprite(Grade);
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
        else
        {
            Debug.LogWarning($"Invalid grade index {gradeIndex} for {gameObject.name}");
        }
    }

    // ��޺� ����ġ 
    public void ApplyGradeModifiers()
    {
        switch (Grade)
        {
            case MonsterGrade.Speed:
                Speed *= 1.2f;
                break;
            case MonsterGrade.Defense:
                Health *= 2;
                break;
            case MonsterGrade.Elite:
                Size *= 1.5f;
                Health = Mathf.RoundToInt(Health * 1.5f);
                AttackPower *= 2;
                break;
        }
    }

    void Update()
    {
        if (player != null)
        {
            MoveTowardsPlayer();
        }
    }

    void MoveTowardsPlayer()
    {
        // Vector3 direction = (player.position - transform.position).normalized; // �÷��̾�-���� ���� 
        transform.position = Vector3.MoveTowards(transform.position, player.position, (Speed/300f) * Time.deltaTime);
    }

}
