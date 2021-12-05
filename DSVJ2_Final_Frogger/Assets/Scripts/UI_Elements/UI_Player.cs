using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI_Player : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI lives;
    [SerializeField] TextMeshProUGUI score;
    [SerializeField] TextMeshProUGUI time;

    GameManager gmReference;

    void Start()
    {
        gmReference = FindObjectOfType<GameManager>();

        if(gmReference != null)
        {
            gmReference.onUIUpdate += UpdateUIPlayer;
        }
    }

    public void UpdateUIPlayer()
    {
        if (gmReference == null)
            return;

        lives.text = "Lives: " + gmReference.remainingLives;
        score.text = "Score: " + gmReference.scorePlayer;
        time.text = gmReference.timePass.ToString("0");
    }
}
