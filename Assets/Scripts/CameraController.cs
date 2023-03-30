using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform player;
    private Vector3 offset; //расстояние между игроком и камерой

    void Start()
    {
        offset = transform.position - player.position; //промежуток расстояния
    }

    void FixedUpdate()
    {
        //Расчет нового места для перемещения
        Vector3 newPosition = new Vector3(transform.position.x, transform.position.y, offset.z + player.position.z); //x,y - текущее положение камеры и z - сумма промежутка расстояния и персонажа
        transform.position = newPosition; //перемещение камеры в позицию
    }
}
