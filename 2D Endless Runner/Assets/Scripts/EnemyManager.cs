using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public enum EnemyToSpawn
    {
        Ground,
        Flying
    }
    public EnemyToSpawn enemyToSpawn;
    public GameManager gameManager;



    float timer;
    public float spawnTimer;
    public int maxEnemies;
    [SerializeField] int enemiesInScene;

    [SerializeField] GameObject groundEnemy;
    [SerializeField] GameObject flyingEnemy;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        switch (enemyToSpawn)
        {
            case EnemyToSpawn.Ground:
                enemiesInScene = GameObject.FindGameObjectsWithTag("GroundEnemy").Length;
                if(enemiesInScene < maxEnemies && gameManager.gameState == GameManager.GameState.Playing)
                {
                    if(timer < spawnTimer) { timer += Time.deltaTime; }
                    else if(timer >= spawnTimer)
                    {
                        Instantiate(groundEnemy, transform.position, Quaternion.identity);
                        spawnTimer = Random.Range(1.5f, 5);
                        timer = 0;
                    }
                }
                break;

            case EnemyToSpawn.Flying:
                enemiesInScene = GameObject.FindGameObjectsWithTag("FlyingEnemy").Length;
                if(enemiesInScene < maxEnemies && gameManager.gameState == GameManager.GameState.Playing) 
                {
                    if(timer < spawnTimer) { timer += Time.deltaTime; }
                    else if(timer >= spawnTimer)
                    {
                        Instantiate(flyingEnemy, transform.position, Quaternion.identity);
                        spawnTimer = Random.Range(3, 6);
                        timer = 0;
                    }
                }
                break;
        }
    }
}
