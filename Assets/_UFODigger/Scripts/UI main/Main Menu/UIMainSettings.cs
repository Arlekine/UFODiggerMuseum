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
        if(_uiSettings)
            _uiSettings.ShowSettingsMenu();

        if(_uiMainMenu)
            _uiMainMenu.HideMainMenu();
    }
}
