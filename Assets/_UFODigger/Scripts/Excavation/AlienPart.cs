using UnityEngine;
using System;
using System.Collections.Generic;
using Utils;
using Random = UnityEngine.Random;

public class AlienPart : MonoBehaviour
{
    [SerializeField] private Part[] _parts;
    [SerializeField] private Alien _alien;

    [SerializeField] private bool _needOrderedParts;
    public void Awake()
    {
        LoadPartsStatus();
    }

    public void LoadPartsStatus()
    {
        foreach (var part in _parts)
        {
            if (SaveLoadSystem.CheckKey($"{part.OriginalPart.name}{_alien.name}"))
            {
                part.IsOpen = SaveLoadSystem.LoadBool($"{part.OriginalPart.name}{_alien.name}");
            }
            else
            {
                part.IsOpen = false;
            }
        }
    }

    public void SavePartsStatus()
    {
        foreach (var part in _parts)
        {
            SaveLoadSystem.Save($"{part.OriginalPart.name}{_alien.name}", part.IsOpen);
        }
    }

    public Alien GetAlien()
    {
        return _alien;
    }
    
    public Part[] GetParts()
    {
        return _parts;
    }

    public void ShowOpenParts()
    {
        for (int i = 0; i < _parts.Length; i++)
        {
            if (!_parts[i].IsOpen)
            {
                _parts[i].OriginalPart.SetActive(false);
            }
        }
    }

    public int GetPartsCount()
    {
        return _parts.Length;
    }
    public int GetOpenPartsCount()
    {
        var openParts = 0;
        for (int i = 0; i < _parts.Length; i++)
        {
            if (_parts[i].IsOpen)
            {
                openParts++;
            }
        }
        return openParts;
    }

    public void HideExcavationParts()
    {
        for (int i = 0; i < _parts.Length; i++)
        {
            _parts[i].ExcavationPart.gameObject.SetActive(false);
        }
    }

    public PartForExcavate[] GetRandomAlienPart()
    {
        var partsForExcavate = new PartForExcavate[2];
        var notOpenParts = new List<PartForExcavate>();
        var openParts = new List<PartForExcavate>();

        for (int i = 0; i < _parts.Length; i++)
        {
            if (!_parts[i].IsOpen)
            {
                notOpenParts.Add(_parts[i].ExcavationPart);
            }
            else
            {
                openParts.Add(_parts[i].ExcavationPart);
            }
        }

        
        if (_needOrderedParts)
        {
            if (notOpenParts.Count > 1)
            {
                partsForExcavate[0] = notOpenParts[0];
                partsForExcavate[1] = notOpenParts[1];
            }else if (notOpenParts.Count == 1)
            {
                partsForExcavate[0] = notOpenParts[0];
                partsForExcavate[1] = openParts[Random.Range(0, openParts.Count)];
            }else if (notOpenParts.Count == 0)
            {
                partsForExcavate[0] = openParts[Random.Range(0, openParts.Count)];
                partsForExcavate[1] = openParts[Random.Range(0, openParts.Count)];
            }

            return partsForExcavate;
        }
        
        
        if (notOpenParts.Count > 0)
        {
            partsForExcavate[0] = notOpenParts[Random.Range(0, notOpenParts.Count)];
            partsForExcavate[0].IsNewPart = true;
            notOpenParts.Remove(partsForExcavate[0]);
        }
        else
        {
            partsForExcavate[0] = openParts[Random.Range(0, openParts.Count)];
            partsForExcavate[0].IsNewPart = false;
            openParts.Remove(partsForExcavate[0]);
        }

        var randomDuplicate = Random.Range(0f, 1f);

        if (randomDuplicate <=  _alien.DuplicateChance)
        {
            if (openParts.Count > 0)
            {
                partsForExcavate[1] = openParts[Random.Range(0, openParts.Count)];
                partsForExcavate[1].IsNewPart = false;
                openParts.Remove(partsForExcavate[1]);
            }
            else
            {
                partsForExcavate[1] = notOpenParts[Random.Range(0, notOpenParts.Count)];
                partsForExcavate[1].IsNewPart = true;
                notOpenParts.Remove(partsForExcavate[1]);
            }
        }
        else
        {
            if (notOpenParts.Count > 0)
            {
                partsForExcavate[1] = notOpenParts[Random.Range(0, notOpenParts.Count)];
                partsForExcavate[1].IsNewPart = true;
                notOpenParts.Remove(partsForExcavate[1]);
            }
            else
            {
                partsForExcavate[1] = openParts[Random.Range(0, openParts.Count)];
                partsForExcavate[1].IsNewPart = false;
                openParts.Remove(partsForExcavate[1]);
            }
        }

        return partsForExcavate;
    }

    public void SetPartFounded(PartForExcavate part)
    {
        var alienOpen = true;
        for (int i = 0; i < _parts.Length; i++)
        {
            if (_parts[i].ExcavationPart == part)
            {
                _parts[i].IsOpen = true;
            }
            if (!_parts[i].IsOpen)
            {
                alienOpen = false;
            }
        }

        _alien.IsAlienOpen = alienOpen;
        _alien.SaveAlienData();
        SavePartsStatus();
    }
}

[Serializable]
public class Part
{
    public GameObject OriginalPart;
    public PartForExcavate ExcavationPart;

    public int ExcavationOrder;

    public bool IsOpen;
}