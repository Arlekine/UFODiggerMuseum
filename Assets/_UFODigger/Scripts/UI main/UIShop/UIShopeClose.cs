using UnityEngine;
using UnityEngine.UI;

public class UIShopeClose : MonoBehaviour
{
    [SerializeField] private ShopManager _shop;
    [SerializeField] private UIMainMenu _uiMainMenu;

    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(CloseSettingsMenu);
    }

    private void CloseSettingsMenu()
    {
        _shop.CloseShop();
        if (_uiMainMenu != null)
        {
            _uiMainMenu.ShowMainMenu();
        }

    }
}
