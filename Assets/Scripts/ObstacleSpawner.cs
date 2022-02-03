using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{

    public GameObject[] obstacles;

    public float minSpawnY;
    public float maxSpawnY;
    private float leftSpawnX;
    private float rightSpawnX;

    public float spawnRate;
    private float lastSpawn;

    private List<GameObject> pooledObstacles = new List<GameObject>();
    private int initialPoolSize = 9999;

    // Start is called before the first frame update
    void Start()
    {
        Camera cam = Camera.main;
        float camWidth = (2.0f * cam.orthographicSize) * cam.aspect;

        leftSpawnX = -camWidth / 2;
        rightSpawnX = camWidth / 2;

        CreateInitialPool();
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time - spawnRate >= lastSpawn)
        {
            lastSpawn = Time.time;
            SpawnObstacle();
        }
    }

    void CreateInitialPool()
    {
        for(int index = 0; index < initialPoolSize; index++)
        {
            GameObject obstacleToSpawn = obstacles[index % 4];

            GameObject spawnedObject = Instantiate(obstacleToSpawn);

            pooledObstacles.Add(spawnedObject);

            spawnedObject.SetActive(false);
        }
    }

    GameObject GetPooledObstacle()
    {
        GameObject pooledObj = null;

        foreach(GameObject obj in pooledObstacles)
        {
            if (!obj.activeInHierarchy)
                pooledObj = obj;
        }

        if (!pooledObj)
            Debug.LogError("Pool size not big enough!");

        pooledObj.SetActive(true);

        return pooledObj;
    }

    void SpawnObstacle()
    {
        GameObject obstacle = GetPooledObstacle();

        obstacle.transform.position = GetSpawnedPosition();

        obstacle.GetComponent<Obstacle>().moveDir = new Vector3(obstacle.transform.position.x > 0 ? -1 : 1, 0, 0);
    }

    Vector3 GetSpawnedPosition()
    {
        float x = Random.Range(0, 2) == 1 ? leftSpawnX : rightSpawnX;
        float y = Random.Range(minSpawnY, maxSpawnY);

        return new Vector3(x, y, 0);
    }
}
