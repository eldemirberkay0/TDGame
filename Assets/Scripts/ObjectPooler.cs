using System.Collections.Generic;
using UnityEngine;

public static class ObjectPooler
{
    public static Dictionary<Transform, List<GameObject>> ObjectPools = new Dictionary<Transform, List<GameObject>>();

    public static void CreatePool(GameObject obj, int count, Transform container)
    {
        List<GameObject> newPool = new List<GameObject>();
        ObjectPools.Add(container, newPool);
        for (int i = 0; i < count; i++)
        {
            GameObject pooledObject = Object.Instantiate(obj, container.position, container.rotation, container);
            pooledObject.SetActive(false);
            newPool.Add(pooledObject);
        }
    }

    public static GameObject GetObject(Transform container)
    {
        List<GameObject> poolToSearch = ObjectPools[container];
        for (int i = 0; i < poolToSearch.Count; i++)
        {
            if (!poolToSearch[i].activeInHierarchy) { return poolToSearch[i]; }
        }
        return null;
    }
}
