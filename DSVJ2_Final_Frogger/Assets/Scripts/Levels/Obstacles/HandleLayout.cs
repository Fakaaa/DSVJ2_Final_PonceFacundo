using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleLayout : MonoBehaviour
{
    [SerializeField] ObstaclesPool myObstaclesPool;
    [SerializeField] public int maximumCarsAtSameTime;
    [SerializeField] List<CarBehaviour> activeCars;
    [SerializeField] public float timeBetweenCars;
    private float t = 0;

    void Start()
    {
        activeCars = new List<CarBehaviour>();
    }

    void Update()
    {
        GetSomeCarsRide();

        DriveActiveCars();
    }

    public void GetSomeCarsRide()
    {
        if (myObstaclesPool == null)
            return;

        if (t < timeBetweenCars)
        {
            t += Time.deltaTime;
            return;
        }

        if (myObstaclesPool.obstaclesActive < maximumCarsAtSameTime)
        {
            if(activeCars.Count < maximumCarsAtSameTime)
            {
                CarBehaviour carFromPool = myObstaclesPool.GetObstacleFromPool();
                carFromPool.gameObject.SetActive(true);
                activeCars.Add(carFromPool);
                t = 0;
            }
        }
    }

    public void DriveActiveCars()
    {
        if (activeCars.Count < 1)
            return;

        for (int i = 0; i < activeCars.Count; i++)
        {
            if(activeCars[i] != null)
            {
                if(!activeCars[i].travelDone)
                {
                    activeCars[i].MakeRide();
                }
                else
                {
                    activeCars[i].ResetCar();
                    myObstaclesPool.AddToPool(activeCars[i]);
                    activeCars.RemoveAt(i);
                }
            }
        }
    }
}
