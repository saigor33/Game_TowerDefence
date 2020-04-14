using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TaskGame_TowerDefence
{
    /// <summary>
    /// Полный перечень уровней модели башни
    /// </summary>
    [CreateAssetMenu(fileName = "FullTowerModelSO", menuName = "TaskGame_TowerDefence/Tower/FullTowerModelSO")]
    public class FullTowerModelSO : ScriptableObject
    {
       public List<TowerModelLevelSO> listLavelModels;
    }


}
