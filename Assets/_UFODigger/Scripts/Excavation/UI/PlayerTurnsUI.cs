using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerTurnsUI : MonoBehaviour
{
    public UpgradesSO PlayerTurns;
    [SerializeField] private TextMeshProUGUI _turnsCount;
    [SerializeField] private Scrollbar _scrollbar;
    [SerializeField] private GameObject _giftTurns;
    [SerializeField] private GameObject _giftWindow;

    [SerializeField] private Button _giftButton;

    [SerializeField] private Color _fewTurns;
    [SerializeField] private Color _manyTurns;
    [SerializeField] private Image _sliderBarImage;


    private int _playerTurns;
    private int _maxPlayerTurns;

    public bool _wasADSShow;

    private void Start()
    {

        _giftTurns.SetActive(false);
        _giftWindow.SetActive(false);

        _maxPlayerTurns = PlayerTurns.GetTotalPower();
        _playerTurns = PlayerTurns.GetTotalPower();

        _turnsCount.text = _playerTurns.ToString();

        _giftButton.onClick.AddListener(ShowGiftOffer);
    }

    public void UpdateTurnsUI()
    {
        _playerTurns -= 1;
        _turnsCount.text = _playerTurns.ToString();
        
        _scrollbar.size = (float)_playerTurns / (float)_maxPlayerTurns;

        if (_playerTurns <= (float)_maxPlayerTurns * 0.25 && !_wasADSShow)
        {
            _giftTurns.SetActive(true);
            _sliderBarImage.color = _fewTurns;
        }
        else
        {
            _giftTurns.SetActive(false);
            _sliderBarImage.color = _manyTurns;
        }

    }

    public void ShowGiftOffer()
    {
        _giftWindow.SetActive(true);
    }

    public void HideGiftOffer()
    {
        _giftWindow.SetActive(false);
    }

    public void AddTurns(int turs, bool fromskill = false)
    {
        _playerTurns += turs;
        _turnsCount.text = _playerTurns.ToString();
        _scrollbar.size = (float)_playerTurns / (float)_maxPlayerTurns;

        _giftTurns.SetActive(false);
        if (!fromskill)
        {
            _wasADSShow = true;
        }
       
        if (_playerTurns <= (float)_maxPlayerTurns * 0.25 && !_wasADSShow)
        {
            _giftTurns.SetActive(true);
            _sliderBarImage.color = _fewTurns;
        }
        else
        {
            _giftTurns.SetActive(false);
            _sliderBarImage.color = _manyTurns;
        }
    }
}
