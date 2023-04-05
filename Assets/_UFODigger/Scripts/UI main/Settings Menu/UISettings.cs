using UnityEngine;

public class UISettings : MonoBehaviour
{
    private Animation _animation;

    private void Start()
    {
        _animation = GetComponent<Animation>();
    }

    public void ShowSettingsMenu()
    {
        if (_animation != null)
            _animation.Play("Settings Menu Show");
    }

    public void HideSettingsMenu()
    {
        if (_animation != null)
            _animation.Play("Settings Menu Hide");
    }
}
