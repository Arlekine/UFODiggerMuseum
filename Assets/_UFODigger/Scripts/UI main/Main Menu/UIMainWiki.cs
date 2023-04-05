using System;
using UnityEngine;
using UnityEngine.UI;

public class UIMainWiki : MonoBehaviour
{
    [SerializeField] private UIWiki _uiWiki;
    [SerializeField] private UIStandsMenu _uiStandMenu;

    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(OpenWiki);
    }

    private void OpenWiki()
    {
        _uiWiki.ShowWiki();
        _uiStandMenu.HideStandMenu();
    }

}
