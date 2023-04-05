using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISkillMenuClose : MonoBehaviour
{
    [SerializeField] private UISkillMenu _uiSkill;
    [SerializeField] private UIMainMenu _uiMainMenu;

    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(CloseSettingsMenu);
    }

    private void CloseSettingsMenu()
    {
        _uiSkill.CloseSkillMenu();
        _uiMainMenu.ShowMainMenu();
    }
}
