using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UICollectAlien : MonoBehaviour
{
    public int GoldForCollect;
    public PLayerData PLayerData;
    public TextMeshProUGUI Count;
    public TextMeshProUGUI Name;
    public void Init(Alien alien)
    {
        Count.text = GoldForCollect.ToString();
        gameObject.SetActive(true);
        PLayerData.AddGold(GoldForCollect);
        Name.text = $"You found all part of {alien.Name}!";
    }
}