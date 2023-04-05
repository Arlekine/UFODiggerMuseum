using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIUpgradeMenu : MonoBehaviour
{
    private Animation _animation;

    private void Start()
    {
        _animation = GetComponent<Animation>();
    }

    public void OpenUpgrades()
    {
        if (_animation != null)
            _animation.Play("Shop Show");
    }
    public void CloseUpgrades()
    {
        if (_animation != null)
            _animation.Play("Shop Hide");
    }
}
