using UnityEngine;
using Utils;

public class MapManager : MonoBehaviour
{
    public AlienForMapData AlienForMapData;
    public AlienForExcavateData AlienForExcavateData;

    public IconOnMap[] IconsOnMap;

    public UIExcavationPrice UIExcavationPrice;

    private void Start()
    {
        AlienForMapData.Load();
        if (!AlienForMapData.IsJustLook)
        {
            UIExcavationPrice.OnExcavationStart.AddListener(SaveAlienData);
        }
    }

    private void SaveAlienData(Alien alien)
    {
        AlienForExcavateData.AlienForExcavate = alien;
        SaveLoadSystem.Save(AlienForMapData.StandSaveAlienKey, alien.name);
        AlienForExcavateData.Save();
    }
}