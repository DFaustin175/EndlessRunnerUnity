using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    GameManager gameManager;
    [HideInInspector] public Animator anim;
    public int health;


    public Transform playerSprite;
    public Transform attackPoint;
    public float attackRange;
    public LayerMask enemyLayers;
    public float attackCooldown;
    [SerializeField] float timer;
    [SerializeField] float hitStopTimer;
    // Start is called before the first frame update
    private void Awake()
    {
        gameManager = GameObject.FindGameObjectWithTag("Manager").GetComponent<GameManager>();
    }

    void Start()
    {
        if(gameManager != null) { Debug.Log("Player Controller: Game Manager Found"); }
        anim = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(gameManager.gameState == GameManager.GameState.Playing) { anim.SetTrigger("Start"); }
        if(health <= 0) { gameManager.gameState = GameManager.GameState.GameOver; anim.SetBool("Dead", true); }
        Attack();
    }

    void Attack()
    {
        if(timer < attackCooldown) { timer += Time.deltaTime; }
        else if(timer >= attackCooldown)
        {
            if(Input.GetKeyDown(KeyCode.Space)) 
            { anim.SetTrigger("Attack"); timer = 0; }
        }
    }

    public void ActivateHitBox()
    {
        Collider2D[] hitObjs = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        foreach(Collider2D hitObj in hitObjs)
        {
            
            Debug.Log("Hit " + hitObj.gameObject.name);
            if(hitObj.gameObject.tag == "GroundEnemy")
            {
                if(hitObj.gameObject.GetComponent<GroundEnemyController>().enemyState != GroundEnemyController.EnemyState.Dead 
                    || hitObj.gameObject.GetComponent<GroundEnemyController>().enemyState != GroundEnemyController.EnemyState.Attacked)
                {
                    //if enemy kill target + HitStop
                    StartCoroutine(HitStop(.3f));
                    //Sprite Shake
                    hitObj.gameObject.GetComponent<GroundEnemyController>().spriteShake(.75f);
                    spriteShake(.5f);
                    hitObj.gameObject.GetComponent<GroundEnemyController>().anim.SetTrigger("Die");
                    CameraShaker.Invoke();
                    gameManager.score += 100;
                }
            }
            else if(hitObj.gameObject.tag == "Projectile")
            {
                hitObj.gameObject.GetComponent<Projectile>().state = Projectile.ProjectileState.PlayerDeflected;
                CameraShaker.Invoke();
                gameManager.score += 125;
            }
            
            //if projectile reflect back to sender
            //Screen Shake
        }

    }
    public IEnumerator HitStop(float magnitude)
    {
        //Stop Anim
        anim.enabled = false;
        yield return new WaitForSecondsRealtime(magnitude);
        anim.enabled = true;
    }

    public void spriteShake(float mag)
    {
        Vector2 shakeStrength = new Vector2(mag, 0);
        playerSprite.DOComplete();
        playerSprite.DOShakePosition(.5f, shakeStrength);
    }

    private void OnDrawGizmosSelected()
    { Gizmos.DrawWireSphere(attackPoint.position, attackRange); }
}
