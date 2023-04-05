using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UICloseAlienStandMenu : MonoBehaviour
{
    public UIAlienMenu AlienMenu;
    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(CloseAlienMenu);
    }

    private void CloseAlienMenu()
    {
        MainMenuController.Instance.ShowMainMenu();
        AlienMenu.AlienOnStand.DeActivateMove();
    }
}
