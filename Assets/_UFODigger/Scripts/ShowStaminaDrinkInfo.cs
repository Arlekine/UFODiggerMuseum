using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowStaminaDrinkInfo : MonoBehaviour
{
    public Button ShowInfo;
    public StaminaDrinkSo Stamina;
    public UIInfo UIInfo;

    private void Start()
    {
        ShowInfo.onClick.AddListener(Show);
    }

    private void Show()
    {
        UIInfo.Setup(Stamina.Info);
    }
}
