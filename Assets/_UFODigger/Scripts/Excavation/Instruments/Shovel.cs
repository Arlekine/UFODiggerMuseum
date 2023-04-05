public class Shovel : Instrument
{
    private void Start()
    {
        CalculateDiff();
        SetPower();
    }

    private void SetPower()
    {
        if (!Data.IsInstrumentUnlock)
            return;

        var upgradeLevel = Data.Upgrades[Data.LevelOfUpgrade];

        PartPower.Add(upgradeLevel.Power[0]);
        PartPower.Add(upgradeLevel.Power[1]);
        PartPower.Add(upgradeLevel.Power[1]);
        PartPower.Add(upgradeLevel.Power[0]);
        PartPower.Add(upgradeLevel.Power[1]);
        PartPower.Add(upgradeLevel.Power[1]);
        PartPower.Add(upgradeLevel.Power[0]);
        PartPower.Add(upgradeLevel.Power[2]);
        PartPower.Add(upgradeLevel.Power[2]);
        PartPower.Add(upgradeLevel.Power[2]);
        PartPower.Add(upgradeLevel.Power[2]);
        PartPower.Add(upgradeLevel.Power[2]);
        PartPower.Add(upgradeLevel.Power[2]);
    }
}