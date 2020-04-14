using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace TaskGame_TowerDefence
{
    public class PlayerData : MonoBehaviour
    {
        [HideInInspector] public static UnityEvent EventUpdateTextHealtPointOnUI;
        [HideInInspector] public static UnityEvent EventUpdateTextGoldPointOnUI;
        [HideInInspector] public static UnityEvent EventShowPanelGameOver;
        [HideInInspector] public static UnityEvent EventShowPanelGameWinner;

        [SerializeField] private ModelStartDataLVL _modelStartDataLVL;

        private static int _countGold;
        private static int _countHealthPoint;
        private static int _countKillEnemy;


        private static int _countReturnEnemyToPool;

        public static int CountReturnToPool 
        {
            get { return _countReturnEnemyToPool; }
            set
            {
                _countReturnEnemyToPool = value;
                if (SpawnerEnemy.isFinishSpawn)
                {
                    if (SpawnerEnemy.countCreateEnemy == _countReturnEnemyToPool)
                        EventShowPanelGameWinner?.Invoke();
                }

            }
        }

        /// <summary>
        /// Количество очков золота, которое имеетсяу игрока
        /// </summary>
        public static int CountGold
        {
            get { return _countGold; }
            set
            {
                _countGold = value;
                EventUpdateTextGoldPointOnUI?.Invoke();
            }
        }

        /// <summary>
        /// Количество очков здоровья, которое имеется у игрока
        /// </summary>
        public static int CountHealthPoint
        {
            get { return _countHealthPoint; }
            set
            {
                _countHealthPoint = value;
                if (_countHealthPoint <= 0)
                {
                    GameOver();
                    _countHealthPoint = 0;
                }
                EventUpdateTextHealtPointOnUI?.Invoke();
            }
        }

        /// <summary>
        /// Количество противников, которое убил игрок
        /// </summary>
        public static int CountKillEnemy 
        { 
            get { return _countKillEnemy; }
            set { _countKillEnemy = value; }
        }

        private void Awake()
        {
            EventUpdateTextHealtPointOnUI = new UnityEvent();
            EventUpdateTextGoldPointOnUI = new UnityEvent();
            EventShowPanelGameOver = new UnityEvent();
            EventShowPanelGameWinner = new UnityEvent();
            _countReturnEnemyToPool = 0;

            if (_modelStartDataLVL == null)
                Debug.LogError($"TaskGame_TowerDefence ({this}): Не добавлена модель стартовых данных для уровня _modelStartDataLVL");
            InitStartDataLVL();
        }

        /// <summary>
        /// Инициалзиация стартовые данных уровня (присвоение золота и здоровья)
        /// </summary>
        private void InitStartDataLVL()
        {
            _countGold = _modelStartDataLVL.countGold;
            _countHealthPoint = _modelStartDataLVL.countHealthPoint;
            _countKillEnemy = 0;
        }

        /// <summary>
        /// Заверешение игры
        /// </summary>
        private static void GameOver()
        {
            EventShowPanelGameOver?.Invoke();
        }
    }
}
