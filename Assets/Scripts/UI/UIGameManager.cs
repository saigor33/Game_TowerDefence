using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace TaskGame_TowerDefence
{

    public class UIGameManager : MonoBehaviour
    {
        [Header("Контролы отображающие данные")]
        [SerializeField] private TextMeshProUGUI _txtHealthPoint;
        [SerializeField] private TextMeshProUGUI _textGoldPoint;

        [Header("Панели стадий игры")]
        [SerializeField] private GameObject _panelPause;
        [SerializeField] private GameObject _panelGameOver;
        [SerializeField] private GameObject _panelGameWinner;

        [SerializeField] private TextMeshProUGUI _txtCountKillEnemy_onGameOver;
        [SerializeField] private TextMeshProUGUI _txtCountKillEnemy_onGameWinner;


        private void Awake()
        {
            if (_txtHealthPoint == null)
                Debug.LogError($"TaskGame_TowerDefence ({this}): Не добавлен объект TextMeshPro для отображения значения здоровья игрока _txtHealthPoint");
            if (_textGoldPoint == null)
                Debug.LogError($"TaskGame_TowerDefence ({this}): Не добавлен объект TextMeshPro для отображения значения золота игрока _textGoldPoint");

            if (_panelPause == null)
                Debug.LogError($"TaskGame_TowerDefence ({this}): Не добавлен панель паузы _panelPause");
            if (_panelGameWinner == null)
                Debug.LogError($"TaskGame_TowerDefence ({this}): Не добавлен панель победы _panelGameWinner");
            if (_panelGameOver == null)
                Debug.LogError($"TaskGame_TowerDefence ({this}): Не добавлен панель поражения _panelGameOver");

            if (_txtCountKillEnemy_onGameOver == null)
                Debug.LogError($"TaskGame_TowerDefence ({this}): Не добавлен объект текст, где отображается количество убийств _txtCountKillEnemy_onGameOver");
            if (_txtCountKillEnemy_onGameWinner == null)
                Debug.LogError($"TaskGame_TowerDefence ({this}): Не добавлен объект текст, где отображается количество убийств _txtCountKillEnemy_onGameWinner");
        }



        private void Start()
        {
            Time.timeScale = 1;
            PlayerData.EventUpdateTextGoldPointOnUI.AddListener(UpdateTextGoldPoint);
            PlayerData.EventUpdateTextHealtPointOnUI.AddListener(UpdateTextHealthPoint);
            PlayerData.EventShowPanelGameOver.AddListener(ShowPanelGameOver);
            PlayerData.EventShowPanelGameWinner.AddListener(ShowPanelGameWinner);
            GlobalSettingsGame.isStopGame = false;

            UpdateTextHealthPoint();
            UpdateTextGoldPoint();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
                ShowPanelPause();
        }

        private void OnDestroy()
        {
            PlayerData.EventUpdateTextGoldPointOnUI.RemoveListener(UpdateTextGoldPoint);
            PlayerData.EventUpdateTextHealtPointOnUI.RemoveListener(UpdateTextHealthPoint);
            PlayerData.EventShowPanelGameOver.RemoveListener(ShowPanelGameOver);
            PlayerData.EventShowPanelGameWinner.RemoveListener(ShowPanelGameWinner);
        }

        /// <summary>
        /// Обновить значение текста здоровья игрока
        /// </summary>
        private void UpdateTextHealthPoint()
        {
            _txtHealthPoint.text = PlayerData.CountHealthPoint.ToString();
        }

        /// <summary>
        /// Обновить значение текста золота игрока
        /// </summary>
        private void UpdateTextGoldPoint()
        {
            _textGoldPoint.text = PlayerData.CountGold.ToString();
        }

        /// <summary>
        /// Отобразить панель проигрыша
        /// </summary>
        private void ShowPanelGameOver()
        {
            // TODO: можно сделать запуск анимации проигрыша

            Time.timeScale = 0;
            _panelGameOver.SetActive(true);
            _txtCountKillEnemy_onGameOver.text = $"Количество убитых врагов: {PlayerData.CountKillEnemy}";
            GlobalSettingsGame.isStopGame = true;
        }

        /// <summary>
        /// Отобразить панель выигрыша
        /// </summary>
        private void ShowPanelGameWinner()
        {
            // TODO: можно сделать запуск анимации выигрыша

            Time.timeScale = 0;
            _panelGameWinner.SetActive(true);
            _txtCountKillEnemy_onGameWinner.text = $"Количество убитых врагов: {PlayerData.CountKillEnemy}";
            GlobalSettingsGame.isStopGame = true;
        }

        public void BtnRestart_onClick()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        }

        /// <summary>
        /// Отобращить панель паузы
        /// </summary>
        private void ShowPanelPause()
        {
            if (GlobalSettingsGame.isStopGame)
                return;

            _panelPause.SetActive(true);
            Time.timeScale = 0;
            GlobalSettingsGame.isStopGame = true;
        }


        public void BtnExit_onClick()
        {
            // TODO: можно сделать диалоговое окно с подтвержением

            Application.Quit();
        }

        public void BtnResumeGame_OnClick()
        {
            _panelPause.SetActive(false);
            Time.timeScale = 1;
            GlobalSettingsGame.isStopGame = false;
        }


    }
}