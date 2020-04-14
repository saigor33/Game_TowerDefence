using UnityEngine;

namespace TaskGame_TowerDefence
{
    [CreateAssetMenu(fileName = "ModelStartDataLVL", menuName = "TaskGame_TowerDefence/Lavel/ModelStartDataLVL")]
    public class ModelStartDataLVL : ScriptableObject
    {
        [Header("Стартовые данные уровня")]
        public int countGold;
        public int countHealthPoint;

    }
}
