using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public GameObject enemy;
    public GameObject coin;
    
    [SerializeField] private GameObject[] pumpkins;
    [SerializeField] private GameObject[] grass;

    [SerializeField] private int minEnemyCount;
    [SerializeField] private int maxEnemyCount;

    [SerializeField] private float coinTimer;
    [SerializeField] private float resetCoinTimer = 3f;

    [SerializeField] private float enemyTimer;
    [SerializeField] private float resetEnemyTimer = 3f;

    void Start()
    {
        coinTimer = resetCoinTimer;
        enemyTimer = resetEnemyTimer;

        InitializeEnemies();
        InitializeBgPumpkings();
        InitializeBgGrass();
    }

    // Update is called once per frame
    void Update()
    {
        coinTimer -= 1f * Time.deltaTime;
        if(coinTimer <= 0)
        {
            MakeCoin();
            resetCoinTimer = Random.Range(0.5f,1.5f);
            coinTimer = resetCoinTimer;
        }

        enemyTimer -= 1f * Time.deltaTime;
        if(enemyTimer <= 0)
        {
            MakeEnemy();
            resetEnemyTimer = Random.Range(0.5f,1.5f);
            enemyTimer = resetEnemyTimer;
        }


    }

    void InitializeEnemies()
    {
        int enemyCount = Random.Range(minEnemyCount, maxEnemyCount);
        for (int i = 0; i <enemyCount; i++)
        {
            MakeEnemy();
        }     
    }

    void InitializeBgPumpkings()
    {
        int bgElementCount = Random.Range(3,5);

        for(int i = 0; i <bgElementCount; i++)
        {
            int whoPumpkin = Random.Range(0,3);
            Instantiate(pumpkins[whoPumpkin], GetRandomRange(), Quaternion.identity);
        }
    }

    void InitializeBgGrass()
    {
        int bgElementCount = Random.Range(14,20);

        for(int i = 0; i <bgElementCount; i++)
        {
            int whoGrass = Random.Range(0,19);
            
            float xPos = Random.Range(-9,9);
            float yPos = Random.Range(-5,5);
            Vector2 makePosition = new Vector2(xPos,yPos);

            Instantiate(grass[whoGrass], makePosition, Quaternion.identity);
        }
    }

    void MakeCoin()
    {
        float xPos = Random.Range(-8.8f,8.8f);
        float yPos = Random.Range(-5,5);
        Vector3 randPosition = new Vector3(xPos,yPos,0);

        Instantiate(coin, randPosition, Quaternion.identity);
    }

    void MakeEnemy()
    {
        Instantiate(enemy, GetRandomRange(), Quaternion.identity);
    }

    Vector2 GetRandomRange()
    {
        //Define a range that EXCLUDES -1 to 1
        float xPos = Random.Range(1,9);
        float yPos = Random.Range(1,5);

        float makeNegativeX = 1;
        float makeNegativeY = 1;
        
        if(Random.Range(0,1f) > 0.5f) {makeNegativeX = -1f;}
        if(Random.Range(0,1f) > 0.5f) {makeNegativeY = -1f;}            

        return new Vector2(xPos * makeNegativeX, yPos * makeNegativeY);
    }
}
