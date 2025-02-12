using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// �� ���� �����͸� �����ϴ� ScriptableObject ��


[CreateAssetMenu(fileName = "NewMonsterData", menuName = "Game/Monster Data")]
public class MonsterDataSO : ScriptableObject
{
    public string Type;       // ���� �̸� (Sand, Mud)
    public float Speed;       // �̵� �ӵ�
    public int Health;        // ü��
    public float Size;        // ũ��
    public int AttackPower;   // ���ݷ�
    public Sprite Sprite;     // ������ ��������Ʈ
}
