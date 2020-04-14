using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace TaskGame_TowerDefence
{
    /// <summary>
    /// Полный перечеь уровней противника
    /// </summary>
    [CreateAssetMenu(fileName = "FullEnemyModelSO", menuName = "TaskGame_TowerDefence/Enemy/FullEnemyModelSO")]
    public class FullEnemyModelSO : ScriptableObject
    {
        public List<EnemyModelLevelSO> listLavelModels;
    }
}
