using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISkillMenu : MonoBehaviour
{
    private Animation _animation;

    private void Start()
    {
        _animation = GetComponent<Animation>();
    }

    public void OpenSkillMenu()
    {
        if (_animation != null)
            _animation.Play("Shop Show");
    }
    public void CloseSkillMenu()
    {
        if (_animation != null)
            _animation.Play("Shop Hide");
    }
}
