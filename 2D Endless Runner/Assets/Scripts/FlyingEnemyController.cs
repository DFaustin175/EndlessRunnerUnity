using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEnemyController : MonoBehaviour
{
    [HideInInspector] public Rigidbody2D rb;
    [HideInInspector] public Animator anim;
    public float speed;
    public enum EnemyState
    {
        Idle,
        Attacking,
        HitStop,
        Dead
    }
    public EnemyState enemyState;
    public Transform firePoint;
    public bool attacked;
    [SerializeField] GameObject projectile;
    [SerializeField] Transform position;
    bool inPosition;
    
    float timer;
    public float attackTimer;
    // Start is called before the first frame update
    void Start()
    {
        position = GameObject.FindGameObjectWithTag("Position").transform;
        transform.DOMove(position.position, 1);
        anim = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position == position.position) { inPosition = true; }
        if(inPosition)
        {
            if (timer < attackTimer && !attacked) { timer += Time.deltaTime; }
            else if (timer >= attackTimer)
            {
                Attack();
            }
        }
    }

    void Attack()
    {
        anim.SetTrigger("Attack");
        attacked = true;
        timer = 0;
    }

    public void SpawnProjectile()
    {
        GameObject proj = Instantiate(projectile, firePoint.position, Quaternion.Euler(0, 0, 20.825f));
        proj.GetComponent<Projectile>().firingEnemy = this.gameObject;
    }
}
