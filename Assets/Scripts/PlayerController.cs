using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{
    private CharacterController controller;
    private CapsuleCollider col;
    private Animator anim;
    private Vector3 dir;
    [SerializeField] private float speed; //для изменения скорости
    [SerializeField] private float jumpForce; //сила прыжка
    [SerializeField] private float gravity; //сила гравитации
    [SerializeField] private int bolt; // монетки
    [SerializeField] GameObject losePanel; //для панели проигрыша
    [SerializeField] private TMP_Text boltText; //отображение текста сбора монет

    private int lineToMove = 1; //линия, по которой сейчас движемся (0-левая, 1-средняя, 3-правая)
    public float lineDistance = 4; //расстояние между линиями
    private float maxSpeed = 100; //максимальная скорость

    void Start()
    {
        anim = GetComponentInChildren<Animator>();
        controller = GetComponent<CharacterController>();
        StartCoroutine(SpeedIncrease()); //вызов функции ускорения
        Time.timeScale = 1;
        speed = 5;
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
            if (controller.isGrounded) //проверка на заземленность
                Jump();
        }

        if (SwipeController.swipeDown)
        {
            StartCoroutine(Slide());
        }

        //Проверка на позицию после свайпа
        Vector3 targetPosition = transform.position.z * transform.forward + transform.position.y * transform.up;
        if (lineToMove == 0)
            targetPosition += Vector3.left * lineDistance;
        else if (lineToMove == 2)
            targetPosition += Vector3.right * lineDistance;

        //Столкновение с препятствиями
        if (transform.position == targetPosition)
            return;
        Vector3 diff = targetPosition - transform.position;
        Vector3 moveDir = diff.normalized * 25 * Time.deltaTime;
        if (moveDir.sqrMagnitude < diff.sqrMagnitude)
            controller.Move(moveDir);
        else
            controller.Move(diff);
    }

    private void Jump()
    {
        dir.y = jumpForce; //y+z - движение прямо и вверх
        anim.SetTrigger("isJumping");
    }

    void FixedUpdate()
    {
        //Движение персонажа
        dir.z = speed; //скорость
        dir.y += gravity * Time.fixedDeltaTime; //гравитация
        controller.Move(dir * Time.fixedDeltaTime); //нужное направление
    }

    //Панель проигрыша
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.tag == "obstacle")
        {
            losePanel.SetActive(true); //вкл панель проигрыша
            Time.timeScale = 0; //остановка времени
        }
    }

    //счёт монеток и их удаление
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Bolt")
        {
            bolt++;
            boltText.text = bolt.ToString(); // счетчик монеток
            Destroy(other.gameObject);
        }
    }

    //функция для ускорения
    private IEnumerator SpeedIncrease()
    {
        yield return new WaitForSeconds(0.1f);
        if (speed < maxSpeed)
        {
            speed += 0.02f;
            StartCoroutine(SpeedIncrease());
        }
    }

    //Перекат
    private IEnumerator Slide()
    {
        col.center = new Vector3(0, 2.5f, 0);
        col.height = 5;

        yield return new WaitForSeconds(1);

        col.center = new Vector3(0, 5.5f, 0);
        col.height = 11;
    }
}