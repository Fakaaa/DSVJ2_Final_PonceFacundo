using UnityEngine;

public class ManagersHandler : MonoBehaviour
{
    public SceneLoader sceneMng;
    public GameManager gameMng;

    void Start()
    {
        sceneMng = FindObjectOfType<SceneLoader>();
        gameMng = FindObjectOfType<GameManager>();
    }

    #region HANDLER_UTILITIES

    public bool CheckIfSceneMngIsNull()
    {
        if (sceneMng == null)
        {
            Debug.LogError("SceneManager is Null. Can´t exceute the method. Error at " + gameObject.name);
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool CheckIfGameMngIsNull()
    {
        if (gameMng == null)
        {
            Debug.LogError("GameManager is Null. Can´t exceute the method. Error at " + gameObject.name);
            return true;
        }
        else
        {
            return false;
        }
    }

    #endregion

    #region SCENE_MANAGER

    public void LoadScene(string sceneName)
    {
        if (CheckIfSceneMngIsNull())
            return;

        sceneMng.LoadSceneAsyncWithProgressBar(sceneName);
    }

    public void QuitfGame()
    {
        if (CheckIfSceneMngIsNull())
            return;

        sceneMng.QuitApplication();
    }

    #endregion

    //===================================================
    
    #region GAME_MANAGER

    public void ActivatePause()
    {
        if (CheckIfGameMngIsNull())
            return;

        gameMng.IsGamePause = !gameMng.IsGamePause;
    }

    public void ResetDataGameMngr()
    {
        if (CheckIfGameMngIsNull())
            return;

        gameMng.ResetData();
    }

    #endregion
}
