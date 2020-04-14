using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[AddComponentMenu("Pool/PoolObject")]
public class PoolObject : MonoBehaviour
{
    /// <summary>
    /// Возвращение объекта в пулл (отлючение его)
    /// </summary>
    public void ReturnToPool()
    {
        gameObject.SetActive(false);
    }
}