using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileGenerator : MonoBehaviour
{
    public GameObject[] tilePrefabs;
    private List<GameObject> activeTiles = new List<GameObject>(); //for add obj & del obj
    private float spawnPos = 0; //позиция спавна платформы по оси z
    private float tileLenght = 100; //длина платформы

    [SerializeField] private Transform player; //для отслеживания положения игрока
    private int startTiles = 7; //стартовые платформы

    void Start()
    {
        //Cпавн случайной платформы
        for (int i = 0; i < startTiles; i++)
        {
            if (i == 0)
                SpawnTile(6);
            SpawnTile(Random.Range(0, tilePrefabs.Length)); 
        }
    }

    void Update()
    {
        if (player.position.z - 60 > spawnPos - (startTiles * tileLenght)) //полож.игр.z > (поз.спавна - длина старт.дорожки)
        {
            SpawnTile(Random.Range(0, tilePrefabs.Length));
            DeleteTile();
        }
    }

    private void SpawnTile(int tileIndex)
    {
        GameObject nextTile = Instantiate(tilePrefabs[tileIndex], transform.forward * spawnPos, transform.rotation); //только что созданная платформа
        activeTiles.Add(nextTile); //добавление в список
        spawnPos += tileLenght;
    }
    private void DeleteTile()
    {
        //Удаление платформы
        Destroy(activeTiles[0]); //со сцены
        activeTiles.RemoveAt(0); //из списка
    }
}
