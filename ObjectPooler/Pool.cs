using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pool<T> where T : MonoBehaviour
{
    public T prefab { get; }
    public bool autoExpand { get; set; }
    public Transform container { get; }
    private List<T> pool;

    public Pool(T prefab, int count, Transform container, bool autoExpand)
    {
        this.prefab = prefab;
        this.container = container;
        this.autoExpand = autoExpand;
        CreatePool(count);
    }
    private void CreatePool(int count)
    {
        this.pool = new List<T>();
        for (int number = 0; number < count; number++)
        {
            CreateObject();
        }
    }
    private T CreateObject(bool isActive = false)
    {
        var createdObject = Object.Instantiate(this.prefab, this.container);
        createdObject.gameObject.SetActive(isActive);
        pool.Add(createdObject);
        return createdObject;
    }
    private bool HasFreeElement(out T element)
    {
        foreach (var Object in pool)
        {
            if (!Object.gameObject.activeInHierarchy)
            {
                element = Object;
                return true;
            }
        }
        element = null;
        return false;
    }
    public T GetFreeElement()
    {
        if (HasFreeElement(out var element))
        {
            return element;
        }
        if (autoExpand)
        {
            return CreateObject(isActive: true);
        }
        throw new System.Exception(message: $"There is no free element in pool of type {typeof(T)}");
    }
}
