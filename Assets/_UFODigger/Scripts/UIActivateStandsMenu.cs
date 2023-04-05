using UnityEngine;
using UnityEngine.UI;

public class UIActivateStandsMenu : MonoBehaviour
{
    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(OpenStandsMenu);
    }

    private void OpenStandsMenu()
    {
        MainMenuController.Instance.ShowStandsMenu();
    }
}
