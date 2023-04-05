using UnityEngine;
using Utils;

[CreateAssetMenu(fileName = "AlienMapData", menuName = "ScriptableObjects/AlienMapData", order = 1)]
public class AlienForMapData : ScriptableObject
{
    public string StandSaveAlienKey;
    public bool IsJustLook;

    private string _lookKey = "mapL";
    private string _standKey = "mapS";
    public void Save()
    {
        SaveLoadSystem.Save(_lookKey, IsJustLook);
        SaveLoadSystem.Save(_standKey, StandSaveAlienKey);
    }
    public void Load()
    {
        IsJustLook = SaveLoadSystem.CheckKey(_lookKey) ? SaveLoadSystem.LoadBool(_lookKey) : IsJustLook;
        if (SaveLoadSystem.CheckKey(_standKey))
        {
            StandSaveAlienKey = SaveLoadSystem.LoadString(_standKey);
        }
    }
}
