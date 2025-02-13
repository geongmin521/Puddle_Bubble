using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    public List<GameObject> spawnPoints;  // ī�޶� �߽����� ȸ���ϴ� 3�� ������Ʈ
    public List<GameObject> monsterList1; // ù ��° ���� ����Ʈ
    public List<GameObject> monsterList2; // �� ��° ���� ����Ʈ
    public List<GameObject> monsterList3; // �� ��° ���� ����Ʈ

    float waveIntervalMin = 0f;
    float waveIntervalMax = 5f;

    private List<List<GameObject>> allMonsterLists; // ��� ����Ʈ�� ������ ����Ʈ

    void Start()
    {
        // ��� ���� ����Ʈ�� �ϳ��� ����Ʈ�� ����
        allMonsterLists = new List<List<GameObject>> { monsterList1, monsterList2, monsterList3 };

        // ���� ���� ��ƾ ����
        StartCoroutine(SpawnMonsterRoutine());
    }

    IEnumerator SpawnMonsterRoutine()
    {
        while (true)
        {
            float waitTime = Random.Range(waveIntervalMin, waveIntervalMax);
            yield return new WaitForSeconds(waitTime);

            StartCoroutine(SpawnMonsters());
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
            Instantiate(monsterPrefab, spawnPoint.position, Quaternion.identity);
            yield return new WaitForSeconds(1f); // 1�� ��� �� ���� ���� ����
        }
    }
}