using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class DragAlien : MonoBehaviour
{
    public UnityEvent OnSelectAlien;
    public UnityEvent UnSelectAlien;
    public GameObject MainMenu;
    public GameObject AlienMenu;


    private void OnMouseUp()
    {
       // MainMenu.SetActive(false);
       // AlienMenu.SetActive(true);
        //OnSelectAlien.Invoke();
    }

    private void OnMouseExit()
    {
        /*
        MainMenu.SetActive(true);
        AlienMenu.SetActive(false);
        UnSelectAlien.Invoke();*/
    }
}
