using System;
using UnityEngine;
using UnityEngine.Purchasing;
using UI;

public class PurchasesManager : MonoBehaviour, IStoreListener
{
    public UIRobotPurchase RobotPurchase;
    public event Action<ProductMetadata> RobotMetadataUpdated;
    public event Action<ProductMetadata> Gem_1MetadataUpdated;
    public event Action<ProductMetadata> Gem_2MetadataUpdated;
    public event Action<ProductMetadata> Gem_3MetadataUpdated;
    public event Action<ProductMetadata> Gem_4MetadataUpdated;

    [SerializeField] private PLayerData _playerData;

    private static IStoreController _StoreController;
    private static IExtensionProvider _StoreExtensionProvider;

    // GOOGLE PLAY //
    private const string _gpGems1_Android = "ufo.diggers.gems_100";
    private const string _gpGems2_Android = "ufo.diggers.gems_550";
    private const string _gpGems3_Android = "ufo.diggers.gems_1200";
    private const string _gpGems4_Android = "ufo.diggers.gems_1000";

    private const string _gpRobot_Android = "ufo.diggers.robot";

    // APP STORE //
    private const string _gpGems1_IOS = "ufo.diggers.gems100_IOS";
    private const string _gpGems2_IOS = "ufo.diggers.gems550_IOS";
    private const string _gpGems3_IOS = "ufo.diggers.gems1200_IOS";
    private const string _gpGems4_IOS = "ufo.diggers.gems1000_IOS";

    private const string _gpRobot_IOS = "ufo.diggers.robot_IOS";


    //UNITY id
    private const string _gems1 = "gems_1";
    private const string _gems2 = "gems_2";
    private const string _gems3 = "gems_3";
    private const string _gems4 = "gems_4";

    private const string _robot = "robot";

    //Google id
    private string _gpGems1;
    private string _gpGems2;
    private string _gpGems3;
    private string _gpGems4;

    private string _gpRobot;

    [SerializeField] private bool _firstPurchase;

    bool _pursh;

    private void Start()
    {
        if (_StoreController == null)
        {
            InitializePurchasing();
        }
    }

    public void InitializePurchasing()
    {
        if (IsInitialized())
        {
            return;
        }

#if UNITY_ANRDOID

    _gpGems1 = _gpGems1_Android;
    _gpGems2 = _gpGems2_Android;
    _gpGems3 = _gpGems3_Android;
    _gpGems4 = _gpGems4_Android;
    _gpRobot = _gpRobot_Android;
    
#elif UNITY_IOS

    _gpGems1 = _gpGems1_IOS;
    _gpGems2 = _gpGems2_IOS;
    _gpGems3 = _gpGems3_IOS;
    _gpGems4 = _gpGems4_IOS;
    _gpRobot = _gpRobot_IOS;

#endif

        //Debug.Log(_gpGems1);

        var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());

        builder.AddProduct(_gems1, ProductType.Consumable, new IDs() {{_gpGems1, GooglePlay.Name}});
        builder.AddProduct(_gems2, ProductType.Consumable, new IDs() {{_gpGems2, GooglePlay.Name}});
        builder.AddProduct(_gems3, ProductType.Consumable, new IDs() {{_gpGems3, GooglePlay.Name}});
        builder.AddProduct(_gems4, ProductType.Consumable, new IDs() {{_gpGems4, GooglePlay.Name}});

        builder.AddProduct(_robot, ProductType.NonConsumable, new IDs() {{_gpRobot, GooglePlay.Name}});

        UnityPurchasing.Initialize(this, builder);

        if (PlayerPrefs.HasKey("_firstPurchase"))
            _firstPurchase = (PlayerPrefs.GetInt("_firstPurchase") == 1) ? true : false;
        else
            _firstPurchase = false;
    }

    private bool IsInitialized()
    {
        return _StoreController != null && _StoreExtensionProvider != null;
    }

    public void BuyProductID(string productId)
    {
        if (IsInitialized())
        {
            Product product = _StoreController.products.WithID(productId);

            if (product != null && product.availableToPurchase)
            {
                Debug.Log(string.Format("Purchasing product asychronously: '{0}'", product.definition.id));
                
                _StoreController.InitiatePurchase(product);
            }
            else
            {
                Debug.Log(
                    "BuyProductID: FAIL. Not purchasing product, either is not found or is not available for purchase");
            }
        }
        else
        {
            Debug.Log("BuyProductID FAIL. Not initialized.");
        }
    }

    public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
    {
        Debug.Log("OnInitialized: PASS");

        _StoreController = controller;
        _StoreExtensionProvider = extensions;

        foreach (var product in _StoreController.products.all)
        {
            if (product != null)
            {
                switch (product.definition.id)
                {
                    case _robot:
                        RobotMetadataUpdated?.Invoke(product.metadata);
                        break;
                    case _gems1:
                        Gem_1MetadataUpdated?.Invoke(product.metadata);
                        break;
                    case _gems2:
                        Gem_2MetadataUpdated?.Invoke(product.metadata);
                        break;
                    case _gems3:
                        Gem_3MetadataUpdated?.Invoke(product.metadata);
                        break;
                    case _gems4:
                        Gem_4MetadataUpdated?.Invoke(product.metadata);
                        break;
                }
            }
        }
    }

    public void OnInitializeFailed(InitializationFailureReason error)
    {
        Debug.Log("OnInitializeFailed InitializationFailureReason:" + error);
    }

    public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
    {
        Debug.Log(string.Format("OnPurchaseFailed: FAIL. Product: '{0}', PurchaseFailureReason: {1}",
            product.definition.storeSpecificId, failureReason));
    }

    public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs purchaseEvent)
    {
        if (String.Equals(purchaseEvent.purchasedProduct.definition.id, _gems1, StringComparison.Ordinal))
        {
            Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'",
                purchaseEvent.purchasedProduct.definition.id));
            _playerData.AddGems(100);

            _pursh = true;
        }
        else if (String.Equals(purchaseEvent.purchasedProduct.definition.id, _gems2, StringComparison.Ordinal))
        {
            Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'",
                purchaseEvent.purchasedProduct.definition.id));
            _playerData.AddGems(550);

            _pursh = true;
        }
        else if (String.Equals(purchaseEvent.purchasedProduct.definition.id, _gems3, StringComparison.Ordinal))
        {
            Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'",
                purchaseEvent.purchasedProduct.definition.id));
            _playerData.AddGems(1200);

            _pursh = true;
        }
        else if (String.Equals(purchaseEvent.purchasedProduct.definition.id, _gems4, StringComparison.Ordinal))
        {
            Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'",
                purchaseEvent.purchasedProduct.definition.id));
            _playerData.AddGems(1000);

            _pursh = true;
        }  
        else if (String.Equals(purchaseEvent.purchasedProduct.definition.id, _robot, StringComparison.Ordinal))
        {
            Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'",
                purchaseEvent.purchasedProduct.definition.id));
            RobotPurchase.UnlockRobot();

            _pursh = true;
        }
        else
        {
            Debug.Log(string.Format("ProcessPurchase: FAIL. Unrecognized product: '{0}'",
                purchaseEvent.purchasedProduct.definition.id));
        }

        //if(_pursh)
        //    AppMetricaManager.instance.Purchase(purchaseEvent.purchasedProduct.definition.id, purchaseEvent.purchasedProduct.transactionID, purchaseEvent.purchasedProduct.metadata.localizedPrice.ToString(), purchaseEvent.purchasedProduct.metadata.isoCurrencyCode);


        if (!_firstPurchase)
        {
            _firstPurchase = _pursh;
            PlayerPrefs.SetInt("_firstPurchase", _firstPurchase ? 1 : 0);

            //if (_firstPurchase)
            //    AppMetricaManager.instance.UniquePurchase();
        }

        return PurchaseProcessingResult.Complete;
    }
}