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
    [SerializeField] UI_EndMenu endMenu;

    GameManager gmReference;

    void Start()
    {
        gmReference = FindObjectOfType<GameManager>();

        if(gmReference != null)
        {
            gmReference.onUIUpdate += UpdateUIPlayer;
            gmReference.onEndLevel += EnableEndScreen;
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

    public void EnableEndScreen(bool wasDefeat)
    {
        endMenu.SetStatusCondition(wasDefeat);
        endMenu.SetStatus();
        endMenu.gameObject.SetActive(true);
    }
}
