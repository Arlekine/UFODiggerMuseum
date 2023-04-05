using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Utils;

public class UISettingsSound : MonoBehaviour
{
    public static UnityEvent<bool> OnSoundStatusChange;

    [SerializeField] private GameObject _soundStatusOnText;
    [SerializeField] private GameObject _soundStatusOffText;

    private static bool _soundStatus;
    private const string SoundKey = "Sounf";
    private void Awake()
    {
        Load();
    }
    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(ChangeSoundStatus);
        OnSoundStatusChange?.Invoke(_soundStatus);
        ChangeSoundText(_soundStatus);
    }

    private void ChangeSoundStatus()
    {
        _soundStatus = !_soundStatus;
        ChangeSoundText(_soundStatus);
        Save();
    }

    private void ChangeSoundText(bool status)
    {
        if (_soundStatus)
        {
            _soundStatusOffText.SetActive(false);
            _soundStatusOnText.SetActive(true);
        }
        else
        {
            _soundStatusOffText.SetActive(true);
            _soundStatusOnText.SetActive(false);
        }

    }
    
    private void Load()
    {
        if (SaveLoadSystem.CheckKey(SoundKey))
        {
            _soundStatus = SaveLoadSystem.LoadBool(SoundKey);
        }
        else
        {
            _soundStatus = true;
        }
    }

    private void Save()
    {
        SaveLoadSystem.Save(SoundKey,_soundStatus);
    }
}
