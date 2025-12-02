using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class ObstacleSpawnerScripts : MonoBehaviour
{
    private PlayerScript playerScript;
    public GameObject obstaclePrefab;
    public float spawnInterval = 2f;
    public float minSpawnInterval = 0.2f;
    public float decreasePerSecond = 0.01f;
    private float timer = 0f;
    public float currentSpawnInterval;

    public List<Transform> ObstacleSpawns = new();
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentSpawnInterval = spawnInterval;
        playerScript = GameObject.FindWithTag("Player").GetComponent<PlayerScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if(playerScript.died) return;
        timer += Time.deltaTime;
        if (timer >= currentSpawnInterval)
        {
            SpawnObstacleAtRandom();
            timer = 0f;
        }
        currentSpawnInterval = Mathf.Max(minSpawnInterval, currentSpawnInterval - decreasePerSecond * Time.deltaTime);
    }

    void SpawnObstacleAtRandom()
    {
         int idx = Random.Range(0, ObstacleSpawns.Count);
        Transform spawnPoint = ObstacleSpawns[idx];

        Instantiate(obstaclePrefab, spawnPoint.position, spawnPoint.rotation);
    }
}
