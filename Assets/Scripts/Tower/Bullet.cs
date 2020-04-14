using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TaskGame_TowerDefence
{
    public class Bullet : MonoBehaviour
    {
        private PoolObject _poolObject;
        private int _damage;
        private float _speedMoving;
        private Transform _target;

        private void Awake()
        {
            _poolObject = GetComponent<PoolObject>();
            if(_poolObject ==null)
                Debug.LogError($"TaskGame_TowerDefence ({this}): Не добавлен объект PoolObject");
        }

        private void Update()
        {
            if(_target !=null)
                Move();
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag(GlobalSettingsGame.TAG_ENEMY))
            {
                EnemyActor enemy = collision.gameObject.GetComponentInChildren<EnemyActor>();
                if (enemy != null)
                    enemy.ReciveDamage(_damage);
                _poolObject.ReturnToPool();

                //TODO: можно сделать воспроизведение звука нанесения урона
            }
        }

        /// <summary>
        /// Инициализация параметров снаряда
        /// </summary>
        /// <param name="damage"></param>
        /// <param name="speedMoving"></param>
        /// <param name="targetTransform"></param>
        public void Init(int damage,  float speedMoving, Transform targetTransform)
        {
            _damage = damage;
            _speedMoving = speedMoving;
            _target = targetTransform;
        }

        /// <summary>
        /// Перемещение снаряда
        /// </summary>
        private void Move()
        {
            Vector2 movement = (_target.position - transform.position).normalized;
            transform.Translate(movement * _speedMoving * Time.fixedDeltaTime);
        }


    }
}