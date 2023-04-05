using System;
using UnityEngine;
using UnityEngine.UI;

public class UIWikiClose : MonoBehaviour
{
    [SerializeField] private UIWiki _uiWiki;
    [SerializeField] private UIStandsMenu _uiStandMenu;


    void Start()
    {
        GetComponent<Button>().onClick.AddListener(CloseWiki);
    }

    private void CloseWiki()
    {
        _uiWiki.HideWiki();
        _uiStandMenu.ShowStandMenu();
    }
}
