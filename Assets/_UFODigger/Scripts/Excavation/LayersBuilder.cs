using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayersBuilder : MonoBehaviour
{
    public ExcavationManager ExcavationManager;
    
    public Transform ScaleComponent;
    public AlienForExcavateData AlienForExcavate;
    public GameObject UnderLayerImage;
    public float DistanceBetweenLayers;

    private PartOnLayer _partOnLayer;
    private List<LayerOfSota> _layersOfSota = new List<LayerOfSota>();
    
    private PartForExcavate[] _partsForExcavate;
    
    [Header("Test layer build")] public bool IsTest;
    public int TestLayersCount;
    public LayerOfSota TestLayerOfSota;
    public PartForExcavate[] TestPartsForExcavate;

    private void Start()
    {
        _partOnLayer = GetComponent<PartOnLayer>();

        if (IsTest)
        {
            TestBuild();
        }
        else
        {
            if (AlienForExcavate.AlienForExcavate != null)
            {
                AlienForExcavate.Load();
                Build();
            }
            else
            {
                Debug.LogWarning("Test Build!");
                TestBuild();
            }
        }

    
    }

    private void Build()
    {
        ExcavationManager.SetExcavationSceneSettings(AlienForExcavate.AlienForExcavate.ExcavateLayerType);
        SetupLayers(AlienForExcavate.AlienForExcavate.LayersCount,AlienForExcavate.AlienForExcavate.ExcavateLayerType);
        _partsForExcavate = AlienForExcavate.AlienForExcavate.AllAlienParts.GetRandomAlienPart();
        _partOnLayer.SetupPartsOnLayer(_partsForExcavate, _layersOfSota);
    }

    private void TestBuild()
    {
        ExcavationManager.SetExcavationSceneSettings(TestLayerOfSota);
        SetupLayers(TestLayersCount,TestLayerOfSota);

        _partOnLayer.SetupPartsOnLayer(TestPartsForExcavate, _layersOfSota);
    }

    private void SetupLayers(int layersCount,LayerOfSota layersOfSota)
    {
        //fake -1 layer build
        var fakeLayer = Instantiate(layersOfSota.gameObject,ScaleComponent);
        var layerOfSota = fakeLayer.GetComponent<LayerOfSota>();
        layerOfSota.ScaleComponent = ScaleComponent;
        layerOfSota.Setup(-1);

        fakeLayer.transform.position = new Vector3(fakeLayer.transform.position.x,
            fakeLayer.transform.position.y + DistanceBetweenLayers,
            fakeLayer.transform.position.z);
        _layersOfSota.Add(layerOfSota);

        for (int i = 0; i < layersCount; i++)
        {
            var layer = Instantiate(layersOfSota.gameObject,ScaleComponent);
            layerOfSota = layer.GetComponent<LayerOfSota>();
            layerOfSota.ScaleComponent = ScaleComponent;
            layerOfSota.Setup(i + 1);

            layer.transform.position = new Vector3(layer.transform.position.x,
                layer.transform.position.y - DistanceBetweenLayers * i,
                layer.transform.position.z);

            _layersOfSota.Add(layerOfSota);
        }
    }
}