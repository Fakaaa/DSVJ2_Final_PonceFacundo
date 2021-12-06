using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviourSingleton<GameManager>
{
    
    public bool IsGamePause
    {
        get
        {
            return paused;
        }
        set
        {
            paused = value;
        }
    }

    [Space(10),Header("Crucial Information")]
    [SerializeField] bool paused;
    [SerializeField] public int remainingLives;
    [SerializeField] public int scorePlayer;
    [SerializeField] public float timePass;

    [SerializeField] FrogMovement theFrogMovement;
    [SerializeField] Frog theFrogInteractions;

    public int idActualLvl = 1;
    public UnityAction onUIUpdate;
    public UnityAction<bool> onEndLevel;

    void Update()
    {
        FindFrog();

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            IsGamePause = !IsGamePause;
            
            if(theFrogMovement != null)
            {
                theFrogMovement.AvoidPlayerJumpOnPause();
            }
        }

        CheckPause();

        CalculateTime();
    }
    public void ResetData()
    {
        remainingLives = 3;
        scorePlayer = 0;
        timePass = 0;
    }

    void FindFrog()
    {
        if(theFrogMovement == null)
        {
            theFrogMovement = FindObjectOfType<FrogMovement>();
        }
        if(theFrogInteractions == null)
        {
            theFrogInteractions = FindObjectOfType<Frog>();
            if(theFrogInteractions != null)
            {
                theFrogInteractions.SetRemainingLives(remainingLives);
                theFrogInteractions.onFrogDeath += PlayerLosesLife;

                theFrogInteractions.onFrogLose += PlayerLoseGame;

                theFrogInteractions.onFrogSmashed += PlayerCantMove;
                theFrogInteractions.onFrogDeath += PlayerCanMove;
            }
        }
    }

    void CheckPause()
    {
        if (IsGamePause)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }

    void PlayerLosesLife()
    {
        remainingLives--;
        theFrogInteractions.SetRemainingLives(remainingLives);
    }

    void PlayerCantMove()
    {
        theFrogMovement.AvoidPlayerMoveOnDie();
    }

    void PlayerCanMove()
    {
        theFrogMovement.ActivatePlayerMove();
    }

    void CalculateTime()
    {
        timePass += Time.deltaTime;

        onUIUpdate?.Invoke();
    }
    
    void PlayerLoseGame()
    {
        onEndLevel?.Invoke(true);
    }

    public void PlayerPassLevel()
    {
        onEndLevel?.Invoke(false);
    }

    public bool SetNextLevel()
    {
        if (idActualLvl < 3)
        {
            idActualLvl++;
            return true;
        }
        else
        {
            Debug.Log("Terminaste todos los levels");
            return false;
        }
    }
}
