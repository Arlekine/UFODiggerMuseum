using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIWiki : MonoBehaviour
{
    public int AlienCount = 10;
    public WikiDescription WikiDescription;

    public GameObject ScrollView;

    [SerializeField] private Aliens _aliens;
    [SerializeField] private Transform _content;
    [SerializeField] private Sprite _closedAlien;
    [SerializeField] private GameObject _alienIcon;
    [SerializeField] private TextMeshProUGUI _alienCounter;

    private Animation _animation;

    private int _openAliens;

    private void Start()
    {
        SetAliens();

        _animation = GetComponent<Animation>();
        WikiDescription.OnDescriptionClose.AddListener(ShowContent);
    }

    private void ShowContent()
    {
        ScrollView.SetActive(true);
    }

    private void SetAliens()
    {
        foreach (var alien in _aliens.AllAliens)
        {
            if (!alien.IsAlienOpen) continue;
            
            var alienIcon = Instantiate(_alienIcon, _content);
            var uiWikiIcon = alienIcon.GetComponent<UIWikiIcon>();

            alienIcon.GetComponentInChildren<Button>().onClick.AddListener(delegate { ShowDescription(alien); });

            uiWikiIcon.SetSprite(alien.Shot);
            uiWikiIcon.SetLocalizateReference(alien.name);

            _openAliens++;
        }

        for (int i = _openAliens; i < AlienCount; i++)
        {
            var alienIcon = Instantiate(_alienIcon, _content);
            var uiWikiIcon = alienIcon.GetComponent<UIWikiIcon>();
            uiWikiIcon.SetLocalizateReference("");
        }

        _alienCounter.text = $"{_openAliens}/{AlienCount}";
    }

    private void ShowDescription(Alien alien)
    {
        WikiDescription.Setup(alien);
        ScrollView.SetActive(false);
    }

    public void ShowWiki()
    {
        if (_animation != null)
            _animation.Play("Wiki Show");
    }

    public void HideWiki()
    {
        if (_animation != null)
            _animation.Play("Wiki Hide");
    }
}