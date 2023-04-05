using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StadAreaZone : MonoBehaviour
{
    public bool IsAreaFree { get; private set; } = true;
    private List<Collider> _zonesInStandAreas = new List<Collider>();
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Stand Zone" || other.tag == "Close for stand zones")
        {
            _zonesInStandAreas.Add(other);
            IsAreaFree = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Stand Zone" || other.tag == "Close for stand zones")
        {
            if (_zonesInStandAreas.Contains(other))
            {
                _zonesInStandAreas.Remove(other);
                if (_zonesInStandAreas.Count==0)
                {
                    IsAreaFree = true;
                }
            }
        }
    }
}
