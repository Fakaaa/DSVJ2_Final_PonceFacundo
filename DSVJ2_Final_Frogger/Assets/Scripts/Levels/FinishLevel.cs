using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishLevel : MonoBehaviour
{
    [SerializeField] ManagersHandler masterManager;

    private void Start()
    {
        masterManager = FindObjectOfType<ManagersHandler>();
    }

    private void OnTriggerEnter(Collider other)
    {
        masterManager.gameMng.PlayerPassLevel();
    }
}
