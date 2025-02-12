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

    protected virtual void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    protected virtual void Start()
    {
        UpdateSprite(Grade);
        transform.localScale = new Vector3(Size, Size, 1f); // ũ�� �ݿ�
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
}