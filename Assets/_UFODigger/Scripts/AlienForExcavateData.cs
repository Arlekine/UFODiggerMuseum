using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Utils;

[CreateAssetMenu(fileName = "AlienData", menuName = "ScriptableObjects/AlienData", order = 1)]
public class AlienForExcavateData : ScriptableObject
{
    public Aliens Aliens;
    public Alien AlienForExcavate;
    public bool IsAlienExcavate;
    public List<PartForExcavate> FoundedParts = new List<PartForExcavate>();

    private const string _alienKey = "exAl";
    private const string _excavKey = "ex?";
    public void Save()
    {
        AlienData.FoundedParts = FoundedParts;
        
        SaveLoadSystem.Save(_excavKey,IsAlienExcavate);
        
        if (AlienForExcavate != null)
        {
            SaveLoadSystem.Save(_alienKey, AlienForExcavate.name);
            
        }
    }

    public void Load()
    {
        if (SaveLoadSystem.CheckKey(_alienKey))
        {
            var alienLoad = from alien in Aliens.AllAliens
                where alien.name == SaveLoadSystem.LoadString(_alienKey)
                select alien;

            var aliens = alienLoad.ToList();
            AlienForExcavate = aliens.FirstOrDefault();

            if (AlienForExcavate != null)
                AlienForExcavate.LoadAlienData();

            if (aliens.Count() > 1)
            {
                Debug.LogWarning("in Aliens two alien with equals names");
            }
            
        }

        
        if (SaveLoadSystem.CheckKey(_excavKey))
        {
            IsAlienExcavate = SaveLoadSystem.LoadBool(_excavKey);
            
        }

        FoundedParts = AlienData.FoundedParts;
    }
}

public static class AlienData
{
    public static List<PartForExcavate> FoundedParts;
}