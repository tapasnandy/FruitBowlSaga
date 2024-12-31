using UnityEngine;
using System.Collections.Generic;
using TMPro;


public class GameManager : MonoBehaviour
{
    [SerializeField] float spawnPosX;
    [SerializeField] float spawnPosY;
    [SerializeField] float spawnDelay;
    [SerializeField] float totalgameTime;
    private float timer=0f;
    [SerializeField] TextMeshProUGUI gameTimerTxt;
    [SerializeField] CollideDetectionFloatingScoreTxt CollideDetectionFloatingScoreTxt;
    

    [Header("Spawn asset prefabs")]
    [SerializeField] GameObject carrotPrefab;
    [SerializeField] GameObject guavaPrefab;
    [SerializeField] GameObject watermelonPrefab;
    [SerializeField] GameObject strawberryPrefab;
    [SerializeField] GameObject pineapplePrefab;
    [SerializeField] GameObject bombPrefab;

    [Header("Spawn probabilities in percentage")]
    [SerializeField] float carrotProbability;
    [SerializeField] float guavaProbability;
    [SerializeField] float watermelonProbability;
    [SerializeField] float strawberryProbability;
    [SerializeField] float pineappleProbability;
    [SerializeField] float bombProbability;

    [Header("Speed")]
    [SerializeField] float speed;
    [SerializeField] float increaseSpeedRatio;
    [SerializeField] float maxSpeed;
    [SerializeField] float increaseSpeedDelayTime;
    private float delayTimer;

    [Header("GameOver Panel")]
    [SerializeField] GameObject gameoverPanel;
    [SerializeField] TextMeshProUGUI gameoverScoreTxt;
    [SerializeField] TextMeshProUGUI gameoverTimeTxt;
    string formattedTime;



    private void Start()
    {
        InvokeRepeating("SpawnPrefabs", 1f, spawnDelay);

        delayTimer = increaseSpeedDelayTime;
        gameoverPanel.SetActive(false);
    }

    private void Update()
    {
        timer += Time.deltaTime;
        delayTimer -= Time.deltaTime;


        if(timer <= totalgameTime && CollideDetectionFloatingScoreTxt.count < 4)
        {
            int minutes = (int)(timer / 60);
            int seconds = (int)(timer % 60);
            int milliseconds = (int)((timer * 100) % 100);
            formattedTime = string.Format("{0:00}:{1:00}:{2:00}", minutes, seconds, milliseconds);
            gameTimerTxt.text = formattedTime;
        }

        

        if (timer > totalgameTime || CollideDetectionFloatingScoreTxt.count >=4)
        {
            CancelInvokePrefabs();
            GameOverPanel();
        }

        if (delayTimer <= 0 && timer <= totalgameTime)
        { 
            speed += increaseSpeedRatio;
            delayTimer = increaseSpeedDelayTime;
        }

    }

    void GameOverPanel()
    {
        gameoverPanel.SetActive(true);
        gameoverScoreTxt.text = CollideDetectionFloatingScoreTxt.totalScore.ToString();
        gameoverTimeTxt.text = formattedTime;

        GameObject.Find("Player").SetActive(false);
    }



    public void CancelInvokePrefabs()
    {
        CancelInvoke("SpawnPrefabs");
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
        else if (randomProbability < carrotProbability + guavaProbability)
        {
            prefabToSpawn = guavaPrefab;
        }
        else if (randomProbability < carrotProbability + guavaProbability + watermelonProbability)
        {
            prefabToSpawn = watermelonPrefab;
        }
        else if (randomProbability < carrotProbability + guavaProbability + watermelonProbability + strawberryProbability)
        {
            prefabToSpawn = strawberryPrefab;
        }
        else if (randomProbability < carrotProbability + guavaProbability + watermelonProbability + strawberryProbability + pineappleProbability)
        {
            prefabToSpawn = pineapplePrefab;
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



