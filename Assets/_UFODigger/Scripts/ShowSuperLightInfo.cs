using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowSuperLightInfo : MonoBehaviour
{
    public Button ShowInfo;
    public SuperLightSO SuperLight;
    public UIInfo UIInfo;

    private void Start()
    {
        ShowInfo.onClick.AddListener(Show);
    }

    private void Show()
    {
        UIInfo.Setup(SuperLight.Info);
    }
}
