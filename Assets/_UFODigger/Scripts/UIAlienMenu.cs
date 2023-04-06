using UnityEngine;
using Utils;

public class UIAlienMenu : Singleton<UIAlienMenu>
{
    private Animation _animation;
    public Stand AlienOnStand;

    public GameObject MapButton;
    public GameObject ExcavateButton;
    
    public Alien OpenMenuButtonAfterAlien;
    public GameObject[] Buttons;

    [Space] 
    [SerializeField] private Camera _camera;
    [SerializeField] private Canvas _canvas;
    [SerializeField] private RectTransform _rectTransform;
    
    private void Start()
    {
        _animation = GetComponent<Animation>();
        _rectTransform = GetComponent<RectTransform>();

        for (int i = 0; i < Buttons.Length; i++)
        {
            if (OpenMenuButtonAfterAlien == null)
            {
                Buttons[i].SetActive(false);
            }
            else
            {
                if (OpenMenuButtonAfterAlien.IsAlienOpen)
                {
                    Buttons[i].SetActive(true);
                }
                else
                {
                    Buttons[i].SetActive(false);
                }
            }
        }
    }

    private void Update()
    {
        if (AlienOnStand != null) 
            _rectTransform.anchoredPosition = _camera.WorldToScreenPoint(AlienOnStand.transform.position) / _canvas.transform.localScale.x;
    }

    public void ShowAlienMenu()
    {
        if (_animation != null)
            _animation.Play("Main Menu Show");
    }

    public void HideAlienMenu()
    {
        if (_animation != null)
            _animation.Play("Main Menu Hide");
    }

    public void ShowMap()
    {
        ExcavateButton.SetActive(false);
        MapButton.SetActive(true);
    }
    
    public void ShowExcavate()
    {
        ExcavateButton.SetActive(true);
        MapButton.SetActive(false);
    }
}
