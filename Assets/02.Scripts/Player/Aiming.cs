using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aming : MonoBehaviour
{
    [SerializeField] private Sprite cursorSprite;
    [SerializeField] private Transform playerTransform;
    [SerializeField] private Material lineMaterial;

    private GameObject cursorObject;
    private LineRenderer lineRenderer;

    private void Start()
    {
        Cursor.visible = false;

        cursorObject = new GameObject("CursorSprite");
        SpriteRenderer sr = cursorObject.AddComponent<SpriteRenderer>();
        sr.sprite = cursorSprite;
        sr.sortingOrder = 10;

        // LineRenderer
        GameObject lineObj = new GameObject("AimLine");
        lineRenderer = lineObj.AddComponent<LineRenderer>();
        lineRenderer.material = lineMaterial;
        lineRenderer.startWidth = 0.05f;
        lineRenderer.endWidth = 0.05f;
        lineRenderer.positionCount = 2;
        lineRenderer.sortingOrder = 5; // Ŀ������ ����

        // ���� ��Ÿ�� ���� (���� ��Ƽ������ ���� ��Ÿ�Ϸ� �����ؾ� ��)
        lineRenderer.textureMode = LineTextureMode.Tile;
    }

    private void Update()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0f;

        cursorObject.transform.position = mousePosition;
        lineRenderer.SetPosition(0, playerTransform.position);
        lineRenderer.SetPosition(1, mousePosition);
    }
}
