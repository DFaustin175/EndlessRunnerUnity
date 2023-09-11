using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimEvents : MonoBehaviour
{
    [SerializeField] PlayerController player;
    [SerializeField] GroundEnemyController groundEnemy;
    [SerializeField] FlyingEnemyController flyingEnemy;
    // Start is called before the first frame update
    void Start()
    {
        player = GetComponentInParent<PlayerController>();
        groundEnemy = GetComponentInParent<GroundEnemyController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayerHitStop(float mag)
    { StartCoroutine(player.HitStop(mag)); }

    public void EnemyHitStop(float mag)
    { StartCoroutine(groundEnemy.HitStopDamaged(mag)); }

    public void HitBox()
    { player.ActivateHitBox(); }

    public void groundHitBox()
    { groundEnemy.ActivateHitBox();  }

    public void returnToIdleGround()
    { groundEnemy.enemyState = GroundEnemyController.EnemyState.Idle; }

    public void DestroyMe()
    {
        Destroy(gameObject);
    }
}
