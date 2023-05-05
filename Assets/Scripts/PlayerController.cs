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
    [SerializeField] private float speed; //��� ��������� ��������
    [SerializeField] private float jumpForce; //���� ������
    [SerializeField] private float gravity; //���� ����������
    [SerializeField] private int bolt; // �������
    [SerializeField] GameObject losePanel; //��� ������ ���������
    [SerializeField] private TMP_Text boltText; //����������� ������ ����� �����

    private int lineToMove = 1; //�����, �� ������� ������ �������� (0-�����, 1-�������, 3-������)
    public float lineDistance = 4; //���������� ����� �������
    private float maxSpeed = 100; //������������ ��������

    void Start()
    {
        anim = GetComponentInChildren<Animator>();
        controller = GetComponent<CharacterController>();
        StartCoroutine(SpeedIncrease()); //����� ������� ���������
        Time.timeScale = 1;
        speed = 5;
    }

    private void Update()
    {
        //�������� ������
        if (SwipeController.swipeRight)
        {
            //�������� �� �������� ������
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
            if (controller.isGrounded) //�������� �� �������������
                Jump();
        }

        if (SwipeController.swipeDown)
        {
            StartCoroutine(Slide());
        }

        //�������� �� ������� ����� ������
        Vector3 targetPosition = transform.position.z * transform.forward + transform.position.y * transform.up;
        if (lineToMove == 0)
            targetPosition += Vector3.left * lineDistance;
        else if (lineToMove == 2)
            targetPosition += Vector3.right * lineDistance;

        //������������ � �������������
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
        dir.y = jumpForce; //y+z - �������� ����� � �����
        anim.SetTrigger("isJumping");
    }

    void FixedUpdate()
    {
        //�������� ���������
        dir.z = speed; //��������
        dir.y += gravity * Time.fixedDeltaTime; //����������
        controller.Move(dir * Time.fixedDeltaTime); //������ �����������
    }

    //������ ���������
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.tag == "obstacle")
        {
            losePanel.SetActive(true); //��� ������ ���������
            Time.timeScale = 0; //��������� �������
        }
    }

    //���� ������� � �� ��������
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Bolt")
        {
            bolt++;
            boltText.text = bolt.ToString(); // ������� �������
            Destroy(other.gameObject);
        }
    }

    //������� ��� ���������
    private IEnumerator SpeedIncrease()
    {
        yield return new WaitForSeconds(0.1f);
        if (speed < maxSpeed)
        {
            speed += 0.02f;
            StartCoroutine(SpeedIncrease());
        }
    }

    //�������
    private IEnumerator Slide()
    {
        col.center = new Vector3(0, 2.5f, 0);
        col.height = 5;

        yield return new WaitForSeconds(1);

        col.center = new Vector3(0, 5.5f, 0);
        col.height = 11;
    }
}