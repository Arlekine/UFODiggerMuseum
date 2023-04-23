using UnityEngine;
using Utils;

[CreateAssetMenu(fileName = "Alien", menuName = "ScriptableObjects/Alien", order = 1)]
public class Alien : ScriptableObject
{
    public string Name;

    [Header("Sprite for wiki 500x600 pixels.")]
    public Sprite Shot;
    public Sprite RarityIcon;

    [Header("Alien discription")]
    public string Discriprion;

    public AlienSize Size;

    public GameObject AlienPrefab;

    public int ExcavationPrice;
    public int SellPrice;
    public AlienPart AllAlienParts;

    public LayerOfSota ExcavateLayerType;
    public int LayersCount;
    
    [Range(0f,1f)]
    public float DuplicateChance;
    
    [HideInInspector]
    public bool IsAlienOpen;
    public bool IsAlienFirstTimeOpen;
    public int Excavations;

    public void LoadAlienData()
    {
        if (SaveLoadSystem.CheckKey($"{Name}"))
        {
            IsAlienOpen = SaveLoadSystem.LoadBool($"{Name}");
        }
        else
        {
            IsAlienOpen = false;
        }
        
        if (SaveLoadSystem.CheckKey($"{Name}+F"))
        {
            IsAlienFirstTimeOpen = SaveLoadSystem.LoadBool($"{Name}+F");
        }
        else
        {
            IsAlienFirstTimeOpen = false;
        }

        if (SaveLoadSystem.CheckKey($"{Name}+Ex"))
        {
            Excavations = SaveLoadSystem.LoadInt($"{Name}+Ex");
        }
        else
        {
            Excavations = 0;
        }
    }
    
    public void SaveAlienData()
    {
        SaveLoadSystem.Save($"{Name}",IsAlienOpen);
        SaveLoadSystem.Save($"{Name}+F",IsAlienFirstTimeOpen);
        SaveLoadSystem.Save($"{Name}+Ex",Excavations);
    }
}

public enum AlienSize
{
    small,
    midle,
    big
}