using UnityEngine;
using System.Collections.Generic;


public class GameManager : MonoBehaviour
{
    [SerializeField] float spawnPosX;
    [SerializeField] float spawnPosY;
    [SerializeField] float spawnDelay;
    [SerializeField] float totalgameTime;
    

    [Header("Spawn asset prefabs")]
    [SerializeField] GameObject carrotPrefab;
    [SerializeField] GameObject cauliflowerPrefab;
    [SerializeField] GameObject chickenPrefab;
    [SerializeField] GameObject bombPrefab;

    [Header("Spawn probabilities in percentage")]
    [SerializeField] float carrotProbability;
    [SerializeField] float cauliflowerProbability;
    [SerializeField] float chickenProbability;
    [SerializeField] float bombProbability;

    [Header("Speed")]
    [SerializeField] float speed;
    [SerializeField] float increaseSpeedRatio;
    [SerializeField] float maxSpeed;
    [SerializeField] float increaseSpeedDelayTime;
    private float delayTimer;


    private void Start()
    {
        InvokeRepeating("SpawnPrefabs", 1f, spawnDelay);

        delayTimer = increaseSpeedDelayTime;
    }

    private void Update()
    {
        totalgameTime -= Time.deltaTime;
        delayTimer -= Time.deltaTime;

        if(totalgameTime <= 0)
        {
            CancelInvoke("SpawnPrefabs");
        }

        if (delayTimer <= 0 && totalgameTime > 0)
        { 
            speed += increaseSpeedRatio;
            delayTimer = increaseSpeedDelayTime;
        }

    }


    void SpawnPrefabs()
    {
        Vector2 randomPos = new Vector2(Random.Range(-spawnPosX, spawnPosX), spawnPosY);
        float randomProbability = Random.Range(0, 1f);

        GameObject spawnPrefab = null;
        GameObject prefabToSpawn = null;

        if (randomProbability < carrotProbability)
        {
            prefabToSpawn = carrotPrefab;
        }
        else if (randomProbability < carrotProbability + cauliflowerProbability)
        {
            prefabToSpawn = cauliflowerPrefab;
        }
        else if (randomProbability < carrotProbability + cauliflowerProbability + chickenProbability)
        {
            prefabToSpawn = chickenPrefab;
        }
        else
        {
            prefabToSpawn = bombPrefab;
        }

        spawnPrefab = Instantiate(prefabToSpawn, randomPos, Quaternion.identity);

        Rigidbody2D rb = spawnPrefab.GetComponent<Rigidbody2D>();
        if(rb != null)
        {
            
            rb.gravityScale = speed;

        }

    }
}



