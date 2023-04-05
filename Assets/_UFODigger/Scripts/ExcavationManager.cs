using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExcavationManager : MonoBehaviour
{
    public FoundPart FoundPart;
    public Transform CameraTransform;
    public Transform BottomGroup;
    public Transform UpperGroup;
    
    public List<ExcavationLayerSettings> ExcavationLayers;
    
    public void SetExcavationSceneSettings(LayerOfSota layerOfSota)
    {
        foreach (var sExcavationLayer in ExcavationLayers)
        {
            if (sExcavationLayer.LayerOfSota.gameObject.name == layerOfSota.gameObject.name)
            {
                CameraTransform.localPosition = sExcavationLayer.CameraPosition;
                BottomGroup.localPosition = sExcavationLayer.BottomGroupPosition;
                BottomGroup.localScale = sExcavationLayer.BottomGroupScale;
                UpperGroup.localPosition = sExcavationLayer.UpperGroupPosition;
                UpperGroup.localScale = sExcavationLayer.UpperGroupScale;

                FoundPart.Scalefactor1x1 = sExcavationLayer.PartScaleFactor1x1;
                FoundPart.Scalefactor1x2 = sExcavationLayer.PartScaleFactor1x2;
                FoundPart.Scalefactor1x3 = sExcavationLayer.PartScaleFactor1x3;
                FoundPart.MoveUpYposition = sExcavationLayer.PartYHeight;
            }
        }
    }

    [Serializable]
    public class ExcavationLayerSettings
    {
        public LayerOfSota LayerOfSota;
        public Vector3 CameraPosition;
        public Vector3 BottomGroupPosition;
        public Vector3 BottomGroupScale;
        public Vector3 UpperGroupPosition;
        public Vector3 UpperGroupScale;

        public float PartYHeight;
        public float PartScaleFactor1x1;
        public float PartScaleFactor1x2;
        public float PartScaleFactor1x3;
    }
}
