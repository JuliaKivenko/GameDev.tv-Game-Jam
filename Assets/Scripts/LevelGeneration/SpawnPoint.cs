using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SpawnPoint : MonoBehaviour
{
    public void SpawnLevelObject<T>(T objectPool) where T : ObjectPool
    {
        GameObject levelObject = objectPool.GetPooledObject();
        if (levelObject != null)
        {
            levelObject.transform.position = transform.position;
            levelObject.transform.rotation = Quaternion.identity;
            levelObject.SetActive(true);
        }
    }

    public abstract void SpawnObjectFromPool();
}
