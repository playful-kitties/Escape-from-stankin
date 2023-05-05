using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileGenerator : MonoBehaviour
{
    public GameObject[] tilePrefabs;
    private List<GameObject> activeTiles = new List<GameObject>(); //for add obj & del obj
    private float spawnPos = 0; //������� ������ ��������� �� ��� z
    private float tileLenght = 100; //����� ���������

    [SerializeField] private Transform player; //��� ������������ ��������� ������
    private int startTiles = 7; //��������� ���������

    void Start()
    {
        //C���� ��������� ���������
        for (int i = 0; i < startTiles; i++)
        {
            if (i == 0)
                SpawnTile(6);
            SpawnTile(Random.Range(0, tilePrefabs.Length)); 
        }
    }

    void Update()
    {
        if (player.position.z - 60 > spawnPos - (startTiles * tileLenght)) //�����.���.z > (���.������ - ����� �����.�������)
        {
            SpawnTile(Random.Range(0, tilePrefabs.Length));
            DeleteTile();
        }
    }

    private void SpawnTile(int tileIndex)
    {
        GameObject nextTile = Instantiate(tilePrefabs[tileIndex], transform.forward * spawnPos, transform.rotation); //������ ��� ��������� ���������
        activeTiles.Add(nextTile); //���������� � ������
        spawnPos += tileLenght;
    }
    private void DeleteTile()
    {
        //�������� ���������
        Destroy(activeTiles[0]); //�� �����
        activeTiles.RemoveAt(0); //�� ������
    }
}
