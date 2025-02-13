using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    public List<GameObject> spawnPoints;
    public List<GameObject> monsterList1;
    public List<GameObject> monsterList2;
    public List<GameObject> monsterList3;

    public float waveIntervalMin = 10f; // ���̺� �ּ� ��� �ð�
    public float waveIntervalMax = 15f; // ���̺� �ִ� ��� �ð�
    public float monsterSpawnInterval = 0.5f; // ���� ���� ���� ����

    private List<List<GameObject>> allMonsterLists; // ��� ����Ʈ ������ ����Ʈ
    public Transform waveParent;                    // ������ ���͵��� �θ�(Parent)

    void Start()
    {
        // ��� ���� ����Ʈ�� �ϳ��� ����Ʈ�� ����
        allMonsterLists = new List<List<GameObject>> { monsterList1, monsterList2, monsterList3 };

        StartCoroutine(SpawnMonsterRoutine());
    }

    IEnumerator SpawnMonsterRoutine()
    {
        while (true)
        {
            float waitTime = Random.Range(waveIntervalMin, waveIntervalMax);
            yield return new WaitForSeconds(waitTime);

            // `yield return`�� ����Ͽ� ���̺갡 ���� ������ ���
            yield return StartCoroutine(SpawnMonsters());
        }
    }

    IEnumerator SpawnMonsters()
    {
        if (spawnPoints.Count == 0 || allMonsterLists.Count == 0) yield break;

        // ȸ���ϴ� ������Ʈ �� ������ ��ġ ����
        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Count)].transform;

        // ���� ����Ʈ �� ���� ����
        List<GameObject> selectedMonsterList = allMonsterLists[Random.Range(0, allMonsterLists.Count)];

        // ����Ʈ�� �ִ� ��� ���� ����
        if (selectedMonsterList.Count == 0) yield break;

        foreach (GameObject monsterPrefab in selectedMonsterList)
        {
            GameObject monsterObject = Instantiate(monsterPrefab, spawnPoint.position, Quaternion.identity, waveParent);
            Monster monster = monsterObject.GetComponent<Monster>();

            if (monster != null)
            {
                // ��� ���� ����
                MonsterGrade randomGrade = (MonsterGrade)Random.Range(0, 4);
                monster.Grade = randomGrade;

                // ��޺� ����ġ ����
                monster.ApplyGradeModifiers();
                // monster.UpdateSprite(randomGrade);
            }

            yield return new WaitForSeconds(monsterSpawnInterval); // 0.5�� �������� ����
        }
    }
}

