using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class UIOpenMuseumArea : MonoBehaviour
{

    [FormerlySerializedAs("AddTargetInticatorCanvas")] public AddTargetIndicator AddTargetIndicator;
    public Alien AvailableBuildAfterAlien;

    public Stand ShowOpenMuseumStand;

    public Stand BuildingStand;
    public UIBuildStand _uiBuild;
    public GameObject BuildUI;
    public Button BuildAreaButton;

    public Image LockBackground;
    public Color AvailableColor;
    public Color NotAvailableColor;

    private void Start()
    {
        if (AvailableBuildAfterAlien == null)
        {
            if (ShowOpenMuseumStand.IsStandBuild)
            {
                Debug.Log(ShowOpenMuseumStand.IsStandBuild);
                BuildUI.SetActive(false);
                LockBackground.color = NotAvailableColor;
                AddTargetIndicator.Deactivate();
            }
            else
            {
                BuildUI.SetActive(false); 
                AddTargetIndicator.Deactivate();
            }

            return;
        }

        AvailableBuildAfterAlien.LoadAlienData();
        if (ShowOpenMuseumStand.IsStandBuild && !BuildingStand.IsStandBuild)
        {
            if (AvailableBuildAfterAlien.IsAlienOpen)
            {
                BuildUI.SetActive(true);
                BuildAreaButton.GetComponent<Button>().onClick.AddListener(ShowBuildInfo);
                LockBackground.color = AvailableColor;
                AddTargetIndicator.enabled = true;
                AddTargetIndicator.Activate();
            }
            else
            {
                LockBackground.color = NotAvailableColor;
                BuildUI.SetActive(false);
                AddTargetIndicator.Deactivate();
            }
        }
        else
        {
            BuildUI.SetActive(false);
            AddTargetIndicator.Deactivate();
        }
    }

    private void ShowBuildInfo()
    {
        if (_uiBuild != null)
        {
            _uiBuild.ShowWindow(BuildingStand, this);
        }
        else
        {
            Debug.LogWarning("Not found component _uiBuild");
        }
    }

    public void Hide()
    {
        AddTargetIndicator.Deactivate();
        BuildUI.SetActive(false);
    }
}