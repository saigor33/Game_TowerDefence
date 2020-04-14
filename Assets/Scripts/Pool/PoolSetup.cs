using UnityEngine;
using System.Collections;

[AddComponentMenu("Pool/PoolSetup")]
public class PoolSetup : MonoBehaviour
{
    [SerializeField]
    private PoolManager.PoolPart[] pools;


    private void Awake()
    {
        Initialize();
    }

    private void Initialize()
    {
        PoolManager.Initialize(pools);
    }

}