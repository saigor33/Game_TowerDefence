using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TaskGame_TowerDefence
{



    public class SpawnerEnemy : MonoBehaviour
    {
        [SerializeField] private PathMoving _pathMovingCollider;
        [SerializeField] private ListWaveEnemy _listWaveEnemy;
        private SpriteRenderer _sp;

        //Если будет несколько спавнеров, то будет баг (когда один спавнер закончит работу у всех установитс флаг).
        //нужно будет иметь список спавнеров и перебирать параметр уже без static
        //сейчас реализовано так
        public static bool isFinishSpawn;
        public static int countCreateEnemy;

        private bool isLastWave;

        private void Awake()
        {
            _sp = GetComponent<SpriteRenderer>();

            if (_pathMovingCollider == null)
                Debug.LogError($"TaskGame_TowerDefence ({this}): Не добавлен объект PathMoving по которому должны двигаться противники");
            if (_listWaveEnemy == null)
                Debug.LogError($"TaskGame_TowerDefence ({this}): Не добавлен объект ListWaveEnemy для спавна _listWaveEnemy");
            if (_sp == null)
                Debug.LogError($"TaskGame_TowerDefence ({this}): Не добавлен объект SpriteRenderer для отображения точки начала спавна _sp");

            //Нужен только, что видеть на сцене где расположена точка спавна. При старте отключаем
            _sp.enabled = false;
        }


        void Start()
        {
            isFinishSpawn = false;
            isLastWave = false;
            countCreateEnemy = 0;
            StartCoroutine(StartSpawn(_listWaveEnemy.delayStartSpawn));
        }

        /// <summary>
        /// Нчать спавн противников
        /// </summary>
        /// <param name="delay"></param>
        /// <returns></returns>
        private IEnumerator StartSpawn(float delay)
        {
            yield return new WaitForSeconds(delay);
            StartCoroutine(StartWaves());
        }

        /// <summary>
        /// Старт выполны
        /// </summary>
        /// <returns></returns>
        private IEnumerator StartWaves()
        {
            for (int i=0; i< _listWaveEnemy.listWave.Count; i++)
            {
                WaveEnemy waveEnemy = _listWaveEnemy.listWave[i];
                StartCoroutine(StartStages(waveEnemy));
                yield return new WaitForSeconds(waveEnemy.delayStartNextWave);

                if (i == _listWaveEnemy.listWave.Count -1)
                    isLastWave = true;
            }
        }

        /// <summary>
        /// Старт стадии в волне
        /// </summary>
        /// <param name="waveEnemy"></param>
        /// <returns></returns>
        private IEnumerator StartStages(WaveEnemy waveEnemy)
        {
            for (int i=0; i< waveEnemy.listStageInWave.Count; i++ )
            {
                WaveStage waveStage = waveEnemy.listStageInWave[i];
                StartCoroutine(StartShowEnemy(waveStage));
                yield return new WaitForSeconds(waveStage.delayStartNextStage);
            }
        }

        /// <summary>
        /// Появления противников стадии
        /// </summary>
        /// <param name="waveStage"></param>
        /// <returns></returns>
        private IEnumerator StartShowEnemy(WaveStage waveStage)
        {
            int countEnemy = Random.Range(waveStage.minCountEnemyInStage, waveStage.maxCountEnemyInStage);
            for (int i = 0; i < countEnemy; i++)
            {
                GameObject enemyPref = PoolManager.GetObject(waveStage.indexEnemyPrefabInPoll, transform.position, Quaternion.identity);
                EnemyActor enemyActor = enemyPref.GetComponent<EnemyActor>();

                bool statusAddEnemy = enemyActor.SetUpgradeLVL(waveStage.indexLavelPowerEnemy, _pathMovingCollider.ListPositionPathMoving.Head);
                if (statusAddEnemy)
                    countCreateEnemy++;

                yield return new WaitForSeconds(waveStage.delayShowOneEnemy);

                if (isLastWave && (i == countEnemy-1))
                    isFinishSpawn = true;
            }
        }
    }
}