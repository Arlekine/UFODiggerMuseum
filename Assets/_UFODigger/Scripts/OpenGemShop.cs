using UnityEngine;
using UnityEngine.UI;

public class OpenGemShop : MonoBehaviour
{
    public ShopManager ShopManager;
    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(OpenShop);
    }

    private void OpenShop()
    {
        ShopManager.OpenShop();
    }
}
