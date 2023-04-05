using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowAlienDetectorInfo : MonoBehaviour
{
    public Button ShowInfo;
    public FossilDetectorSO Fossil;
    public UIInfo UIInfo;

    private void Start()
    {
        ShowInfo.onClick.AddListener(Show);
    }

    private void Show()
    {
        UIInfo.Setup(Fossil.Info);
    }
}
