using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TaskGame_TowerDefence
{

    public class Tower : MonoBehaviour
    {
        private SpriteRenderer _sp;
        private CircleCollider2D _ccAffectedArea;
        private SkillModelTowerSO _skillModelTower;
        private ParametrsTower _parametrsTower;

        private List<EnemyActor> _listEnemyInRange;

        private float LastTimeShoot { get; set; }

        private void Awake()
        {
            _listEnemyInRange = new List<EnemyActor>();
            _sp = GetComponent<SpriteRenderer>();
            _ccAffectedArea = GetComponent<CircleCollider2D>();

            if (_sp== null)
                Debug.LogError($"TaskGame_TowerDefence ({this}): Не добавлен объект SpriteRenderer для башни");
            if (_ccAffectedArea == null)
                Debug.LogError($"TaskGame_TowerDefence ({this}): Не добавлен объект CircleCollider2D для башни");
        }

        private void Update()
        {
            if (IsColldown())
                Shoot();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag(GlobalSettingsGame.TAG_ENEMY))
            {
                EnemyActor enemy = collision.GetComponent<EnemyActor>();
                if (enemy != null)
                    _listEnemyInRange.Add(enemy);
                else
                    Debug.LogError($"TaskGame_TowerDefence ({collision}): установлен тег {GlobalSettingsGame.TAG_ENEMY} для объекта у которого отсутсвует EnemyActor");
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.CompareTag(GlobalSettingsGame.TAG_ENEMY))
            {

                EnemyActor enemy = collision.GetComponent<EnemyActor>();
                if (enemy != null)
                    _listEnemyInRange.Remove(enemy);
                else
                    Debug.LogError($"TaskGame_TowerDefence ({collision}): установлен тег {GlobalSettingsGame.TAG_ENEMY} для объекта у которого отсутсвует EnemyActor");
            }
        }

        /// <summary>
        /// Инициализация параметров бащни
        /// </summary>
        /// <param name="towerModelSO"></param>
        public virtual void Init(TowerModelLevelSO towerModelSO)
        {
            _sp.sprite = towerModelSO.spriteTower;
            _parametrsTower = towerModelSO.parametrsTower;
            _skillModelTower = towerModelSO.skillTower;

            _ccAffectedArea.radius = _parametrsTower.shootingDistance;
            LastTimeShoot = -_parametrsTower.cooldown;
        }

        protected virtual void Shoot()
        {
            // TODO: можно добавить алгоритм выбора противника, который ближе всех к финальной точке
            EnemyActor enemyTarget = GetEnemyTarget();
            if (enemyTarget == null)
                return;

            _skillModelTower.Shoot(_parametrsTower.damageOnShoot, transform.position, enemyTarget.transform);
            LastTimeShoot = Time.time;
        }

        /// <summary>
        /// Получить цель атаки
        /// </summary>
        /// <returns></returns>
        protected virtual EnemyActor GetEnemyTarget()
        {
            if (_listEnemyInRange.Count == 0)
                return null;

            int indexEnemy = Random.Range(0, _listEnemyInRange.Count);
            EnemyActor enemyActor = _listEnemyInRange[indexEnemy];
            if(enemyActor == null)
            {
                // enemyActor может быть null в случае, если противника убила другая башня
                _listEnemyInRange.RemoveAt(indexEnemy);
                return GetEnemyTarget();
            }

            return enemyActor;
        }

        /// <summary>
        /// Проверить закончился ли кулдаун выстрела
        /// </summary>
        /// <returns>true - кулдаун закончился, можно стрелять</returns>
        protected virtual bool IsColldown()
        {
            return Time.time - LastTimeShoot > _parametrsTower.cooldown;
        }

        /// <summary>
        /// Отключить объект башню
        /// </summary>
        /// <param name="isActive"></param>
        public virtual void SetActiveTower(bool isActive)
        {
            gameObject.SetActive(isActive);
        }
    }

}
