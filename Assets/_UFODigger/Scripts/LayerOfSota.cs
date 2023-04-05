using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Events;
using Random = UnityEngine.Random;

public class LayerOfSota : MonoBehaviour
{
    public int CountOfRandomIceObject;

    public int Layer;
    public int SotasHealth;
    public List<SotaInLayer> sotaInLayers = new List<SotaInLayer>();

    public float distanceBetweenLayers;
    public Material SotaWallsMaterial;
    public List<Material> SotaMaterials = new List<Material>();
    public List<AroundSotaPosition> AroundSotaPositions = new List<AroundSotaPosition>();

    public UnityEvent<List<SotaInLayer>, GameObject> OnPartSetup;

    [Space] [Header("Tests")] public bool IsPositionTest;
    public Material testFreeSotaMaterial;
    public Material testBusySotaMaterial;
    public Material testPartSotaMaterial;
    public bool IsRandomPartPositionTest;

    public GameObject TestPart;

    public Transform ScaleComponent;

    private List<SotaInLayer> _freeSotaInLayers = new List<SotaInLayer>();
    private List<SotaInLayer> _busySotaInLayers = new List<SotaInLayer>();

    private void TestSetup()
    {
        if (IsPositionTest || IsRandomPartPositionTest)
        {
            Setup(0);
            if (IsPositionTest)
            {
                StartCoroutine(TestSotaInLayer());
            }

            if (IsRandomPartPositionTest)
            {
                var part = Instantiate(TestPart);
                FindRandomPlaceForPart(part);
            }
        }
    }

    public void Setup(int layer)
    {
        Layer = layer;

        var randomSotaMaterial = SotaMaterials[UnityEngine.Random.Range(0, SotaMaterials.Count)];

        for (int i = 0; i < sotaInLayers.Count; i++)
        {
            sotaInLayers[i].RelativePosition[2] = Layer;
            sotaInLayers[i].sota.Health = SotasHealth;

            if (sotaInLayers[i].sota.IsFree)
            {
                sotaInLayers[i].sota.transform.GetComponent<MeshRenderer>().material = randomSotaMaterial;
                sotaInLayers[i].sota.transform.GetComponent<Sota>().SetFreeSota();
                _freeSotaInLayers.Add(sotaInLayers[i]);
            }

            if (!sotaInLayers[i].sota.IsFree)
            {
                sotaInLayers[i].sota.transform.GetComponent<Sota>().SetSotaWall();
                _busySotaInLayers.Add(sotaInLayers[i]);
            }
        }

        //Fake layer
        if (layer == -1)
        {
            for (int i = 0; i < _busySotaInLayers.Count; i++)
            {
                _busySotaInLayers[i].sota.RemoveSota();
            }

            var randomSotasIce = new List<SotaInLayer>();
            for (int i = 0; i < CountOfRandomIceObject; i++)
            {
                var randomSota = _freeSotaInLayers[Random.Range(0, _freeSotaInLayers.Count)];
                randomSota.sota.SetFakeIceSota();
                randomSotasIce.Add(randomSota);
                _freeSotaInLayers.Remove(randomSota);
            }

            for (int i = 0; i < _freeSotaInLayers.Count; i++)
            {
                _freeSotaInLayers[i].sota.RemoveSota();
            }
        }
    }

    public void FindRandomPlaceForPart(GameObject partObject)
    {
        var part = Instantiate(partObject, ScaleComponent);

        List<SotaInLayer> randomSota = new List<SotaInLayer>();

        var randomSotaPlace = _freeSotaInLayers[UnityEngine.Random.Range(0, _freeSotaInLayers.Count)];
        randomSota.Add(randomSotaPlace);

        var partForExcavate = part.GetComponent<PartForExcavate>();
        for (int i = 1; i < partForExcavate.PartPositions.Count; i++)
        {
            var nextRandomPosition = new int[3];
            nextRandomPosition[0] = randomSotaPlace.RelativePosition[0] +
                                    partForExcavate.PartPositions[i].RelativePosition[0];
            nextRandomPosition[1] = randomSotaPlace.RelativePosition[1] +
                                    partForExcavate.PartPositions[i].RelativePosition[1];
            nextRandomPosition[2] = randomSotaPlace.RelativePosition[2];

            for (int j = 0; j < _freeSotaInLayers.Count; j++)
            {
                if (_freeSotaInLayers[j].RelativePosition[0] == nextRandomPosition[0] &&
                    _freeSotaInLayers[j].RelativePosition[1] == nextRandomPosition[1])
                {
                    if (_freeSotaInLayers[j].sota.IsFree == false)
                    {
                        FindRandomPlaceForPart(part);
                        return;
                    }
                    else
                    {
                        randomSota.Add(_freeSotaInLayers[j]);
                    }
                }
            }

            if (randomSota.Count != i + 1)
            {
                FindRandomPlaceForPart(part);
                return;
            }
        }

        for (int i = 0; i < randomSota.Count; i++)
        {
            randomSota[i].sota.IsFree = false;
            _freeSotaInLayers.Remove(randomSota[i]);
            if (IsRandomPartPositionTest)
            {
                randomSota[i].sota.SetSotaDetect();
            }
        }

        MarkAroundPartPositionBusy(randomSota, part);
    }

    private void MarkAroundPartPositionBusy(List<SotaInLayer> randomSota, GameObject part)
    {
        for (int i = 0; i < randomSota.Count; i++)
        {
            for (int j = 0; j < AroundSotaPositions.Count; j++)
            {
                var nextAroundPosition = new int[3];
                nextAroundPosition[0] = randomSota[i].RelativePosition[0] + AroundSotaPositions[j].RelativePosition[0];
                nextAroundPosition[1] = randomSota[i].RelativePosition[1] + AroundSotaPositions[j].RelativePosition[1];
                nextAroundPosition[2] = randomSota[i].RelativePosition[2];

                for (int k = 0; k < sotaInLayers.Count; k++)
                {
                    if (sotaInLayers[k].RelativePosition[0] == nextAroundPosition[0] &&
                        sotaInLayers[k].RelativePosition[1] == nextAroundPosition[1])
                    {
                        if (sotaInLayers[k].sota.IsFree)
                        {
                            sotaInLayers[k].sota.IsFree = false;

                            _freeSotaInLayers.Remove(sotaInLayers[k]);
                            if (IsRandomPartPositionTest)
                            {
                                sotaInLayers[k].sota.SetSotaDetect();
                            }
                        }
                    }
                }
            }
        }

        SetPartPosition(randomSota, part);
    }

    private void SetPartPosition(List<SotaInLayer> randomSota, GameObject part)
    {
        var partForExcavate = part.GetComponent<PartForExcavate>();
        part.transform.position +=
            randomSota[0].sota.transform.position - partForExcavate.FirstSotaInPartPosition.position;

        part.transform.position = new Vector3(part.transform.position.x,
            part.transform.position.y - distanceBetweenLayers,
            part.transform.position.z);

        OnPartSetup.Invoke(randomSota, part);
    }

    IEnumerator TestSotaInLayer()
    {
        for (int j = 0; j < _busySotaInLayers.Count; j++)
        {
            _busySotaInLayers[j].sota.transform.GetComponent<MeshRenderer>().material = testBusySotaMaterial;
        }

        var i = 0;
        while (i < _freeSotaInLayers.Count)
        {
            yield return new WaitForSeconds(1f);
            _freeSotaInLayers[i].sota.transform.GetComponent<MeshRenderer>().material = testFreeSotaMaterial;

            i++;
        }
    }
}

[Serializable]
public class SotaInLayer
{
    public Sota sota;
    public int[] RelativePosition = new int[3];
}

[Serializable]
public class AroundSotaPosition
{
    public int[] RelativePosition = new int[2];
}