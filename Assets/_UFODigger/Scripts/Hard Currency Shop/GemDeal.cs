using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class GemDeal : MonoBehaviour
{
    [SerializeField] private int _gemsCount;
    [SerializeField] private float _price;

    [SerializeField] private TextMeshProUGUI _priceText;
    [SerializeField] private TextMeshProUGUI _gemsCountText;


    private void Start()
    {
        _priceText.text = $"{_price}$";
        _gemsCountText.text = _gemsCount.ToString();
    }
}
