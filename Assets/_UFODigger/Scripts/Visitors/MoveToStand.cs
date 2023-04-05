using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MoveToStand : IState
{
    private Visitor _visitor;
    private NavMeshAgent _navMeshAgent;

    private Animator _animator;

    public MoveToStand(Visitor visitor, NavMeshAgent navMeshAgent)
    {
        _visitor = visitor;
        _navMeshAgent = navMeshAgent;
    }

    public void OnEnter()
    {
        //Debug.Log($"Visitor MoveToStand state - {_visitor.gameObject.name}");
        _navMeshAgent.isStopped = false;
        _visitor.MoveStandPoint = _visitor.TargetStand.GetViewPoint();
        _navMeshAgent.SetDestination(_visitor.MoveStandPoint);
        
    }

  
    public void OnExit()
    {
    }

    public void Tick()
    {
       
    }

}
