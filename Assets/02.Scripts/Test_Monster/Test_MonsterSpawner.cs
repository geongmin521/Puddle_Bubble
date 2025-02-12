using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_MonsterSpawner : MonoBehaviour
{
    public List<GameObject> monsterPrefabs; // ���� ������ ������Ʈ ����Ʈ

    float minSpawnTime = 0f;  // �ּ� ���� �ð�
    float maxSpawnTime = 5f;  // �ִ� ���� �ð�
    float spawnOffset = 100f; // ȭ�� �ٱ� ���� �Ÿ� (�ȼ�)

    Camera mainCamera;


    void Start()
    {
        mainCamera = Camera.main; // ī�޶� ����� ���� ���� 
        StartCoroutine(SpawnMonsterRoutine());
    }


    IEnumerator SpawnMonsterRoutine()
    {
        while (true)
        {
            float waitTime = Random.Range(minSpawnTime, maxSpawnTime);
            yield return new WaitForSeconds(waitTime);
            
            SpawnMonster();
        }
    }


    void SpawnMonster()
    {
        if (mainCamera == null || monsterPrefabs.Count == 0) { Debug.LogError("mainCamera == null || monsterPrefabs.Count == 0"); return; }

        // ���� ī�޶� ��ġ �� ȭ�� ũ�� ���
        Vector3 camPosition = mainCamera.transform.position;
        float camHeight = 2f * mainCamera.orthographicSize;
        float camWidth = camHeight * mainCamera.aspect;

        // �ȼ� ���� -> World ������ ��ȯ
        float pixelToWorld = camHeight / Screen.height;
        float worldOffset = spawnOffset * pixelToWorld;

        // ������ ȭ�� �����ڸ� ���� (��, �Ʒ�, ����, ������)
        int side = Random.Range(0, 4);
        Vector3 spawnPos = Vector3.zero;

        switch (side)
        {
            case 0: // ����
                spawnPos = new Vector3(
                    Random.Range(camPosition.x - camWidth / 2, camPosition.x + camWidth / 2),
                    camPosition.y + camHeight / 2 + worldOffset,
                    0);
                break;
            case 1: // �Ʒ���
                spawnPos = new Vector3(
                    Random.Range(camPosition.x - camWidth / 2, camPosition.x + camWidth / 2),
                    camPosition.y - camHeight / 2 - worldOffset,
                    0);
                break;
            case 2: // ����
                spawnPos = new Vector3(
                    camPosition.x - camWidth / 2 - worldOffset,
                    Random.Range(camPosition.y - camHeight / 2, camPosition.y + camHeight / 2),
                    0);
                break;
            case 3: // ������
                spawnPos = new Vector3(
                    camPosition.x + camWidth / 2 + worldOffset,
                    Random.Range(camPosition.y - camHeight / 2, camPosition.y + camHeight / 2),
                    0);
                break;
        }

        // ���� ������Ʈ ���� �� ����
        GameObject randomObject = monsterPrefabs[Random.Range(0, monsterPrefabs.Count)];
        Instantiate(randomObject, spawnPos, Quaternion.identity);
    }
}