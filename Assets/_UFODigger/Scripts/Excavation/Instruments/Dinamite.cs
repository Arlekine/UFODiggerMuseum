using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dinamite : Instrument
{
    private void Start()
    {
        CalculateDiff();
        SetPower();
    }

    private void SetPower()
    {
        var instrumentUpgradeLevel = Data.Upgrades[Data.LevelOfUpgrade];

        
        for (int i = 0; i < FormOfExcavation.Length; i++)
        {
            PartPower.Add(instrumentUpgradeLevel.Power[0]);
        }
    }
}
