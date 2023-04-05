using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Utils;

public class UISettingsMusic : MonoBehaviour
{
    public static UnityEvent<bool> OnMusicStatusChange;

    [SerializeField] private GameObject _musicStatusOnText;
    [SerializeField] private GameObject _musicStatusOffText;

    private static bool _musicStatus;
    private const string MusicKey = "Sounf";
    private void Awake()
    {
        Load();
    }
    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(ChangeMusicStatus);
        OnMusicStatusChange?.Invoke(_musicStatus);
        ChangeMusicText(_musicStatus);
    }

    private void ChangeMusicStatus()
    {
        _musicStatus = !_musicStatus;
        ChangeMusicText(_musicStatus);
        Save();
    }

    private void ChangeMusicText(bool status)
    {
        if (_musicStatus)
        {
            _musicStatusOffText.SetActive(false);
            _musicStatusOnText.SetActive(true);
        }
        else
        {
            _musicStatusOffText.SetActive(true);
            _musicStatusOnText.SetActive(false);
        }

    }
    
    private void Load()
    {
        if (SaveLoadSystem.CheckKey(MusicKey))
        {
            _musicStatus = SaveLoadSystem.LoadBool(MusicKey);
        }
        else
        {
            _musicStatus = true;
        }
    }

    private void Save()
    {
        SaveLoadSystem.Save(MusicKey,_musicStatus);
    }
}
