using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Utils;

public class UISettingsVibro : MonoBehaviour
{
    public static UnityEvent<bool> OnVibroStatusChange;

    [SerializeField] private GameObject _vibroStatusOnText;
    [SerializeField] private GameObject _vibroStatusOffText;

    public static bool _vibroStatus;
    private const string VibroKey = "Vibro";

    private void Awake()
    {
        Load();
    }

    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(ChangeSoundStatus);
        OnVibroStatusChange?.Invoke(_vibroStatus);
        ChangeSoundText();
    }

    private void ChangeSoundStatus()
    {
        _vibroStatus = !_vibroStatus;
        ChangeSoundText();
        Save();
    }

    private void ChangeSoundText()
    {
        if (_vibroStatus)
        {
            _vibroStatusOffText.SetActive(false);
            _vibroStatusOnText.SetActive(true);
        }
        else
        {
            _vibroStatusOffText.SetActive(true);
            _vibroStatusOnText.SetActive(false);
        }
    }

    private void Load()
    {
        if (SaveLoadSystem.CheckKey(VibroKey))
        {
            _vibroStatus = SaveLoadSystem.LoadBool(VibroKey);
        }
        else
        {
            _vibroStatus = true;
        }
    }

    private void Save()
    {
        SaveLoadSystem.Save(VibroKey,_vibroStatus);
    }
}