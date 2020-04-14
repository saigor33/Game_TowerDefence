using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class PoolManager
{
    private static PoolPart[] _pools;
    private static GameObject _objectsParent;


    [System.Serializable]
    public struct PoolPart
    {
        public int indexPref; 
        public PoolObject prefab; 
        public int count; 
        public ObjectPooling objectWorkingPool; 
    }

    /// <summary>
    /// Инициализация менеджера пула, в котором будут хранится все создаваемые объекты
    /// </summary>
    /// <param name="newPools"></param>
    public static void Initialize(PoolPart[] newPools)
    {
        _pools = newPools; 
        _objectsParent = new GameObject();
        _objectsParent.name = "Pool"; 
        for (int i = 0; i < _pools.Length; i++)
        {
            if (_pools[i].prefab != null)
            {
                _pools[i].objectWorkingPool = new ObjectPooling(); 
                _pools[i].objectWorkingPool.Initialize(_pools[i].count, _pools[i].prefab, _objectsParent.transform);
            }
        }
    }

    /// <summary>
    /// Получить объект из пула и активировать его
    /// </summary>
    /// <param name="indexPref"></param>
    /// <param name="position"></param>
    /// <param name="rotation"></param>
    /// <returns></returns>
    public static GameObject GetObject(int indexPref, Vector3 position, Quaternion rotation)
    {
        GameObject result = null;
        if (_pools != null)
        {
            for (int i = 0; i < _pools.Length; i++)
            {
                if (_pools[i].indexPref == indexPref)
                { 
                    result = _pools[i].objectWorkingPool.GetObject().gameObject; 
                    result.transform.position = position;
                    result.transform.rotation = rotation;
                    result.SetActive(true); 
                    return result;
                }
            }
        }

        Debug.LogError($"Данный префаб не добавлен в Pool. Тип префаба = {indexPref}");
        return result;
    }
}

