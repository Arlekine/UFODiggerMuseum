using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIGoldMove : MonoBehaviour
{
    public float RandomOffset;
    public int GoldCount = 10;
    public float MoveTime = 1f;

    public GameObject GoldIcon;
    public Transform GoldContainer;
    public RectTransform StartPoint;
    public RectTransform FinishPoint;

    private List<GameObject> _golds = new List<GameObject>();
    private int _goldMove = 0;

    public void MoveGold()
    {
        _goldMove = 0;
        StartCoroutine(CreateIconGold());
    }


    private IEnumerator CreateIconGold()
    {
        var gold = 0;

        while (gold < GoldCount)
        {
            var randomInstantiatePosition = new Vector3(StartPoint.position.x + Random.Range(-RandomOffset, RandomOffset),
                StartPoint.position.y + Random.Range(-RandomOffset, RandomOffset), StartPoint.position.z + Random.Range(-RandomOffset, RandomOffset));
            var goldIcon = Instantiate(GoldIcon, randomInstantiatePosition, Quaternion.identity, GoldContainer);
            _golds.Add(goldIcon);
            gold++;
            yield return new WaitForSeconds(0.1f);
        }

        StartCoroutine(MoveIconGold());
    }


    private IEnumerator MoveIconGold()
    {
       
        foreach (var gold in _golds)
        {
            var elapsedTime = 0f;
            var startPosition = gold.transform.position;
            while (elapsedTime < MoveTime)
            {
                gold.transform.position = Vector3.Lerp(startPosition, FinishPoint.position, elapsedTime / MoveTime);
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            Destroy(gold.gameObject);
        }
    }
}