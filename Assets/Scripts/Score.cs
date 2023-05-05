using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Score : MonoBehaviour
{
    [SerializeField] private Transform player; //��������� ������ � ������ ������
    [SerializeField] private TMP_Text scoreText; //����������� ������ �� ������

    void Update()
    {
        scoreText.text = ((int)(player.position.z / 2)).ToString(); //��������� ��������� ������ �� ��� z/2
    }
}
