using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UICloseUpgrades : MonoBehaviour
{
    [SerializeField] private UIUpgradeMenu _uiUpgrade;
    [SerializeField] private UIMainMenu _uiMainMenu;

    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(CloseSettingsMenu);
    }

    public void CloseSettingsMenu()
    {
        _uiUpgrade.CloseUpgrades();
        _uiMainMenu.ShowMainMenu();
    }
}
