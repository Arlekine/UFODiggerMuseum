using UnityEngine;

public class UISettings : MonoBehaviour
{
    [SerializeField] private GameObject _blockingPanel;
    [SerializeField] private CameraMove _cameraMove;

    private Animation _animation;

    private void Start()
    {
        _animation = GetComponent<Animation>();
    }

    public void ShowSettingsMenu()
    {
        if (_animation != null)
            _animation.Play("Settings Menu Show");

        _blockingPanel.SetActive(true);
        _cameraMove?.TurnOffMouseCameraControl();
    }

    public void HideSettingsMenu()
    {
        if (_animation != null)
            _animation.Play("Settings Menu Hide");

        _blockingPanel.SetActive(false);
        _cameraMove?.TurnOnMouseCameraControl();
    }
}
