using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using EPOOutline;
using MoreMountains.NiceVibrations;

public class FoundPart : MonoBehaviour
{
    public CharacterAnimation BoyCharacterAnimation;
    public ExcavateComplete ExcavateComplete;
    public int MaxParts;
    public List<BoxesFoAlienPart> Boxes;
    public float MoveUpYposition;
    public float MoveUpTime;
    public float MoveInTime;
    public float MoveDownTime;

    [Range(1f, 2f)] public float Scalefactor1x1;

    [Range(1f, 2f)] public float Scalefactor1x2;

    [Range(1f, 2f)] public float Scalefactor1x3;

    public AlienForExcavateData AlienForExcavate;

    private int _partInBox = 0;
    private int _foundPart = 0;
    public event Action AllPartFound;
    private static readonly int Like = Animator.StringToHash("like");

    private Vector3 _partOffset = Vector3.zero;

    private void Start()
    {
        AlienForExcavate.FoundedParts.Clear();
        BoyCharacterAnimation.OnWinComplete += CompleteExcavation;
    }

    private void CompleteExcavation()
    {
        ExcavateComplete.CompleteExcavate();
    }

    private void OnDestroy()
    {
        BoyCharacterAnimation.OnWinComplete -= CompleteExcavation;
    }

    public void MoveFoundPartInStand(PartInLayer part)
    {
        _foundPart++;
        if (_foundPart == MaxParts)
        {
            AllPartFound?.Invoke();
        }

        if (UISettingsVibro._vibroStatus)
        {
            MMVibrationManager.Haptic(HapticTypes.HeavyImpact);
        }

        _partOffset = part.Part.GetComponent<PartForExcavate>().PartInPosition;
        var partObject = part.Part.GetComponent<PartForExcavate>().partObject;
        if (partObject.transform.parent.TryGetComponent<Outlinable>(out var outlinable))
        {
            outlinable.enabled = true;
          
        }
        else
        {
            Debug.LogWarning($"On part not set Outlinable component.");
        }

        SetAlienFounded(part);
        AlienForExcavate.FoundedParts.Add(part.ExcavatePart);
        AlienForExcavate.Save();

        StartCoroutine(MoveUp(partObject, part.ExcavatePart.PartPositions.Count));
    }

    IEnumerator MoveUp(GameObject alienPart, int partSize = 0)
    {
        var elapsedTime = 0f;
        var targetPosition = new Vector3(alienPart.transform.position.x,
            alienPart.transform.position.y + MoveUpYposition,
            alienPart.transform.position.z);

        var startPosition = alienPart.transform.position;
        while (MoveUpTime > elapsedTime)
        {
            alienPart.transform.position = Vector3.Lerp(startPosition, targetPosition, elapsedTime / MoveUpTime);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        for (int i = 0; i < Boxes.Count; i++)
        {
            if (Boxes[i].isFree)
            {
                Boxes[i].isFree = false;
                StartCoroutine(MoveIn(alienPart, Boxes[i], partSize));
                break;
            }
        }
    }

    private void SetAlienFounded(PartInLayer alienPart, int partSize = 0)
    {
        if (AlienForExcavate.AlienForExcavate != null)
        {
            AlienForExcavate.AlienForExcavate.AllAlienParts.SetPartFounded(alienPart.ExcavatePart);
        }
    }

    IEnumerator MoveIn(GameObject alienPart, BoxesFoAlienPart box, int partSize = 0)
    {
        var boxTransform = box.StandForFoundItem.gameObject.transform;
        var elapsedTime = 0f;
        var targetPosition = new Vector3(boxTransform.position.x,
            alienPart.transform.position.y,
            boxTransform.position.z);

        var startPosition = alienPart.transform.position;
        while (MoveDownTime > elapsedTime)
        {
            alienPart.transform.position = Vector3.Lerp(startPosition, targetPosition, elapsedTime / MoveDownTime);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        StartCoroutine(MoveDown(alienPart, box, partSize));
    }

    IEnumerator MoveDown(GameObject alienPart, BoxesFoAlienPart box, int partSize = 0)
    {
        if (alienPart.transform.parent.TryGetComponent<Outlinable>(out var outlinable))
        {
            outlinable.enabled = false;
        }
        else
        {
            Debug.LogWarning($"On part not set Outlinable component.");
        }

        box.CharacterAnimator.SetTrigger(Like);
        var boxTransform = box.StandForFoundItem.gameObject.transform;
        var elapsedTime = 0f;
        
        var targetPosition = new Vector3(boxTransform.position.x + _partOffset.x,
            boxTransform.position.y + _partOffset.y,
            boxTransform.position.z + _partOffset.z);

        var isNeedScale = false;
        var newScale = Vector3.one;
        var startScale = alienPart.transform.localScale;
        switch (partSize)
        {
            case 1:
                newScale = startScale * Scalefactor1x1;
                isNeedScale = true;
                break;
            case 2:
                newScale = startScale * Scalefactor1x2;
                isNeedScale = true;
                break;
            case 3:
                newScale = startScale * Scalefactor1x3;
                isNeedScale = true;
                break;
            default:
                newScale = startScale * Scalefactor1x3;
                isNeedScale = true;
                break;
        }

        if (isNeedScale)
        {
            targetPosition.y += 0.2f;
        }


        var startPosition = alienPart.transform.position;

        var startRotation = alienPart.transform.localEulerAngles;
        var finishRotation = startRotation;
        finishRotation.y += 90;

        while (MoveInTime > elapsedTime)
        {
            alienPart.transform.position = Vector3.Lerp(startPosition, targetPosition, elapsedTime / MoveInTime);
            alienPart.transform.localEulerAngles =
                Vector3.Lerp(startRotation, finishRotation, elapsedTime / MoveInTime);

            if (isNeedScale)
            {
                alienPart.transform.localScale = Vector3.Lerp(startScale, newScale, elapsedTime / MoveInTime);
            }

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        _partInBox++;
        box.BoxAnimation.Play();
        if (_partInBox == MaxParts)
        {
            StopAllCoroutines();
            for (int i = 0; i < Boxes.Count; i++)
            {
                Boxes[i].CharacterAnimator.SetTrigger("win");
            }

            var coins = FindObjectsOfType<Coin>();
            for (int i = 0; i < coins.Length; i++)
            {
                if (coins[i].gameObject.activeSelf)
                {
                    coins[i].CollectWorld();
                }
            }
        }
    }

    private void CompleteWinAnimation()
    {
        Debug.Log("1");
    }

    [Serializable]
    public class BoxesFoAlienPart
    {
        public GameObject StandForFoundItem;
        public bool isFree = true;
        public Animator CharacterAnimator;
        public Animation BoxAnimation;
        public int PartSize;
    }
}