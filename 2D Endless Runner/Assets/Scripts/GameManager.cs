using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public enum GameState
    {
        Idle,
        Playing,
        Paused,
        GameOver
    }
    public GameState gameState;

    public int score;

    [SerializeField] GameObject StartMenu;
    [SerializeField] GameObject GameOverScreen;


    [SerializeField] PlayerController player;
    public Image[] health;
    public Sprite[] fullHealth;
    public Sprite[] emptyHealth;
    public TextMeshProUGUI scoreText;
    float timer;
    public float scoreTimer;


    // Start is called before the first frame update
    void Awake()
    {
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        UIControl();
        switch (gameState)
        {
            case GameState.Idle:
                Time.timeScale = 1;
                break; 
            
            case GameState.Playing:
                Time.timeScale = 1;
                if(timer < scoreTimer) { timer += Time.deltaTime; }
                else { score += 50; timer = 0; }
                break;

            case GameState.Paused:
                Time.timeScale = 0;
                break;

            case GameState.GameOver:
                Time.timeScale = 1;
                GameObject[] groundEnemies = GameObject.FindGameObjectsWithTag("GroundEnemy");
                GameObject[] flyingEnemies = GameObject.FindGameObjectsWithTag("FlyingEnemy");
                GameObject[] projectiles = GameObject.FindGameObjectsWithTag("Projectile");
                foreach(var enemy in groundEnemies)
                {
                    Destroy(enemy.gameObject);
                }                
                
                foreach(var enemy in flyingEnemies)
                {
                    Destroy(enemy.gameObject);
                }                
                foreach(var enemy in projectiles)
                {
                    Destroy(enemy.gameObject);
                }
                GameOverScreen.SetActive(true);
                break;
        }
    }

    void UIControl()
    {
        for(int i =0; i < health.Length; i++)
        {
            if(i < player.health)
            { health[i].sprite = fullHealth[i]; }
            else { health[i].sprite = emptyHealth[i]; }
        }

        scoreText.text = score.ToString();
    }


    public void StartGame()
    {
        gameState = GameState.Playing;
        StartMenu.SetActive(false);
        GameOverScreen.SetActive(false);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
