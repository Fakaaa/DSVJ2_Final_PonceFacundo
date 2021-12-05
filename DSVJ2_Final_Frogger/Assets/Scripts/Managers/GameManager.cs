using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviourSingleton<GameManager>
{
    [SerializeField] List<Level> levels;
    
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
    [SerializeField] Level actualLevel;
    [SerializeField] public int remainingLives;
    [SerializeField] public int scorePlayer;
    [SerializeField] public float timePass;

    [SerializeField] FrogMovement theFrogMovement;
    [SerializeField] Frog theFrogInteractions;

    public UnityAction onUIUpdate;

    void Start()
    {
        actualLevel = levels[0];
    }

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

    private void OnDisable()
    {
        theFrogInteractions.onFrogDeath -= PlayerLosesLife;
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
            theFrogInteractions.SetRemainingLives(remainingLives);
            theFrogInteractions.onFrogDeath += PlayerLosesLife;

            theFrogInteractions.onFrogLose += PlayerLoseGame;
            
            theFrogInteractions.onFrogSmashed += PlayerCantMove;
            theFrogInteractions.onFrogDeath += PlayerCantMove;
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

    void CalculateTime()
    {
        timePass += Time.deltaTime;

        onUIUpdate?.Invoke();
    }
    
    void PlayerLoseGame()
    {

    }
}
