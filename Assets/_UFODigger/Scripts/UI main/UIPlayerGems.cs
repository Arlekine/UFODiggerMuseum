using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class UIPlayerGems : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _count;
    [SerializeField] private PLayerData _playerData;
    private  void Start()
    {
        _count.text = _playerData.GemCount.ToString();
        _playerData.OnGemsCountChange.AddListener(SetGemCount);
    }

    private void SetGemCount()
    {
        _count.text = _playerData.GemCount.ToString();
    }
}
