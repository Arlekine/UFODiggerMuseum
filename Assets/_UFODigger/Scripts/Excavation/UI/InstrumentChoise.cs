using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class InstrumentChoise : MonoBehaviour
{
    [SerializeField] private InstrumentChoise[] _anotherInstruments;
    [SerializeField] private bool _isDefaultInstrument;

    [SerializeField] private PlayerInstrument _playerInstrument;
    [SerializeField] private Instrument _instrumentToset;

    [SerializeField] private Color _choiseColor;
    [SerializeField] private Color _unchoiseColor;
    [SerializeField] private Image _instrumentBackgroundImage;
    public Image InstrumentIcon;
    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(SetInstrument);

        if (_isDefaultInstrument)
        {
            SetInstrument();
        }
        
        //InstrumentIcon.sprite = _instrumentToset.GetInstrumentData().Icon;
        
        if (!_instrumentToset.GetInstrumentData().IsInstrumentUnlock)
        {
            gameObject.SetActive(false);
        }
    }

    private void SetInstrument()
    {
        //_instrumentBackgroundImage.color = _choiseColor;
        _playerInstrument.SetInstrument(_instrumentToset);

        for (int i = 0; i < _anotherInstruments.Length; i++)
        {
            _anotherInstruments[i].UnsetInstrument();
        }
    }

    public void UnsetInstrument()
    {
        //_instrumentBackgroundImage.color = _unchoiseColor;
    }
}
