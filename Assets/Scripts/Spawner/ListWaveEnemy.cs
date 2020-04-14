using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TaskGame_TowerDefence
{
    [CreateAssetMenu(fileName = "ListWaveEnemy", menuName = "TaskGame_TowerDefence/Enemy/Spawner/ListWaveEnemy")]
    public class ListWaveEnemy : ScriptableObject
    {
        public float delayStartSpawn;
        [Header("Список волн противника")]
        public List<WaveEnemy> listWave;
    }


    [Serializable]
    public struct WaveEnemy
    {
        [Header("Параметры волны")]
        public float delayStartNextWave;
        [Header("Список стадий волны")]
        public List<WaveStage> listStageInWave;
    }

    [Serializable]
    public struct WaveStage
    {
        [Header("Параметры стадии")]
        public float delayStartNextStage;
        public int minCountEnemyInStage;
        public int maxCountEnemyInStage;
        public int indexEnemyPrefabInPoll;
        public float delayShowOneEnemy;
        public int indexLavelPowerEnemy;
    }
}

