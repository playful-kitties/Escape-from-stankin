using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Score : MonoBehaviour
{
    [SerializeField] private Transform player; //положение игрока в данный момент
    [SerializeField] private TMP_Text scoreText; //отображение текста на экране

    void Update()
    {
        scoreText.text = ((int)(player.position.z / 2)).ToString(); //выведение положения игрока по оси z/2
    }
}
