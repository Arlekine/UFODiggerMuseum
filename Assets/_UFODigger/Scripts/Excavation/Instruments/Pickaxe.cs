
public class Pickaxe : Instrument
{
    private void Start()
    {
        CalculateDiff();
        SetPower();
    }

    private void SetPower()
    {
        var instrumentUpgradeLEvel = Data.Upgrades[Data.LevelOfUpgrade];

        PartPower.Add(instrumentUpgradeLEvel.Power[0]);
        PartPower.Add(instrumentUpgradeLEvel.Power[1]);
        PartPower.Add(instrumentUpgradeLEvel.Power[2]);
        PartPower.Add(instrumentUpgradeLEvel.Power[1]);
        PartPower.Add(instrumentUpgradeLEvel.Power[1]);
        PartPower.Add(instrumentUpgradeLEvel.Power[2]);
        PartPower.Add(instrumentUpgradeLEvel.Power[1]);
    }

}
