using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TaskGame_TowerDefence
{
    /// <summary>
    /// Уровень башни и её параметры
    /// </summary>
    [CreateAssetMenu(fileName = "TowerModelLevelSO", menuName = "TaskGame_TowerDefence/Tower/TowerModelLevelSO")]
    public class TowerModelLevelSO : ScriptableObject
    {
        public Sprite spriteTower;
        public ParametrsTower parametrsTower;
        public SkillModelTowerSO skillTower;

    }

    [Serializable]
    public struct ParametrsTower
    {
        public int priceUpdate;
        public int damageOnShoot;
        public float cooldown;
        public float shootingDistance;
    }

}

