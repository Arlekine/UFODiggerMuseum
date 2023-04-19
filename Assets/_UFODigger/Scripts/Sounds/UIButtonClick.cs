using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIButtonClick : MonoBehaviour
{
    public bool _GetCoinButton;
    private Button _button;

    private Button Button
    {
        get
        {
            if (_button == null)
                _button = GetComponent<Button>();

            return _button;
        }
    }

    private void OnEnable()
    {
        if (Button != null)
            Button.onClick.AddListener(PlaySound);
    }

    private void OnDisable()
    {
        if (Button != null)
            Button.onClick.RemoveListener(PlaySound);
    }

    private void PlaySound()
    {
        if(_GetCoinButton)
            SoundManager.Instance.PlayUIClickCoin();
        else
            SoundManager.Instance.PlayUIClick();
    }
}
