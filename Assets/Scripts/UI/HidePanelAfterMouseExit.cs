using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HidePanelAfterMouseExit : MonoBehaviour
{
    [SerializeField]private GameObject _objectHide;

    private void Awake()
    {
        if(_objectHide ==null)
            Debug.LogError($"TaskGame_TowerDefence ({this}): Не добавлен объект, который нужено спрятать при потери фокуса _objectHide");
    }

    private void OnMouseExit()
    {
        Debug.Log("Exit");
        _objectHide.SetActive(false);
    }

}
