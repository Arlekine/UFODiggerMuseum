using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;

public class UIBuildStand : MonoBehaviour
{
    [SerializeField] private Button AcceptBuildButton;
    [SerializeField] private Button CancelBuildButton;

    [SerializeField] private TextMeshProUGUI BuildPrice;
    
    [SerializeField] private GameObject BuildWindow;
    [SerializeField] private PLayerData PlayerData;

    [SerializeField] private Color EnoughGoldColor;
    [SerializeField] private Color NotEnoughGoldColor;
    
    private Stand _stand;

    private UIOpenMuseumArea _uiOpenMuseumArea;
    private void Start()
    {
        CancelBuildButton.onClick.AddListener(CloseWindow);
        BuildWindow.SetActive(false);
    }
    
    public void ShowWindow(Stand stand, UIOpenMuseumArea uiOpenMuseumArea)
    {
        _uiOpenMuseumArea = uiOpenMuseumArea;
        _stand = stand;
        BuildPrice.text = _stand.BuildPrice.ToString();
        if (PlayerData.GoldCount >= _stand.BuildPrice)
        {
            AcceptBuildButton.onClick.AddListener(BuildStand);
            BuildPrice.color = EnoughGoldColor;
        }
        else
        {
            AcceptBuildButton.onClick.RemoveListener(BuildStand);
            BuildPrice.color = NotEnoughGoldColor; 
        }

        BuildWindow.SetActive(true);
    }
    private void CloseWindow()
    {
        _stand = null;
        BuildWindow.SetActive(false);
    }

    private void BuildStand()
    {
        if (_stand == null)
        {
            Debug.LogWarning("No reference on stand for build!");
            return;
        }
        _uiOpenMuseumArea.Hide();
        _stand.Build();
        CloseWindow();
    }

  
}
