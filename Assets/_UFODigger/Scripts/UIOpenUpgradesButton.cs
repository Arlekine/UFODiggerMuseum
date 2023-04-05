using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIOpenUpgradesButton : MonoBehaviour
{
    [SerializeField] private UIUpgradeMenu _upgrades;
    [SerializeField] private UIMainMenu _uiMainMenu;

    private void Awake()
    {
        GetComponent<Button>().onClick.AddListener(OpenUpgrades);
    }

    private void OpenUpgrades()
    {
        _upgrades.OpenUpgrades();
        _uiMainMenu.HideMainMenu();
    }

    public void RemoveListener()
    {
        GetComponent<Button>().onClick.RemoveListener(OpenUpgrades);
    }
}
