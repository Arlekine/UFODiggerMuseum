using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

public class PlayerManager : Singleton<PlayerManager>
{
    public RectTransform _goldCanvasCollectZone;
    public PLayerData _playerData;
    public Transform _worldCollectZone;

    private void Awake()
    {
        _playerData.LoadData();
    }

    private void OnDestroy()
    {
        _playerData.SaveData();
    }

    private void OnApplicationQuit()
    {
        _playerData.SaveData();
    }
}
