using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformController : MonoBehaviour
{
    [SerializeField] GameManager gameManager;
    [SerializeField] private float speed;
    [SerializeField] public Rigidbody2D rb;
    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        gameManager = GameObject.FindGameObjectWithTag("Manager").gameObject.GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.gameState == GameManager.GameState.Playing) { rb.velocity = new Vector2(-1 * speed, 0); }
        else if(gameManager.gameState == GameManager.GameState.GameOver) { rb.velocity = Vector2.zero; }
    }
}
