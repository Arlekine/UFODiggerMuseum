using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIAlienProgress : MonoBehaviour
{
    public TextMeshProUGUI ProgressText;
    public Image Bar;


    public void UpdateValue(int val,int totalValue)
    {
        var amount = (float)val/(float)totalValue; 
        Bar.fillAmount = amount;
        ProgressText.text = $"Alien Progress: {(int)(amount*100)}%";
    }

   
}
