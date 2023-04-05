using System;
using TMPro;
using UnityEngine;
using UnityEngine.Purchasing;

public class ShopManager : MonoBehaviour
{
    //public PurchasesManager PurchasesManager;
    //public TextMeshProUGUI Gem1Price;
    //public TextMeshProUGUI Gem2Price;
    //public TextMeshProUGUI Gem3Price;
    //public TextMeshProUGUI Gem4Price;

    [SerializeField] private IAPButton _GemPack_100;
    
    private Animation _animation;

    private bool _isShopOpen = false;

    private void Awake()
    {

        //var product = CodelessIAPStoreListener.Instance.GetProduct(_GemPack_100.productId);
        //if (product != null)
        //{
        //    _GemPack_100.priceText.text = product.metadata.localizedPriceString;

        //    Debug.Log("______________________________IAP BUTTON: " + product.metadata.localizedPriceString);
        //}

        //PurchasesManager.Gem_1MetadataUpdated += SetupGem1;
        //PurchasesManager.Gem_2MetadataUpdated += SetupGem2;
        //PurchasesManager.Gem_3MetadataUpdated += SetupGem3;
        //PurchasesManager.Gem_4MetadataUpdated += SetupGem4;
    }

    //private void SetupGem1(ProductMetadata metadata)
    //{
    //    Gem1Price.text = $"{metadata.isoCurrencyCode} {metadata.localizedPrice:0.##}";
    //}

    //private void SetupGem2(ProductMetadata metadata)
    //{
    //    Gem2Price.text = $"{metadata.isoCurrencyCode} {metadata.localizedPrice:0.##}";
    //}

    //private void SetupGem3(ProductMetadata metadata)
    //{
    //    Gem3Price.text = $"{metadata.isoCurrencyCode} {metadata.localizedPrice:0.##}";
    //}

    //private void SetupGem4(ProductMetadata metadata)
    //{
    //    Gem4Price.text = $"{metadata.isoCurrencyCode} {metadata.localizedPrice:0.##}";
    //}
    private void Start()
    {
        _animation = GetComponent<Animation>();
    }

    public void OpenShop()
    {
        if (_animation != null && !_isShopOpen)
        {
            _isShopOpen = true;
            _animation.Play("Shop Show");
        }
    }
    public void CloseShop()
    {
        if (_animation != null && _isShopOpen)
        {
            _isShopOpen = false;
            _animation.Play("Shop Hide"); 
        }


    }

}