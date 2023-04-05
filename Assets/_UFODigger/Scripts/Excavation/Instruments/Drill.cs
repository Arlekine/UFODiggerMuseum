
using System;

public class Drill : Instrument
{
 
    private void Start()
    {
        CalculateDiff();
        SetPower();
    }

    private void SetPower()
    {
        if(!Data.IsInstrumentUnlock)
            return;
        
        var instrumentUpgradeLevel = Data.Upgrades[Data.LevelOfUpgrade];

        PartPower.Add(instrumentUpgradeLevel.Power[0]);
    }
}
