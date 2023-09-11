using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundEnemyController : MonoBehaviour
{
    [HideInInspector] public Rigidbody2D rb;
    [HideInInspector] public Animator anim;
    public float speed;
    public enum EnemyState
    {
        Idle,
        Attacked,
        Attacking,
        HitStop,
        Dead
    }
    public EnemyState enemyState;

    public SpriteRenderer enemySprite;
    public Transform sprite;
    [SerializeField] float hitStopTimer;
    public Transform attackPoint;
    public float attackRange;
    public LayerMask playerLayer;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (enemyState)
        {
            case EnemyState.Idle:
                rb.velocity = new Vector2(-1 * speed, 0);

                break;

            case EnemyState.Attacking:
                //attack
                rb.velocity = Vector2.zero;

                break;

            case EnemyState.HitStop:
                rb.velocity = Vector2.zero;
                break;

            case EnemyState.Dead:
                //Dead
                rb.velocity = new Vector2(-1 * speed, 0);
                break;
        }
    }

    public void ActivateHitBox()
    {
        
        Collider2D[] hitObjs = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, playerLayer);

        foreach (Collider2D hitObj in hitObjs)
        {
            Debug.Log("Hit " + hitObj.gameObject.name);
            PlayerController playerController = hitObj.gameObject.GetComponent<PlayerController>();
            playerController.anim.SetTrigger("Hurt");
            playerController.spriteShake(.75f);
            playerController.health--;
            spriteShake(.5f);
            StartCoroutine(HitStop());
            //Screen Shake
            //StartCoroutine(HitStop(playerController));
            CameraShaker.Invoke();
            enemyState = EnemyState.Attacked;
        }
    }

    public void Die()
    {
        anim.SetTrigger("Die");
        enemyState = EnemyState.Dead;
        enemySprite.sortingOrder = 1;
    }

    IEnumerator HitStop()
    {
        //Stopp Anim
        anim.enabled = false;
        yield return new WaitForSecondsRealtime(.3f);
        anim.enabled = true;
    }

    public IEnumerator HitStopDamaged(float magnitude)
    {
        //Stopp Anim
        anim.enabled = false;
        //Stop Movement
        enemyState = EnemyState.HitStop;
        yield return new WaitForSecondsRealtime(magnitude);
        enemyState = EnemyState.Dead;
        anim.enabled = true;
    }

    public void spriteShake(float mag)
    { 
        Vector2 shakeStrength = new Vector2(mag, 0);
        sprite.DOComplete();
        sprite.DOShakePosition(.5f, shakeStrength);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player" && enemyState != EnemyState.Dead)
        { enemyState = EnemyState.Attacking; anim.SetTrigger("Attack"); }
    }
}
