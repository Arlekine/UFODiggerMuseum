using System;
using UnityEngine;
using UnityEngine.Purchasing;

public class InAppPurchaseManager : IAPListener
{
	public UIRobotPurchase RobotPurchase;
	[SerializeField] private bool _firstPurchase;
	[SerializeField] private PLayerData _playerData;

	private void Awake()
	{
		onPurchaseComplete.AddListener(OnPurchaseComplete);

		if (PlayerPrefs.HasKey("_firstPurchase"))
			_firstPurchase = (PlayerPrefs.GetInt("_firstPurchase") == 1) ? true : false;
		else
			_firstPurchase = false;

		switch (Application.platform)
		{
			case RuntimePlatform.WSAPlayerX86:
			case RuntimePlatform.WSAPlayerX64:
			case RuntimePlatform.WSAPlayerARM:
				CodelessIAPStoreListener.Instance.GetStoreExtensions<IMicrosoftExtensions>()
					.RestoreTransactions();
				break;
			case RuntimePlatform.IPhonePlayer:
			case RuntimePlatform.OSXPlayer:
			case RuntimePlatform.tvOS:
				CodelessIAPStoreListener.Instance.GetStoreExtensions<IAppleExtensions>()
					.RestoreTransactions(null);
				break;
			case RuntimePlatform.Android:
				switch (StandardPurchasingModule.Instance().appStore)
				{
					case AppStore.GooglePlay:
						CodelessIAPStoreListener.Instance.GetStoreExtensions<IGooglePlayStoreExtensions>()
							.RestoreTransactions(null);
						break;
				}

				break;
			default:
				Debug.Log($"{Application.platform} is not a supported platform for the Codeless IAP restore");
				break;
		}
	}

	private void OnDestroy()
	{
		onPurchaseComplete.RemoveListener(OnPurchaseComplete);
	}

	private void OnPurchaseComplete(Product product)
	{

		if (!_firstPurchase)
		{
			_firstPurchase = true;
			PlayerPrefs.SetInt("_firstPurchase", _firstPurchase ? 1 : 0);
		}

		foreach (var payout in product.definition.payouts)
		{
			switch (payout.type)
			{
				case PayoutType.Currency:
					_playerData.AddGems((int)Math.Round(payout.quantity, MidpointRounding.AwayFromZero));
					break;

				case PayoutType.Item:
					RobotPurchase.UnlockRobot();
					break;
			}
		}
	}
}