using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] List<Level> levels;
    [SerializeField] List<Transform> respawnCheckPoints;
    [SerializeField] Level actualLevel;

    private ManagersHandler masterManager;

    void Start()
    {
        masterManager = FindObjectOfType<ManagersHandler>();

        if(masterManager.gameMng != null)
        {
            for (int i = 0; i < levels.Count; i++)
            {
                if(levels[i] != null)
                {
                    if(levels[i].levelData.id == masterManager.gameMng.idActualLvl)
                    {
                        actualLevel = levels[i];
                    }
                }
            }
        }

    }

    void Update()
    {
        
    }
}
