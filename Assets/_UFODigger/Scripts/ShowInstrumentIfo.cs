using UnityEngine;
using UnityEngine.UI;

public class ShowInstrumentIfo : MonoBehaviour
{
    public Button ShowInfo;
    public InstrumentData Instrument;
    public UIInfo UIInfo;

    private void Start()
    {
        ShowInfo.onClick.AddListener(Show);
    }

    private void Show()
    {
        UIInfo.Setup(Instrument.Description);
    }
}