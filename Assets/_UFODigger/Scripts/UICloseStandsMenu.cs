using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UICloseStandsMenu : MonoBehaviour
{
    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(CloseStandsMenu);
    }

    private void CloseStandsMenu()
    {
        MainMenuController.Instance.ShowMainMenu();
    }
}
