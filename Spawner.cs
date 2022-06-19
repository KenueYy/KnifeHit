using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour, ISpawner
{
    [SerializeField] Transform ObjectPoolContainer;
    private Dictionary<string, GameObjectPool> pools;
    public Spawner()
    {
        pools = new Dictionary<string, GameObjectPool>();
    }
    public void PreparationPool(PoolObject PoolData)
    {
        GameObjectPool pool = new GameObjectPool(PoolData.prefab, PoolData.poolCount, ObjectPoolContainer, PoolData.autoExpand);
        pools.Add(PoolData.prefab.name,pool);
    }
    public GameObject SpawnObject(PoolObject PoolData)
    {
        pools.TryGetValue(PoolData.prefab.name, out GameObjectPool pool);
        GameObject poolObject = pool.GetFreeElement();
        poolObject.transform.position = transform.position;
        poolObject.SetActive(true);
        return poolObject;
    }
    public void DispawnAll()
    {
        foreach (var pool in pools)
        {
            List<GameObject> dispawnElements = pool.Value.GetBusyElements();
            foreach (GameObject element in dispawnElements)
            {
                element.transform.position = ObjectPoolContainer.transform.position;
                element.transform.parent = ObjectPoolContainer.transform;
                element.transform.rotation = new Quaternion(0,0,0,0);
                element.SetActive(false);
            }
        }
    }
}
