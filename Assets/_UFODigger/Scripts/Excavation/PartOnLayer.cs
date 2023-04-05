using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartOnLayer : MonoBehaviour
{
    [SerializeField] private Transform ScaleComponent;
    [SerializeField] private FoundPart _foundPart;

    [SerializeField] private FossilDetectorSO _fossilDetectorSO;
    [SerializeField] private UISkillsBag _skillsBag;

    private LayerOfSota _lastLayer;

    private PartForExcavate[] _partForExcavates;
    private int _partForSetupIndex;

    private List<PartInLayer> _partInLayers = new List<PartInLayer>();

    private List<LayerOfSota> _layersOfSota;
    public void ShowDetectSota(int yAccuracy = 3, int xAccuracy = 1)
    {
        if (_partForExcavates == null || _layersOfSota == null)
            return;

        var allPossibleDetectPosition = new List<SotaInLayer>();

        for (int i = 0; i < _partInLayers.Count; i++)
        {
            if (!_partInLayers[i].IsPartFind)
            {
                for (int j = -yAccuracy; j <= yAccuracy; j++)
                {
                    for (int k = -xAccuracy; k <= xAccuracy; k++)
                    {
                        var newDetectPositiveSota = new SotaInLayer();
                        newDetectPositiveSota.RelativePosition[0] = _partInLayers[i].SotasAbovePart[0].RelativePosition[0] + k;
                        newDetectPositiveSota.RelativePosition[1] = _partInLayers[i].SotasAbovePart[0].RelativePosition[1] + j;
                        newDetectPositiveSota.RelativePosition[2] = 0;

                        allPossibleDetectPosition.Add(newDetectPositiveSota);
                    }
                }
            }
        }

        var detectSotas = new List<Sota>();

        for (int i = 1; i < _layersOfSota.Count; i++) //ignore -1 fake layer layer
        {
            for (int j = 0; j < _layersOfSota[i].sotaInLayers.Count; j++)
            {
                for (int k = 0; k < allPossibleDetectPosition.Count; k++)
                {
                    if (allPossibleDetectPosition[k].RelativePosition[0] == _layersOfSota[i].sotaInLayers[j].RelativePosition[0] &&
                        allPossibleDetectPosition[k].RelativePosition[1] == _layersOfSota[i].sotaInLayers[j].RelativePosition[1])
                    {
                        detectSotas.Add(_layersOfSota[i].sotaInLayers[j].sota);
                    }
                }
            }
        }

        StartCoroutine(ShowDetectSota(detectSotas));
    }

    IEnumerator ShowDetectSota(List<Sota> sotas)
    {
        while (sotas.Count > 0)
        {
            var randomSotaToDetect = sotas[UnityEngine.Random.Range(0, sotas.Count)];
            randomSotaToDetect.SetSotaDetect();
            sotas.Remove(randomSotaToDetect);

            yield return new WaitForSeconds(0.05f);
        }

        _skillsBag.RemoveSkill(_fossilDetectorSO as ISkill);
    }

    public void SetupPartsOnLayer(PartForExcavate[] partForExcavates, List<LayerOfSota> layerOfSotas)
    {
        _partForExcavates = partForExcavates;
        _partForSetupIndex = 0;

        _lastLayer = layerOfSotas[layerOfSotas.Count - 1];
        _lastLayer.OnPartSetup.AddListener(OnPartSetup);
        _lastLayer.FindRandomPlaceForPart(partForExcavates[_partForSetupIndex].gameObject);

        _layersOfSota = layerOfSotas;
    }

    private void OnPartSetup(List<SotaInLayer> sotasAbove, GameObject part)
    {
        SetupPartInLayer(sotasAbove, part);

        _partForSetupIndex++;
        if (_partForSetupIndex < _partForExcavates.Length)
        {
            _lastLayer.FindRandomPlaceForPart(_partForExcavates[_partForSetupIndex].gameObject);
        }
    }

    private void SetupPartInLayer(List<SotaInLayer> sotasAbove, GameObject part)
    {
        _partInLayers.Add(new PartInLayer(part, sotasAbove,_partForExcavates[_partForSetupIndex]));
        for (int i = 0; i < sotasAbove.Count; i++)
        {
            sotasAbove[i].sota.OnSotaDestroy.AddListener(RemoveSotaFromAboveList);
        }
    }

    private void RemoveSotaFromAboveList(Sota sota)
    {
        for (int i = 0; i < _partInLayers.Count; i++)
        {
            for (int j = 0; j < _partInLayers[i].SotasAbovePart.Count; j++)
            {
                if (_partInLayers[i].SotasAbovePart[j].sota == sota)
                {
                    _partInLayers[i].SotasAbovePart.Remove(_partInLayers[i].SotasAbovePart[j]);
                }
            }

            if (_partInLayers[i].SotasAbovePart.Count < 1 && !_partInLayers[i].IsPartFind)
            {
                _partInLayers[i].IsPartFind = true;
                Debug.Log(_partInLayers[i].Part.name + " part found!");

                _foundPart.MoveFoundPartInStand(_partInLayers[i]);
            }
        }

    }
}

[Serializable]
public class PartInLayer
{
    public PartForExcavate ExcavatePart;
    public GameObject Part;
    public List<SotaInLayer> SotasAbovePart;
    public bool IsPartFind = false;

    public PartInLayer(GameObject part, List<SotaInLayer> sotasAbovePart,PartForExcavate partForExcavate)
    {
        Part = part;
        SotasAbovePart = sotasAbovePart;
        ExcavatePart = partForExcavate;
    }
}
