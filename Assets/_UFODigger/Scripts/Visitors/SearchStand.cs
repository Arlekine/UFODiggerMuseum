using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class SearchStand : IState
{
    private Visitor _visitor;
    private Stand[] _stands = new Stand[4];

    private Animator _animator;
    public SearchStand(Visitor visitor, Stand[] stands) {

        _visitor = visitor;
        _stands = stands;
    }

    public void OnEnter()
    {
        //Debug.Log($"Visitor SearchStand state - {_visitor.gameObject.name}");
       
        _visitor.VisitorInMuseum();
        var visibleStand = from stand in _stands where stand.IsStandBuild == true select stand;
        var enumerable = visibleStand as Stand[] ?? visibleStand.ToArray();
        var randomStand = enumerable[Random.Range(0, enumerable.Count())];
        _visitor.SetTargetStand(randomStand);
    }

    public void OnExit()
    {
    }

    public void Tick()
    {
        
    }

  
}
