using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingEnemy : Enemy
{
    [SerializeField] float shootingInterval;
    [SerializeField] Transform shootSocket;
    float timer = 0;

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= shootingInterval)
        {
            GameObject enemyProjectile = EnemyProjectileObjectPool.sharedInstance.GetPooledObject();
            if (enemyProjectile != null)
            {
                enemyProjectile.transform.position = shootSocket.position;
                enemyProjectile.transform.rotation = Quaternion.identity;
                enemyProjectile.SetActive(true);
            }
            timer = 0;
        }
    }
}
