using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnPoint : MonoBehaviour
{
    private void OnEnable()
    {
        GameObject enemyObject = EnemyObjectPool.sharedInstance.objectPool.GetPooledObject();
        if (enemyObject != null)
        {
            enemyObject.transform.position = transform.position;
            enemyObject.transform.rotation = Quaternion.identity;
            enemyObject.GetComponent<Enemy>().InitializeEnemy();
            enemyObject.SetActive(true);
        }
    }
}
