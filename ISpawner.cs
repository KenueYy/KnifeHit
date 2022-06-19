using UnityEngine;

public interface ISpawner
{
    public void PreparationPool(PoolObject PoolData);
    public GameObject SpawnObject(PoolObject PoolData);
    public void DispawnAll();

}
