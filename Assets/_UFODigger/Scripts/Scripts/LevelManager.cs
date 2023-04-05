using System.Collections;
using System.Collections.Generic;
using MoreMountains.NiceVibrations;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;

    public bool _endGameplay;

    public int _levelNum;
    public int _partNum;

    public GameObject _gameplayRoot;
    public GameObject _galleryRoot;

    public List<GameObject> _levelProps;
    public List<GameObject> _alienParts;


    public Animator _fade;


    private void Awake()
    {
        if (!instance)
            instance = this;


        Application.targetFrameRate = 60;
        QualitySettings.vSyncCount = 0;

        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        _gameplayRoot.SetActive(true);
        _galleryRoot.SetActive(false);

        if (PlayerPrefs.HasKey("_levelNum"))
        {
            _levelNum = PlayerPrefs.GetInt("_levelNum");
        }

        for(int i = 0; i < _levelProps.Count; i++)
        {
            if(i == _levelNum)
            {
                _levelProps[i].gameObject.SetActive(true);
            }
            else
            {
                _levelProps[i].gameObject.SetActive(false);
            }
        }
    }

    public void DetectPart()
    {
        if (_endGameplay) return;

        _partNum++;

        if (_partNum >= 2)
        {
            StartCoroutine(Delay());
            _endGameplay = true;
        }
    }

    private void SetupGallery()
    {
        _gameplayRoot.SetActive(false);
        _galleryRoot.SetActive(true);

        for (int i = 0; i < _alienParts.Count; i++)
        {
            if (i <= _levelNum)
            {
                _alienParts[i].gameObject.SetActive(true);
            }
            else
            {
                _alienParts[i].gameObject.SetActive(false);
            }
        }

        Destroy(_fade.gameObject, 0.2f);

        if(_levelNum >= _levelProps.Count -1)
            _levelNum = 0;
        else
            _levelNum++;

        PlayerPrefs.SetInt("_levelNum", _levelNum);
    }

    public void DestroyThis()
    {
        Destroy(gameObject);
    }

    IEnumerator Delay()
    {
        MMVibrationManager.Haptic(HapticTypes.Success);

        yield return new WaitForSeconds(1.3f);

        _fade.SetTrigger("in");

        yield return new WaitForSeconds(0.5f);

        Application.LoadLevel(2);

        SetupGallery();
    }

}
