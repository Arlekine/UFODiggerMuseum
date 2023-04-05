using System.Collections.Generic;
using UnityEngine;

public class Instrument : MonoBehaviour
{
    [Header("Centr must be first")]
    [SerializeField] protected Transform[] FormOfExcavation; 
    [SerializeField] protected InstrumentData Data;

    protected List<Vector3> FormDiff = new List<Vector3>();
    protected List<int> PartPower = new List<int>();

    private void Awake()
    {
        Data.LoadData();
    }

    protected void CalculateDiff()
    {
        var centr = FormOfExcavation[0];

        for (int i = 1; i < FormOfExcavation.Length; i++)
        {
            FormDiff.Add(centr.position - FormOfExcavation[i].position);
        }
    }

    public InstrumentData GetInstrumentData()
    {
        return Data;
    }

    public List<Vector3> GetDiff()
    {
        return FormDiff;
    }

    public List<int> GetPartPower()
    {
        return PartPower;
    }
}
