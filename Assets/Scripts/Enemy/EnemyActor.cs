using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TaskGame_TowerDefence
{
    public class EnemyActor : MonoBehaviour
    {

        [SerializeField] private GameObject _healthBar;
        [SerializeField] private FullEnemyModelSO _fullEnemyModelSO;
        private const float _distansFinishPosition = 0.2f;
        
        private PoolObject _poolObject;

        private EnemyParametrs enemyStartParametrs;
        private float _speed;
        private int _healthpoint;
        private int _damage;

        private Node<Vector2> _currentPositionMoving;

        private void Awake()
        {
            _poolObject = GetComponent<PoolObject>();
            CheckCorrectData();
        }

        private void FixedUpdate()
        {
            Moving();
            CheckPositonMoving();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag(GlobalSettingsGame.TAG_FORTRES))
            {
                // TODO: можно сделать анимацию столкновения (взрыв/ подёргивание экрана и т.д..)
                PlayerData.CountHealthPoint -= _damage;
                ReturnToPool();
            }
        }

        /// <summary>
        /// Инициализация параметров противника
        /// </summary>
        /// <param name="enemyModelSO"></param>
        protected virtual void InitParametrsEnemy(EnemyModelLevelSO enemyModelSO)
        {
            if (enemyModelSO == null)
                return;

            enemyStartParametrs = enemyModelSO.enemyParametrs;
            _speed = enemyStartParametrs.speedMovingStart;
            _healthpoint = enemyStartParametrs.healthPoints;
            _damage = enemyStartParametrs.damage;
            UpdateHealtBar();
        }

        /// <summary>
        /// Улучшить показатели уровня монстра
        /// </summary>
        /// <param name="enemyPowerLVL"></param>
        /// <param name="startPathMoving"></param>
        /// <returns>Возвращает значение true, если объект успешно улучшен. Иначе false(отправлcz обратно в Pool)</returns>
        public bool SetUpgradeLVL(int enemyPowerLVL, Node<Vector2> startPathMoving)
        {
            if (enemyPowerLVL >= _fullEnemyModelSO.listLavelModels.Count)
            {
                Debug.Log($"TaskGame_TowerDefence ({this}): для данного противника не предусмотрен такой уровень модели enemyPowerLVL = {enemyPowerLVL}. Будет установлен уровнеь = 0");
                _poolObject.ReturnToPool(); //добавлять в метод  ReturnToPool() не трубется т.к. это значение не учитывается
                return false;
            }

            _currentPositionMoving = startPathMoving;
            InitParametrsEnemy(_fullEnemyModelSO.listLavelModels[enemyPowerLVL]);
            return true;
        }

        /// <summary>
        /// Двигаться к указанной точке
        /// </summary>
        private void Moving()
        {
            // TODO: можно добавить анимацию передвижения

            Vector2 movement = (_currentPositionMoving.Data - new Vector2(transform.position.x, transform.position.y)).normalized;
            transform.Translate(movement * _speed * Time.fixedDeltaTime);
        }

        /// <summary>
        /// Проверить текущую позицию движение на достижение выбранной точки
        /// </summary>
        private void CheckPositonMoving()
        {
            if (Vector2.Distance(transform.position, _currentPositionMoving.Data) < _distansFinishPosition)
            {
                if (_currentPositionMoving.Next != null)
                    _currentPositionMoving = _currentPositionMoving.Next;
                else
                    _speed = 0;
            }
        }

        /// <summary>
        /// Нанести урон 
        /// </summary>
        /// <param name="damage"></param>
        public void ReciveDamage(int damage)
        {
            //TODO: можно сделать воспроизведение звука нанесения урона

            _healthpoint -= damage;
            if (_healthpoint <= 0)
                Kill();
            else
                UpdateHealtBar();
        }

        /// <summary>
        /// Обновить данные шкалы здоровья
        /// </summary>
        private void UpdateHealtBar()
        {
            _healthBar.transform.localScale = new Vector3(GetHealthPercent(), 1);
        }

        /// <summary>
        /// Получить значение здоровья в процентах
        /// </summary>
        /// <returns></returns>
        private float GetHealthPercent()
        {
            return _healthpoint /(float)enemyStartParametrs.healthPoints;
        }

        /// <summary>
        /// Смерть противника
        /// </summary>
        private void Kill()
        {
            PlayerData.CountGold += enemyStartParametrs.priceKill;
            PlayerData.CountKillEnemy++;
            ReturnToPool();
        }

        private void ReturnToPool()
        {
            PlayerData.CountReturnToPool++;
            _poolObject.ReturnToPool();
        }

        /// <summary>
        /// Проверка наличия всех требуемых (ссылок) для старта
        /// </summary>
        private void CheckCorrectData()
        {

            if (_healthBar == null)
                Debug.LogError($"TaskGame_TowerDefence ({this}): Не добавлен объект для отображения здоровья противника _healthBar");
            if (_poolObject == null)
                Debug.LogError($"TaskGame_TowerDefence ({this}): Не добавлен объект PoolObject");
            if (_fullEnemyModelSO == null)
                Debug.LogError($"TaskGame_TowerDefence ({this}): Не добавлена модель противника _fullEnemyModelSO");
            if (_fullEnemyModelSO.listLavelModels.Count == 0)
                Debug.LogError($"TaskGame_TowerDefence ({this}): В модели противника _fullEnemyModelSO нет нет ни одного уровня");

            CheckCorrectDataModel();
        }

        /// <summary>
        /// Проверка модели данных на корректность значений
        /// </summary>
        private void CheckCorrectDataModel()
        {
            for (int i=0; i< _fullEnemyModelSO.listLavelModels.Count; i++)
            {
                var modelLVL = _fullEnemyModelSO.listLavelModels[i];
                if (modelLVL.enemyParametrs.damage == 0)
                    Debug.Log($"TaskGame_TowerDefence ({this}): В модели противника _fullEnemyModelSO параметру damage (индекс уровеня ={i}) установлено значение 0");
                if (modelLVL.enemyParametrs.healthPoints == 0)
                    Debug.Log($"TaskGame_TowerDefence ({this}): В модели противника _fullEnemyModelSO параметру healthPoints (индекс уровеня ={i}) установлено значение 0");
                if (modelLVL.enemyParametrs.priceKill == 0)
                    Debug.Log($"TaskGame_TowerDefence ({this}): В модели противника _fullEnemyModelSO параметру priceKill (индекс уровеня ={i}) установлено значение 0");
                if (modelLVL.enemyParametrs.speedMovingStart == 0)
                    Debug.Log($"TaskGame_TowerDefence ({this}): В модели противника _fullEnemyModelSO параметру speedMovingStart (индекс уровеня ={i}) установлено значение 0");

                if(modelLVL.spriteEnemy == null)
                    Debug.Log($"TaskGame_TowerDefence ({this}): В модели противника _fullEnemyModelSO параметру spriteEnemy (индекс уровеня ={i}) не установлено значение");
            }

        }

    }

}

