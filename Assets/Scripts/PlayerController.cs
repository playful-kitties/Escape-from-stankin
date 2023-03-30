using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 dir;
    [SerializeField] private int speed; //для изменения скорости
    [SerializeField] private float jumpForce; //сила прыжка
    [SerializeField] private float gravity; //сила гравитации

    private int lineToMove = 1; //линия, по которой сейчас движемся (0-левая, 1-средняя, 3-правая)
    public float lineDistance = 4; //расстояние между линиями

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        //Движение вправо
        if (SwipeController.swipeRight)
        {
            //Проверка на движение вправо
            if (lineToMove < 2)
                lineToMove++;
        }

        if (SwipeController.swipeLeft)
        {
            if (lineToMove > 0)
                lineToMove--;
        }

        if (SwipeController.swipeUp)
        {
            if (controller.isGrounded)//проверка на заземленность
                Jump();
        }

        //Проверка на позицию после свайпа
        Vector3 targetPosition = transform.position.z * transform.forward + transform.position.y * transform.up;
        if (lineToMove == 0)
            targetPosition += Vector3.left * lineDistance;
        else if (lineToMove == 2)
            targetPosition += Vector3.right * lineDistance;

        transform.position = targetPosition;
    }

    private void Jump()
    {
        dir.y = jumpForce; //y+z - движение прямо и вверх
    }

    void FixedUpdate()
    {
        //Движение персонажа
        dir.z = speed; //скорость
        dir.y += gravity * Time.fixedDeltaTime; //гравитация
        controller.Move(dir*Time.fixedDeltaTime); //нужное направление
    }
}
