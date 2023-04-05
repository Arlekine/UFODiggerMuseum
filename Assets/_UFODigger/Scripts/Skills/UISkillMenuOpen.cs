using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISkillMenuOpen : MonoBehaviour
{
    [SerializeField] private UISkillMenu _uiSkill;
    [SerializeField] private UIMainMenu _uiMainMenu;

    private void Awake()
    {
        AddListener();
    }
    
    private void AddListener()
    {
        GetComponent<Button>().onClick.AddListener(OpenSkillMenu);
    }
    
    private void OpenSkillMenu()
    {
        _uiSkill.OpenSkillMenu();
        _uiMainMenu.HideMainMenu();
    }
    
    public void RemoveListener()
    {
        GetComponent<Button>().onClick.RemoveListener(OpenSkillMenu);
    }
}