using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandFundament : MonoBehaviour
{
    public GameObject BuildVFX;
    public GameObject Platform;
    
    public void Build(bool vfxOn)
    {
        if (vfxOn)
            BuildVFX.SetActive(true);

        Platform.SetActive(true);
    }
}
