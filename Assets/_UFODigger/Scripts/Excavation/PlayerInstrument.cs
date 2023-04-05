using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInstrument : MonoBehaviour
{
    [SerializeField] private Instrument _dinamite;
    [SerializeField] private DinamiteData _dinamiteData;
    [SerializeField] private UISkillsBag _skillsBag;

    private Instrument _instrument;
    private Instrument _oldInstrument;
    public bool IsOneTurnInstrument { get; private set; }
    public void SetInstrument(Instrument instrument)
    {
        _instrument = instrument;
    }

    public void SetInstrumentOnOneTurn()
    {
        _oldInstrument = _instrument;
        _instrument = _dinamite;

        IsOneTurnInstrument = true;
    }

    //return old instrument after use dinamite(one turn instrument)
    public void ReturnOldInstrument()
    {
        IsOneTurnInstrument = false;
        if (_oldInstrument)
        {
            _instrument = _oldInstrument;
            _skillsBag.RemoveSkill(_dinamiteData as ISkill);
        }
    }

    public Instrument GetInstrument()
    {
        return _instrument;
    }
}
