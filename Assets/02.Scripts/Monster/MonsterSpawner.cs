using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// �� ���� ���� ��


public class MonsterSpawner : MonoBehaviour
{
    public Camera mainCamera;
    public GameObject mudMonsterPrefab;
    public GameObject sandMonsterPrefab;
    public GameObject stoneMonsterPrefab;

    public MonsterType monsterType;

    float spawnIntervalMin = 0f;
    float spawnIntervalMax = 5f;
    float spawnOffset = 100f;

    private float nextSpawnTime;

    public Transform spawnParent; // ������ ���͵��� �θ�(Parent)


    void Start()
    {
        nextSpawnTime = Time.time + Random.Range(spawnIntervalMin, spawnIntervalMax);
    }

    void Update()
    {
        if (Time.time >= nextSpawnTime)
        {
            SpawnMonster();
            nextSpawnTime = Time.time + Random.Range(spawnIntervalMin, spawnIntervalMax);
        }
    }

    void SpawnMonster()
    {
        if (mainCamera == null) { return; }

        Vector3 spawnPosition = GetRandomOffScreenPosition();

        // ���� Ÿ�� ���� ����
        MonsterType selectedType = (MonsterType)Random.Range(0, 3);

        GameObject monsterPrefab = null;
        switch (selectedType)
        {
            case MonsterType.Sand:
                monsterPrefab = sandMonsterPrefab;
                break;
            case MonsterType.Mud:
                monsterPrefab = mudMonsterPrefab;
                break;
            case MonsterType.Stone:
                monsterPrefab = stoneMonsterPrefab;
                break;
        }

        if (monsterPrefab == null) { return; }

        GameObject monsterObject = Instantiate(monsterPrefab, spawnPosition, Quaternion.identity, spawnParent);
        Monster monster = monsterObject.GetComponent<Monster>();

        if (monster != null)
        {
            // ��� ���� ����
            MonsterGrade randomGrade = (MonsterGrade)Random.Range(0, 4);
            monster.Grade = randomGrade;

            // ��޺� ����ġ ����
            monster.ApplyGradeModifiers();
           // monster.UpdateSprite(randomGrade); // ���� ���� �ڵ� ���� 
        }
        else { Debug.LogError("Spawned monster prefab does not have a Monster script attached."); }
    }

    private Vector3 GetRandomOffScreenPosition()
    {
        Vector3 camPosition = mainCamera.transform.position;
        float camHeight = 2f * mainCamera.orthographicSize;
        float camWidth = camHeight * mainCamera.aspect;

        float pixelToWorld = camHeight / Screen.height;
        float worldOffset = spawnOffset * pixelToWorld;

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

        return spawnPos;
    }
}
