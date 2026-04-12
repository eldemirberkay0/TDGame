using System.Collections.Generic;
using UnityEngine;

public static class ObjectPooler
{
    public static Dictionary<GameObject, List<GameObject>> ObjectPools = new Dictionary<GameObject, List<GameObject>>();

    public static void CreatePool(GameObject obj, int count, Transform container = null)
    {
        if (ObjectPools.ContainsKey(obj)) { return; }
        List<GameObject> newPool = new List<GameObject>();
        ObjectPools.Add(obj, newPool);
        for (int i = 0; i < count; i++)
        {
            GameObject pooledObject = Object.Instantiate(obj, parent: container);
            pooledObject.SetActive(false);
            newPool.Add(pooledObject);
        }
    }

    public static GameObject GetObject(GameObject obj)
    {
        List<GameObject> poolToSearch = ObjectPools[obj];
        for (int i = 0; i < poolToSearch.Count; i++)
        {
            if (!poolToSearch[i].activeInHierarchy) { return poolToSearch[i]; }
        }
        return Object.Instantiate(obj);
    }

    public static void ClearPool() => ObjectPools.Clear();
}
