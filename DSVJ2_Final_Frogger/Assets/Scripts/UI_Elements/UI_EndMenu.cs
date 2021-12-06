using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI_EndMenu : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI textStatus;
    [SerializeField] TextMeshProUGUI finalScore;
    [SerializeField] TextMeshProUGUI finalTime;
    [SerializeField] Button nextButton;
    [SerializeField] bool wasDefeat;
    [SerializeField] ManagersHandler managerGMref;

    public void SetStatusCondition(bool value)
    {
        wasDefeat = value;
    }

    public void SetStatus()
    {
        if(wasDefeat)
        {
            if(nextButton != null)
            {
                nextButton.enabled = false;
            }
            textStatus.text = "Lose";
            finalScore.text = managerGMref.gameMng.scorePlayer.ToString();
            finalTime.text = managerGMref.gameMng.timePass.ToString("0");
        }
        else
        {
            textStatus.text = "Win";
            finalScore.text = managerGMref.gameMng.scorePlayer.ToString();
            finalTime.text = managerGMref.gameMng.timePass.ToString("0");
        }
    }
}
