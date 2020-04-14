using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[AddComponentMenu("Pool/ObjectPooling")]
public class ObjectPooling
{
    private List<PoolObject> _objectsPool;
    private Transform _transformObjectsParent;

    /// <summary>
    /// Добавление экземпляра объекта в пул
    /// </summary>
    /// <param name="sample"></param>
    /// <param name="objects_parent"></param>
    private void AddObject(PoolObject sample, Transform objects_parent)
    {
        GameObject temp = GameObject.Instantiate(sample.gameObject);
        temp.name = sample.name;
        temp.transform.SetParent(objects_parent);
        _objectsPool.Add(temp.GetComponent<PoolObject>());
        temp.SetActive(false);
    }

    /// <summary>
    /// Создаём стартовый набор экземпляров объектов
    /// </summary>
    /// <param name="count"></param>
    /// <param name="sample"></param>
    /// <param name="objects_parent"></param>
    public void Initialize(int count, PoolObject sample, Transform objects_parent)
    {
        _objectsPool = new List<PoolObject>();
        _transformObjectsParent = objects_parent; 
        for (int i = 0; i < count; i++)
        {
            AddObject(sample, objects_parent);
        }
    }

    /// <summary>
    /// Получить свободный объект из пула
    /// </summary>
    /// <returns></returns>
    public PoolObject GetObject()
    {
        for (int i = 0; i < _objectsPool.Count; i++)
        {
            if (_objectsPool[i].gameObject.activeInHierarchy == false)
            {
                return _objectsPool[i];
            }
        }
        AddObject(_objectsPool[0], _transformObjectsParent);
        return _objectsPool[_objectsPool.Count - 1];
    }
}
