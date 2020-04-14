using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace TaskGame_TowerDefence
{
    public class UpdateViewSprite : MonoBehaviour
    {
        [SerializeField] private Sprite _spriteStandUsual;
        [SerializeField] private Sprite _spriteStandActive;
        [SerializeField] private UnityEvent EventOnClickSprte;
        private SpriteRenderer _sp;

        private void Awake()
        {
            _sp = GetComponent<SpriteRenderer>();
            if (_sp == null)
                Debug.LogError($"TaskGame_TowerDefence ({this}): Не добавлен объект SpriteRenderer для подставки башни");
            //спрайты могут быть null, если ,например, при наведении хотим спрятать что-то

            _sp.sprite = _spriteStandUsual;
        }

        private void OnMouseEnter()
        {
            if (GlobalSettingsGame.isStopGame)
                return;

            _sp.sprite = _spriteStandActive;
        }

        private void OnMouseExit()
        {
            if (GlobalSettingsGame.isStopGame)
                return;

            _sp.sprite = _spriteStandUsual;
        }

        private void OnMouseDown()
        {
            if (GlobalSettingsGame.isStopGame)
                return;

            EventOnClickSprte?.Invoke();
        }

        private void OnEnable()
        {
            if (GlobalSettingsGame.isStopGame)
                return;

            _sp.sprite = _spriteStandUsual;
        }

    }

}

