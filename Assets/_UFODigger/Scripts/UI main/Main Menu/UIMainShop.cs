using UnityEngine;
using UnityEngine.UI;

public class UIMainShop : MonoBehaviour
{
    [SerializeField] private ShopManager _shop;
    [SerializeField] private UIMainMenu _uiMainMenu;

    private void Awake()
    {
        AddListener();
    }
    private void AddListener()
    {
        GetComponent<Button>().onClick.AddListener(OpenShop);
    }
    private void OpenShop()
    {
        _shop.OpenShop();
        _uiMainMenu.HideMainMenu();
    }
    public void RemoveListener()
    {
        GetComponent<Button>().onClick.RemoveListener(OpenShop);
    }
}
