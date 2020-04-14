using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace TaskGame_TowerDefence
{
    public class TowerLogicEconomic : MonoBehaviour
    {
        [SerializeField] private Tower _tower;
        // TODO: можно сделать List<FullTowerModelSO> _fullTowerModelSO, чтобы был список разных башен, которые можно построить
        [SerializeField] private FullTowerModelSO _fullTowerModelSO;

        [Header("Элементы панели улучшения Башни")]
        [SerializeField] private TextMeshPro _textPriceUpdate;
        [SerializeField] private GameObject _panelUpdateTower;
        [SerializeField] private GameObject _imgUpdateLVL;

        [Header("Элементы панели строительства Башни")]
        [SerializeField] private TextMeshPro _textPriceBuild;
        [SerializeField] private GameObject _panelBuildTower;

        private bool _isOccupy;
        private int _currentLVL;

        private void Awake()
        {
            CheckCorrectStartData();
        }

        private void OnMouseDown()
        {
            if (!_isOccupy)
                ShowMenuBuilding();
            else
                ShowMenuUpdateTower();
        }

        /// <summary>
        /// Отобразить меню строительства башен
        /// </summary>
        private void ShowMenuBuilding()
        {
            if (GlobalSettingsGame.isStopGame)
                return;

            _textPriceBuild.text = $"{_fullTowerModelSO.listLavelModels[0].parametrsTower.priceUpdate}";
            _textPriceUpdate.enabled = true;
            _panelBuildTower.SetActive(true);

        }

        /// <summary>
        /// Отобразить муню улучшения башни
        /// </summary>
        private void ShowMenuUpdateTower()
        {
            if (GlobalSettingsGame.isStopGame)
                return;

            if (_currentLVL >= _fullTowerModelSO.listLavelModels.Count)
            {
                _textPriceUpdate.enabled = false;
                _imgUpdateLVL.SetActive(false);
            }
            else
            {
                _textPriceUpdate.text = $"{_fullTowerModelSO.listLavelModels[_currentLVL].parametrsTower.priceUpdate}";
            }

            _panelUpdateTower.SetActive(true);
        }

        /// <summary>
        /// Разрушить башню
        /// </summary>
        public void DestroyTower()
        {
            if (GlobalSettingsGame.isStopGame)
                return;

            _tower.SetActiveTower(false);
            _panelUpdateTower.SetActive(false);
            _isOccupy = false;

            _imgUpdateLVL.SetActive(true);
            _textPriceUpdate.enabled = true;
            _currentLVL = 0;
        }

        /// <summary>
        /// Построить башню
        /// </summary>
        public void BuildTower()
        {
            if (GlobalSettingsGame.isStopGame)
                return;

            // TODO:  можно добавить оторажения панели выбора построения разных башень
            int priceBuild = _fullTowerModelSO.listLavelModels[_currentLVL].parametrsTower.priceUpdate;
            if (PlayerData.CountGold < priceBuild)
                return;

            _tower.SetActiveTower(true);
            _panelBuildTower.SetActive(false);
            _isOccupy = true;

            _tower.Init(_fullTowerModelSO.listLavelModels[_currentLVL]);

            PlayerData.CountGold -= priceBuild;
            _currentLVL++;
        }

        /// <summary>
        /// Улучшить уровень башни
        /// </summary>
        public void UpdateTowerLVL()
        {
            if (GlobalSettingsGame.isStopGame)
                return;

            int priceBuild = _fullTowerModelSO.listLavelModels[_currentLVL].parametrsTower.priceUpdate;
            if (PlayerData.CountGold < priceBuild)
                return;

            _tower.SetActiveTower(true);
            _panelUpdateTower.SetActive(false);

            PlayerData.CountGold -= priceBuild;
            _currentLVL++;
        }

        /// <summary>
        /// Проверка наличия всех требуемых (ссылок) для старта
        /// </summary>
        private void CheckCorrectStartData()
        {
            if (_tower == null)
                Debug.LogError($"TaskGame_TowerDefence ({this}): Не добавлена ссылка на объект Tower (_tower)");
            if (_fullTowerModelSO == null)
                Debug.LogError($"TaskGame_TowerDefence ({this}): Не добавлена ссылка на модель башни (_fullTowerModelSO)");
            if (_fullTowerModelSO.listLavelModels.Count == 0)
                Debug.LogError($"TaskGame_TowerDefence ({this}): В модель башни не добавлено ни одного уровня (_fullTowerModelSO)");

            if (_panelUpdateTower == null)
                Debug.LogError($"TaskGame_TowerDefence ({this}): Не добавлена ссылка на панель улучшения башни (_panelUpdateTower)");
            if (_textPriceUpdate == null)
                Debug.LogError($"TaskGame_TowerDefence ({this}): Не добавлена ссылка на объект TextMeshPro для вывода цены улучшения");
            if (_imgUpdateLVL == null)
                Debug.LogError($"TaskGame_TowerDefence ({this}): Не добавлена ссылка на изображение-кнопку улучшения башни");

            if (_panelBuildTower == null)
                Debug.LogError($"TaskGame_TowerDefence ({this}): Не добавлена ссылка на панель улучшения башни (_panelBuildTower)");
            if (_textPriceBuild == null)
                Debug.LogError($"TaskGame_TowerDefence ({this}): Не добавлена ссылка на объект TextMeshPro для вывода цены улучшения");

            CheckCorrectDataModel();
        }

        /// <summary>
        /// Проверка модели данных на корректность значений
        /// </summary>
        private void CheckCorrectDataModel()
        {
            for (int i = 0; i < _fullTowerModelSO.listLavelModels.Count; i++)
            {
                var modelLVL = _fullTowerModelSO.listLavelModels[i];
                if (modelLVL.parametrsTower.cooldown == 0)
                    Debug.Log($"TaskGame_TowerDefence ({this}): В модели противника _fullTowerModelSO параметру cooldown (индекс уровеня ={i}) установлено значение 0");
                if (modelLVL.parametrsTower.damageOnShoot == 0)
                    Debug.Log($"TaskGame_TowerDefence ({this}): В модели противника _fullTowerModelSO параметру damageOnShoot (индекс уровеня ={i}) установлено значение 0");
                if (modelLVL.parametrsTower.priceUpdate == 0)
                    Debug.Log($"TaskGame_TowerDefence ({this}): В модели противника _fullTowerModelSO параметру priceUpdate (индекс уровеня ={i}) установлено значение 0");
                if (modelLVL.parametrsTower.shootingDistance == 0)
                    Debug.Log($"TaskGame_TowerDefence ({this}): В модели противника _fullTowerModelSO параметру shootingDistance (индекс уровеня ={i}) установлено значение 0");

                if (modelLVL.skillTower == null)
                    Debug.LogError($"TaskGame_TowerDefence ({this}): В модели противника _fullTowerModelSO параметру skillTower (индекс уровеня ={i}) не установлено значение");
                if (modelLVL.spriteTower == null)
                    Debug.Log($"TaskGame_TowerDefence ({this}): В модели противника _fullTowerModelSO параметру spriteTower (индекс уровеня ={i}) не установлено значение");
            }
        }
    }
}

