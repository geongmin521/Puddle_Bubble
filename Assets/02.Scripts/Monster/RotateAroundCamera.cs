using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RotateAroundCamera : MonoBehaviour
{
    public Transform cameraTransform;
    public float rotationSpeed = 30f;
    private Vector3 offset;

    void Start()
    {
        if (cameraTransform == null)
        {
            cameraTransform = Camera.main.transform;
        }

        // �ʱ� ��ġ�� ī�޶� ��ġ ���� ����
        offset = transform.position - cameraTransform.position;
    }

    void Update()
    {
        if (cameraTransform == null) return;

        // �����̴� ī�޶�� ������ �Ÿ� �����ϸ鼭 ȸ��
        transform.position = cameraTransform.position + offset;
        transform.RotateAround(cameraTransform.position, Vector3.forward, rotationSpeed * Time.deltaTime);

        // ���� ��ġ �������� ���ο� offset ���
        offset = transform.position - cameraTransform.position;
    }
}
