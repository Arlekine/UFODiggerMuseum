using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(MovePart))]
public class GalaryScene : MonoBehaviour
{
    public float TimeToCompleteRotate = 2f;
    public GameObject CollectVFX;
    
    public GameObject NothingPopup;
    public Animator[] CharacterAnimators;
    
    public AlienForExcavateData AlienData;
    public UIExcavationPrice ExcavationPrice;

    public AlienPart[] Aliens;

    public RotateObjectController RotateController;

    private MovePart _movePart;

    public UIAlienProgress UIAlienProgress;

    public UICollectAlien UICollectAlien;
    
    private List<Part> _partForExcavates = new List<Part>();

    private AlienPart _alienPart;
    private static readonly int Lose = Animator.StringToHash("lose");
    private static readonly int Win = Animator.StringToHash("win");

    private void Awake()
    {
        NothingPopup.SetActive(false);
        AlienData.Load();
    }

    private void Start()
    {
        _movePart = GetComponent<MovePart>();
        _movePart.OnPartPlaced.AddListener(MoveNextPart);

        foreach (var alien in Aliens)
        {
            if (AlienData.AlienForExcavate.Name == alien.GetAlien().Name)
            {
                alien.LoadPartsStatus();
                alien.gameObject.SetActive(true);
                alien.HideExcavationParts();
                alien.ShowOpenParts();

                _alienPart = alien;
            }
            else
            {
                alien.gameObject.SetActive(false);
            }
        }
        UIAlienProgress.UpdateValue(_alienPart.GetOpenPartsCount(),_alienPart.GetPartsCount());


        if (AlienData.IsAlienExcavate)
        {
            ExcavationPrice.gameObject.SetActive(true);
            ExcavationPrice.Setup(AlienData.AlienForExcavate);
            
            RotateController.enabled = true;
        }
        else
        {
            RotateController.enabled = false;
            ExcavationPrice.gameObject.SetActive(false);

            if (AlienData.FoundedParts.Count > 0)
            {
                // _partForExcavates = AlienData.FoundedParts;
                var parts = _alienPart.GetParts();

                for (var i = 0; i < AlienData.FoundedParts.Count; i++)
                {
                    for (var j = 0; j < parts.Length; j++)
                    {
                        if (parts[j].ExcavationPart.partObject.name == AlienData.FoundedParts[i].partObject.name)
                        {
                            _partForExcavates.Add(parts[j]);
                        }
                    }
                }

                for (var i = 0; i < _partForExcavates.Count; i++)
                {
                    if (AlienData.FoundedParts[i].IsNewPart)
                    {
                        _partForExcavates[i].OriginalPart.GetComponent<MeshRenderer>().enabled =false;
                        var childeMeshs = _partForExcavates[i].OriginalPart.GetComponentsInChildren<MeshRenderer>();
                        if (childeMeshs != null && childeMeshs.Length > 0)
                        {
                            foreach (var mesh in childeMeshs)
                            {
                                mesh.enabled = false;
                            }
                        }
                    }
                    else
                    {
                        _partForExcavates[i].OriginalPart.SetActive(true);
                    }
                }

                _movePart.PlacePart(_partForExcavates[0].ExcavationPart.partObject.transform,
                    _partForExcavates[0].OriginalPart.transform);

                _partForExcavates.Remove(_partForExcavates[0]);
                
                for (int i = 0; i < CharacterAnimators.Length; i++)
                {
                    CharacterAnimators[i].SetTrigger(Win);
                }
            }
            else
            {
                NothingPopup.SetActive(true);
                RotateController.enabled = true;
                ExcavationPrice.gameObject.SetActive(true);
                ExcavationPrice.Setup(AlienData.AlienForExcavate);
                for (int i = 0; i < CharacterAnimators.Length; i++)
                {
                    CharacterAnimators[i].SetTrigger(Lose);
                }
            }
        }
        if (_alienPart.GetAlien().IsAlienOpen && !_alienPart.GetAlien().IsAlienFirstTimeOpen)
        {
            ExcavationPrice.gameObject.SetActive(false);
            CollectVFX.SetActive(true);
        }
    }

    private void OnRotateComplete()
    {
        StartCoroutine(CollectAlienRotate());
    }

    IEnumerator CollectAlienRotate()
    {
        yield return new WaitForSeconds(TimeToCompleteRotate);
        SceneManager.LoadScene(1);
    }

    private void MoveNextPart()
    {
        if (_partForExcavates.Count > 0)
        {
            _movePart.PlacePart(_partForExcavates[0].ExcavationPart.partObject.transform,
                _partForExcavates[0].OriginalPart.transform);
            
            _partForExcavates.Remove(_partForExcavates[0]);
        }
        else
        {
            if (_alienPart.GetAlien().IsAlienOpen && !_alienPart.GetAlien().IsAlienFirstTimeOpen)
            {
                RotateController.GetComponent<Animation>().Play();
                _alienPart.GetAlien().IsAlienFirstTimeOpen = true;
                _alienPart.GetAlien().SaveAlienData();
            
                ExcavationPrice.gameObject.SetActive(false);
                
                RotateController.GetComponent<Animation>().Play();
                RotateController.enabled = false;
                RotateController.GetComponent<AlienRotateAnimation>().OnRotateComplete += OnRotateComplete;

                SoundManager.Instance.PlayAlienComplete();
                Analitics.Instance.SendAlienEvent(_alienPart.GetAlien().Name, _alienPart.GetAlien().Excavations);
                UICollectAlien.Init(_alienPart.GetAlien());
            }
            else
            {
                ExcavationPrice.gameObject.SetActive(true);
                ExcavationPrice.Setup(AlienData.AlienForExcavate);
                RotateController.enabled = true;
            }
            Debug.Log("Complete placing");
        }
    }
}