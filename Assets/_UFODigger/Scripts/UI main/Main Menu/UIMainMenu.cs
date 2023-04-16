using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class UIMainMenu : MonoBehaviour
{
    public Alien OpenMenuButtonAfterAlien;
    //public UISkillMenuOpen UISkillMenuOpen;
    //public UIOpenUpgradesButton UIOpenUpgrades;

    public Sprite UnavailableBack;
    public Color _activeColor, _deactivColor;
    public GameObject Preview;

    [Space]
    public RectTransform settingsButton;
    public float buttonShowPos;
    public float buttonHidePos;
    public float buttonAnimateTime;
    
    private Animation _animation;

    private void Start()
    {
        _animation = GetComponent<Animation>();

        if (OpenMenuButtonAfterAlien.IsAlienOpen)
        {
            //UISkillMenuOpen.GetComponent<Button>().onClick.RemoveListener(ShowPreview);
            //UIOpenUpgrades.GetComponent<Button>().onClick.RemoveListener(ShowPreview);

            //UISkillMenuOpen.GetComponent<Image>().color = _activeColor;
            //UIOpenUpgrades.GetComponent<Image>().color = _activeColor;
        }
        else
        {
            //UISkillMenuOpen.RemoveListener();
            //UIOpenUpgrades.RemoveListener();

            //UISkillMenuOpen.GetComponent<Button>().onClick.AddListener(ShowPreview);
            //UIOpenUpgrades.GetComponent<Button>().onClick.AddListener(ShowPreview);

            //UISkillMenuOpen.GetComponent<Image>().sprite = UnavailableBack;
            //UIMainShop.GetComponent<Image>().sprite = UnavailableBack;
            //UIOpenUpgrades.GetComponent<Image>().sprite = UnavailableBack;

            //UISkillMenuOpen.GetComponent<Image>().color = _deactivColor;
            //UIOpenUpgrades.GetComponent<Image>().color = _deactivColor;
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

        settingsButton.DOAnchorPosY(buttonShowPos, buttonAnimateTime);
    }

    public void HideMainMenu()
    {
        if (_animation != null)
            _animation.Play("Main Menu Hide");

        settingsButton.DOAnchorPosY(buttonHidePos, buttonAnimateTime);
    }
}