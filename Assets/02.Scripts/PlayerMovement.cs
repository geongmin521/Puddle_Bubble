using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;  // ĳ���� �̵� �ӵ�
    private Rigidbody2D rb;       // Rigidbody2D ������Ʈ�� ������ ����

    void Start()
    {
        // Rigidbody2D ������Ʈ�� ������
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // ���� (�¿�) �� ���� (����) �Է� ó��
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        // ĳ������ �̵� ���� ���� (�����¿� ��� ����)
        Vector2 moveDirection = new Vector2(moveX, moveY).normalized;

        // Rigidbody2D�� ����Ͽ� ĳ���� �̵�
        rb.velocity = new Vector2(moveDirection.x * moveSpeed, moveDirection.y * moveSpeed);
    }
}
