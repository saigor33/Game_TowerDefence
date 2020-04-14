using UnityEngine;

namespace TaskGame_TowerDefence
{
    [CreateAssetMenu(fileName = "SkillModelTowerSO", menuName = "TaskGame_TowerDefence/Tower/SkillModelTowerSO")]
    public class SkillModelTowerSO : ScriptableObject
    {
        [SerializeField] private int _indexPrefabFromPoll;
        [SerializeField] private float _speedBullet;

        //скорость снаряда
        //снаряд
        public virtual void Shoot(int damageOnShoot, Vector2 shooterPosition, Transform targetTransform)
        {
            GameObject bullet = PoolManager.GetObject(_indexPrefabFromPoll, shooterPosition, Quaternion.identity);
            bullet.GetComponent<Bullet>().Init(damageOnShoot, _speedBullet, targetTransform);
        }
    }

}

