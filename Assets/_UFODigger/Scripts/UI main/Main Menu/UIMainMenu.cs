using UnityEngine;
using UnityEngine.UI;

public class UIMainMenu : MonoBehaviour
{
    public Alien OpenMenuButtonAfterAlien;
    public UISkillMenuOpen UISkillMenuOpen;
    public UIMainShop UIMainShop;
    public UIOpenUpgradesButton UIOpenUpgrades;

    public Sprite UnavailableBack;
    public Color _activeColor, _deactivColor;
    public GameObject Preview;
    private Animation _animation;

    public RectTransform _settingsButton;

    private void Start()
    {
        _animation = GetComponent<Animation>();

        if (OpenMenuButtonAfterAlien.IsAlienOpen)
        {
            UISkillMenuOpen.GetComponent<Button>().onClick.RemoveListener(ShowPreview);
            UIMainShop.GetComponent<Button>().onClick.RemoveListener(ShowPreview);
            UIOpenUpgrades.GetComponent<Button>().onClick.RemoveListener(ShowPreview);

            UISkillMenuOpen.GetComponent<Image>().color = _activeColor;
            UIMainShop.GetComponent<Image>().color = _activeColor;
            UIOpenUpgrades.GetComponent<Image>().color = _activeColor;
        }
        else
        {
            UISkillMenuOpen.RemoveListener();
            UIMainShop.RemoveListener();
            UIOpenUpgrades.RemoveListener();

            UISkillMenuOpen.GetComponent<Button>().onClick.AddListener(ShowPreview);
            UIMainShop.GetComponent<Button>().onClick.AddListener(ShowPreview);
            UIOpenUpgrades.GetComponent<Button>().onClick.AddListener(ShowPreview);

            //UISkillMenuOpen.GetComponent<Image>().sprite = UnavailableBack;
            //UIMainShop.GetComponent<Image>().sprite = UnavailableBack;
            //UIOpenUpgrades.GetComponent<Image>().sprite = UnavailableBack;

            UISkillMenuOpen.GetComponent<Image>().color = _deactivColor;
            UIMainShop.GetComponent<Image>().color = _deactivColor;
            UIOpenUpgrades.GetComponent<Image>().color = _deactivColor;
        }
    }

    private void ShowPreview()
    {
        Preview.SetActive(true);
    }

    public void ShowMainMenu()
    {
        if (_animation != null)
            _animation.Play("Main Menu Show");
    }

    public void HideMainMenu()
    {
        if (_animation != null)
            _animation.Play("Main Menu Hide");
    }
}