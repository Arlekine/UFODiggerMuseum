using UnityEngine;
using UnityEngine.UI;

public class UISettingsClose : MonoBehaviour
{
    [SerializeField] private UISettings _uiSettings;
    [SerializeField] private UIMainMenu _uiMainMenu;

    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(CloseSettingsMenu);
    }

    private void CloseSettingsMenu()
    {
        _uiSettings.HideSettingsMenu();
        _uiMainMenu.ShowMainMenu();
    }
}
