using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TaskGame_TowerDefence
{
    /// <summary>
    /// Параметры уровня противника
    /// </summary>
    [CreateAssetMenu(fileName = "EnemyModelLevelSO", menuName = "TaskGame_TowerDefence/Enemy/EnemyModelLevelSO")]
    public class EnemyModelLevelSO : ScriptableObject
    {
        public Sprite spriteEnemy;
        public EnemyParametrs enemyParametrs;
    }

    [Serializable]
    public struct EnemyParametrs
    {
        public int priceKill;
        public int healthPoints;
        public float speedMovingStart;
        public int damage;
    }

}

