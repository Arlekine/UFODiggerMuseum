using UnityEngine;
using UnityEngine.UI;

public class UIMainSettings : MonoBehaviour
{
    [SerializeField] private UISettings _uiSettings;
    [SerializeField] private UIMainMenu _uiMainMenu;

    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(OpenSettingsMenu);
    }

    private void OpenSettingsMenu()
    {
        _uiSettings.ShowSettingsMenu();
        _uiMainMenu.HideMainMenu();
    }
}
