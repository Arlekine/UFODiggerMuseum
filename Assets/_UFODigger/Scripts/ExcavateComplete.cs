using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExcavateComplete : MonoBehaviour
{
    public AlienForExcavateData AlienData;

    public float DelayBeforeLose;
    public float DelayBeforeWin;

    private bool _levelFinish;

    int _levelNum;

    PlayerTurnsUI _playerTurnsUI;

    ADSManager dSManager;

    private void Start()
    {
        _playerTurnsUI = FindObjectOfType<PlayerTurnsUI>();

        dSManager = FindObjectOfType<ADSManager>();

        //AdMobManager.instance._stopTimer = true;
    }

    public void CompleteExcavate()
    {
        if (_levelFinish)
            return;

        _levelFinish = true;
        AlienData.IsAlienExcavate = false;
        AlienData.Save();

        //AdMobManager.instance._stopTimer = false;


        if (PlayerPrefs.HasKey("levelNum"))
            _levelNum = PlayerPrefs.GetInt("levelNum");

        if (AlienData.FoundedParts != null && AlienData.FoundedParts.Count > 0)
        {
            _levelNum++;

            _playerTurnsUI.HideGiftOffer();

            //AppMetricaManager.instance.LevelProgress(_levelNum, "Win");
            StartCoroutine(DelayBeforeMoveToNewScene(DelayBeforeWin));
        }
        else
        {
            //AppMetricaManager.instance.LevelProgress(_levelNum, "Lose");
            StartCoroutine(DelayBeforeMoveToNewScene(DelayBeforeLose));
        }

        PlayerPrefs.SetInt("levelNum", _levelNum);
    }

    IEnumerator DelayBeforeMoveToNewScene(float delay)
    {
        yield return new WaitForSeconds(delay);

        dSManager.ShowInterstitialAds();

        yield return new WaitForSeconds(0.3f);

        SceneManager.LoadScene(3);
    }
}