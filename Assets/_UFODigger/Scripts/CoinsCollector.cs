using System;
using System.Collections.Generic;
using UnityEngine;

public class CoinsCollector : MonoBehaviour
{
    public PLayerData PLayerData;
    public GameObject PrefabCoin;
    private static List<Vector3> CoinsPosition = new List<Vector3>();
    private void Start()
    {
        foreach (var coinPosition in CoinsPosition)
        {
            Instantiate(PrefabCoin, coinPosition, PrefabCoin.transform.rotation);
        }
    }

    public void AddCoin(Vector3 coinPosition)
    {
        if (!CoinsPosition.Contains(coinPosition))
        {
            CoinsPosition.Add(coinPosition);
        }

    }
    
    public void Remove(Vector3 coinPosition)
    {
        if (CoinsPosition.Contains(coinPosition))
        {
            CoinsPosition.Remove(coinPosition);
        }
    }

    public void OnApplicationQuit()
    {
        var adobedCoins = 0;
        foreach (var coinPos in CoinsPosition)
        {
            adobedCoins += PrefabCoin.GetComponent<Coin>().GoldCount();
        }

        PLayerData.AbandonedGold = adobedCoins;
    }
}