using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public enum ProjectileState
    {
        EnemyFired,
        PlayerDeflected
    }
    public ProjectileState state;
    public LayerMask playerMask;
    public LayerMask enemyMask;
    public float speed;
    public float attackRange;
    [HideInInspector] public GameObject firingEnemy;
    [SerializeField] GameObject explode;
    GameObject player;
    SpriteRenderer sprite;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        HitBox();
        switch (state)
        {
            case ProjectileState.EnemyFired:
                speed = 2.5f;
                transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
                break;

            case ProjectileState.PlayerDeflected:
                speed = 4.75f;
                transform.position = Vector2.MoveTowards(transform.position, firingEnemy.transform.position, speed * Time.deltaTime);
                sprite.flipX = true;
                sprite.color = Color.white;
                firingEnemy.GetComponent<FlyingEnemyController>().anim.SetBool("Screaming", true);
                break;

        }
        
    }

    void HitBox()
    {
        Collider2D[] player = Physics2D.OverlapCircleAll(transform.position, attackRange, playerMask);
        Collider2D[] enemy = Physics2D.OverlapCircleAll(transform.position, attackRange, enemyMask);
        if(state == ProjectileState.EnemyFired)
        {
            foreach (Collider2D playerObj in player)
            {
                Instantiate(explode, transform.position, Quaternion.identity);
                playerObj.gameObject.GetComponent<PlayerController>().anim.SetTrigger("Hurt");
                playerObj.gameObject.GetComponent<PlayerController>().spriteShake(.75f);
                playerObj.gameObject.GetComponent<PlayerController>().health--;
                firingEnemy.GetComponent<FlyingEnemyController>().attacked = false;
                Destroy(this.gameObject);
            }
        }
        else if(state == ProjectileState.PlayerDeflected)
        {
            foreach(Collider2D enemyObj in enemy)
            {
                if(enemyObj.gameObject == firingEnemy.gameObject)
                {
                    Instantiate(explode, transform.position, Quaternion.identity);
                    Destroy(enemyObj.gameObject);
                    Destroy(this.gameObject);
                }
            }
        }

    }


    private void OnDrawGizmosSelected()
    { Gizmos.DrawWireSphere(transform.position, attackRange); }
}
