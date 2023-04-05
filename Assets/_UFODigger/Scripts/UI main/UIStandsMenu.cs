using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIStandsMenu : MonoBehaviour
{
    private Animation _animation;
    private void Start()
    {
        _animation = GetComponent<Animation>();
    }


    public void ShowStandMenu()
    {
        if (_animation != null)
            _animation.Play("Main Menu Show");
    }

    public void HideStandMenu()
    {
        if (_animation != null)
            _animation.Play("Main Menu Hide");
    }
}
