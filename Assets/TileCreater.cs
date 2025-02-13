using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileCreater : MonoBehaviour
{
    public Tilemap tilemap;               // Ÿ�ϸ�
    public Tile[] tiles;                  // �������� ��ġ�� Ÿ�ϵ�
    public Tile road;                     // �������� ��ġ�� Ÿ�ϵ� (����)
    public Tile grass;                    // �������� ��ġ�� Ÿ�ϵ� (�ܵ�)
    public Tile roadbleow;                // �������� ��ġ�� Ÿ�ϵ� (����,�Ʒ�)
    public Tile grassbleow;               // �������� ��ġ�� Ÿ�ϵ� (�ܵ�,�Ʒ�)
    public GameObject[] objectsToPlace;   // �������� ��ġ�� ������Ʈ��
    public Vector2Int gridSize;           // Ÿ�ϸ� �׸��� ũ�� (X, Y)

    public Transform gridParent;          // �θ� ��ü�� ��ġ�� Grid (Ÿ�ϸ��� �θ�� ����)

    public int minPlacementInterval = 1;  // �ּ� ��ġ ���� (��: 1ĭ)
    public int maxPlacementInterval = 5;  // �ִ� ��ġ ���� (��: 5ĭ)

    void Start()
    {
        PlaceRandomTiles();
    }

    // Ÿ���� �����ϰ� ��ġ�ϴ� �Լ�
    void PlaceRandomTiles()
    {
        int size = gridSize.x / 2;

        // 1. Ÿ���� �����ϰ� ��ġ
        for (int x = -size; x < size; x++)
        {
            for (int y = -size; y < size; y++)
            {
                Vector3Int tilePosition = new Vector3Int(x, y, 0);
                Tile randomTile = tiles[Random.Range(0, tiles.Length)];
                tilemap.SetTile(tilePosition, randomTile);
            }
        }

        // 2. ���� Ÿ�� ��ġ (y = 10 ��ġ��)
        for (int y = -size; y < size; y++)
        {
            Vector3Int tilePosition = new Vector3Int(y, 10, 0);
            tilemap.SetTile(tilePosition, road);
        }

        // 3. �ܵ� Ÿ�� ��ġ (y = 11 �̻� ��ġ��)
        for (int x = -size; x < size; x++)
        {
            for (int y = 11; y < size; y++)
            {
                Vector3Int tilePosition = new Vector3Int(x, y, 0);
                tilemap.SetTile(tilePosition, grass);
            }
        }

        // 4. ���� Ÿ�� ��ġ (y = -10 ��ġ��)
        for (int y = -size; y < size; y++)
        {
            Vector3Int tilePosition = new Vector3Int(y, -10, 0);
            tilemap.SetTile(tilePosition, roadbleow);
        }

        // 5. �ܵ� Ÿ�� ��ġ (y = -11 ���� ��ġ��)
        for (int x = -size; x < size; x++)
        {
            for (int y = -11; y >= -size; y--)  // �Ʒ��� Ÿ�ϸ� ��ġ�� y�� -11 ���Ϸ� ��ġ (������������ ����)
            {
                Vector3Int tilePosition = new Vector3Int(x, y, 0);
                tilemap.SetTile(tilePosition, grassbleow);
            }
        }

        // 6. �ܵ� Ÿ�� ��ġ���� ���� �������� ������Ʈ ��ġ
        PlaceRandomObjectsOnGrass();
    }

    // �ܵ� Ÿ�� ������ ���� ������Ʈ�� ���� �������� ��ġ�ϴ� �Լ�
    void PlaceRandomObjectsOnGrass()
    {
        int size = gridSize.x / 2;

        // gridSize ũ�� ��ŭ �ݺ����� ���� ���� ������Ʈ ��ġ
        for (int x = -size + 1; x < size - 1; x++)   // 1ĭ �������� ��ġ ���� ����
        {
            for (int y = -size + 1; y < size - 1; y++)  // 1ĭ �������� ��ġ ���� ����
            {
                // y=10�� y=-10������ ������Ʈ�� ��ġ���� ����
                if (y == 11 || y == -10)
                {
                    continue; // �� ��ġ�� �ǳʶٱ�
                }

                Vector3Int tilePosition = new Vector3Int(x, y, 0);

                // �ش� ��ġ�� ��ġ�� Ÿ���� Ȯ��
                Tile currentTile = tilemap.GetTile<Tile>(tilePosition);

                // ���� Ÿ���� grass�� ���� ������Ʈ ��ġ
                if (currentTile == grass || currentTile == grassbleow)
                {
                    // ������ ������ ���� (minPlacementInterval ~ maxPlacementInterval)
                    int randomIntervalX = Random.Range(minPlacementInterval, maxPlacementInterval + 1);
                    int randomIntervalY = Random.Range(minPlacementInterval, maxPlacementInterval + 1);

                    // ���� �������θ� ������Ʈ ��ġ
                    if (x % randomIntervalX == 0 && y % randomIntervalY == 0)
                    {
                        // ���� ������Ʈ ����
                        GameObject randomObject = objectsToPlace[Random.Range(0, objectsToPlace.Length)];

                        // ������Ʈ�� �׸��� ��ġ�� ��ġ
                        Vector3 position = tilemap.CellToWorld(tilePosition);  // Ÿ�� ��ġ�� ���� ��ǥ�� ��ȯ

                        // ������Ʈ ����, �θ�� gridParent�� ����
                        GameObject placedObject = Instantiate(randomObject, position, Quaternion.identity);
                        placedObject.transform.SetParent(gridParent); // Ÿ�ϸ��� �θ�� ����
                    }
                }
            }
        }
    }

}
