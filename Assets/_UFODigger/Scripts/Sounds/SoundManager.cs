using UnityEngine;
using Utils;

public class SoundManager : Singleton<SoundManager>
{
    [SerializeField] private AudioSource _uiClickCoinSound;
    [SerializeField] private AudioSource _uiClickSound;
    [SerializeField] private AudioSource _coinCollect;
    [SerializeField] private AudioSource _iceBreak;
    [SerializeField] private AudioSource _bomb;
    [SerializeField] private AudioSource _alienPart;
    [SerializeField] private AudioSource _alienComplete;
    [SerializeField] private AudioSource _newStand;
    [SerializeField] private AudioSource _Win;

    public void PlayUIClick()
    {
        _uiClickSound.Play();
    }

    public void PlayUIClickCoin()
    {
        _uiClickCoinSound.Play();
    }

    public void PlayCoinCollect()
    {
        _coinCollect.Play();
    }

    public void PlayIceBreak()
    {
        _iceBreak.pitch = Random.Range(0.9f, 1.1f);
        _iceBreak.Play();
    }

    public void PlayBomb()
    {
        _bomb.Play();
    }

    public void PlayAlienPartFound()
    {
        _alienPart.Play();
    }

    public void PlayAlienComplete()
    {
        _alienComplete.Play();
    }

    public void PlayNewStand()
    {
        _newStand.Play();
    }

    public void PlayWin()
    {
        _Win.Play();
    }
}
