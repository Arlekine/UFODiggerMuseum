using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

public class SoundManager : Singleton<SoundManager>
{
    [SerializeField] private AudioSource _uiClickSound;
    [SerializeField] private AudioSource _coinCollect;
    [SerializeField] private AudioSource _iceBreak;
    [SerializeField] private AudioSource _bomb;
    [SerializeField] private AudioSource _alienPart;
    [SerializeField] private AudioSource _alienComplete;
    [SerializeField] private AudioSource _newStand;

    public void PlayUIClick()
    {
        _uiClickSound.Play();
    }

    public void PlayCoinCollect()
    {
        _coinCollect.Play();
    }

    public void PlayIceBreak()
    {
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
}
