using System;
using System.Collections;
using UnityEngine;


public abstract class FieldEventSO : ScriptableObject
{
    [SerializeField] private GameObject _UIPopup;
    [SerializeField] private float _popupDuration = 7;

    private bool _wasActivated;

    private void OnEnable()
    {
        _wasActivated = false;
    }

    public virtual void Activate(PlayerController playerController)
    {
        if (!_wasActivated)
        {
            playerController.StartCoroutine(PopupCoroutine());
        }
    }

    private IEnumerator PopupCoroutine()
    {
        _wasActivated = true;
        var popup = Instantiate(_UIPopup);
        yield return new WaitForSeconds(+_popupDuration);
        Destroy(popup);
        _wasActivated = false;
    }
}
