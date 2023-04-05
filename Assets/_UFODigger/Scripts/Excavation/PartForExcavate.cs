using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PartForExcavate : MonoBehaviour
{
    public Vector3 PartInPosition = Vector3.zero;
    public List<PartPosition> PartPositions = new List<PartPosition>();

    public Transform FirstSotaInPartPosition;
    public GameObject partObject;

    public bool IsNewPart;
}

[Serializable]
public class PartPosition
{
    public int[] RelativePosition = new int[2];
}
