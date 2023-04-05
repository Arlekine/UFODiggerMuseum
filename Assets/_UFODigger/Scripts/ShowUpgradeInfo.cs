using UnityEngine;
using UnityEngine.UI;

public class ShowUpgradeInfo : MonoBehaviour
{
    public Button ShowInfo;
    public UpgradesSO Upgrade;
    public UIInfo UIInfo;

    private void Start()
    {
        ShowInfo.onClick.AddListener(Show);
    }

    private void Show()
    {
        UIInfo.Setup(Upgrade.Description);
    }
}
