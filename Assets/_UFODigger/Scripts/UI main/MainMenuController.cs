using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

public class MainMenuController : Singleton<MainMenuController>
{
    public GameObject MainMenu;
    public GameObject StandsMenu;
    public GameObject AlienMenu;
    public GameObject SettingsButton;

    public UISettings SettingsMenu;

    private Stand[] stands;

    private void Start()
    {
        ShowMainMenu();

        stands = FindObjectsOfType<Stand>();
    }
    public void ShowMainMenu()
    {
        MainMenu.SetActive(true);
        SettingsButton.SetActive(true);
        StandsMenu.SetActive(false);
        AlienMenu.SetActive(false);

        CameraMove.Instance.TurnOnMouseCameraControl();
        
        stands = FindObjectsOfType<Stand>();
        for (int i = 0; i < stands.Length; i++)
        {
            stands[i].HideStandMenuButton();
        }
    }

    public void ShowStandsMenu()
    {
        MainMenu.SetActive(false);
        SettingsButton.SetActive(false);
        StandsMenu.SetActive(true);
        AlienMenu.SetActive(false);

        CameraMove.Instance.TurnOnMouseCameraControl();
        
        stands = FindObjectsOfType<Stand>();
        for (int i = 0; i < stands.Length; i++)
        {
            stands[i].ShowStandMenuButton();
        }
      
    }
    
    public void ShowAlienMenu(Stand stand)
    {
        var uiAlienMenu = AlienMenu.GetComponent<UIAlienMenu>();
        uiAlienMenu.AlienOnStand = stand;
        
        //if (stand.AlienOnStand == null)
        //{
        //    uiAlienMenu.ShowMap();
        //}
        //else
        //{
            uiAlienMenu.ShowExcavate();
        //}

        MainMenu.SetActive(false);
        SettingsButton.SetActive(false);
        StandsMenu.SetActive(false);
        AlienMenu.SetActive(true);
        
        CameraMove.Instance.TurnOffMouseCameraControl();
        CameraMove.Instance.MoveCamera(stand.transform.position,stand.CameraOnStandZOffset);
        
        stands = FindObjectsOfType<Stand>();
        for (int i = 0; i < stands.Length; i++)
        {
            stands[i].HideStandMenuButton();
        }

    }
}