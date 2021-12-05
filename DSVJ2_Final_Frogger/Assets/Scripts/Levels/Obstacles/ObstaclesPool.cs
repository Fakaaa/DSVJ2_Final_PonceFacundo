using System.Collections.Generic;
using UnityEngine;

public class ObstaclesPool : MonoBehaviour
{
    [SerializeField] List<GameObject> respawnPoints;
    [SerializeField] List<GameObject> targetPoints;
    [SerializeField] List<CarBehaviour> prefabsObstacles;
    public int poolSize;
    public int obstaclesActive;

    Queue<CarBehaviour> obstaclesPool = new Queue<CarBehaviour>();

    public bool firstGrow = false;
    public int lastRandomRespawn = 0;

    private void Start()
    {
        GrowPool();
    }

    public void GrowPool()
    {
        int randomPerPrefab = 0;

        for (int i = 0; i < poolSize; i++)
        {
            randomPerPrefab = Random.Range(0, prefabsObstacles.Count);

            var instanceToAdd = Instantiate(prefabsObstacles[randomPerPrefab]);

            AddToPool(instanceToAdd);
        }

        firstGrow = true;
    }

    public void AddToPool(CarBehaviour obstacle)
    {
        int randomSpawnPosition = 0;
        do
        {
            randomSpawnPosition = Random.Range(0, respawnPoints.Count);

        } while (randomSpawnPosition == lastRandomRespawn);

        obstacle.gameObject.SetActive(false);

        if(firstGrow)
            obstaclesActive--;

        obstacle.SetInitialPosition(respawnPoints[randomSpawnPosition].transform);
        obstacle.SetTargetDestiny(targetPoints[randomSpawnPosition].transform);
        lastRandomRespawn = randomSpawnPosition;

        obstacle.transform.position = respawnPoints[randomSpawnPosition].transform.position;
        obstaclesPool.Enqueue(obstacle);
    }

    public CarBehaviour GetObstacleFromPool()
    {
        if (obstaclesPool.Count == 0)
            GrowPool();

        var instance = obstaclesPool.Dequeue();

        obstaclesActive++;

        if (instance != null)
            return instance;
        else
            return null;
    }
}
