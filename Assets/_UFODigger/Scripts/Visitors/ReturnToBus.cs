using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ReturnToBus : IState
{
    private Visitor _visitor;

    private NavMeshAgent _navMeshAgent;

    private Transform _targetPoint;

    private bool _visitorStartReturn;
    public ReturnToBus(Visitor visitor, NavMeshAgent navMeshAgent, Transform target)
    {
        _visitor = visitor;
        _navMeshAgent = navMeshAgent;
        _targetPoint = target;
    }

    public void OnEnter()
    {
        _navMeshAgent.isStopped = false;
        _navMeshAgent.SetDestination(_targetPoint.position);
        _visitorStartReturn = true;
        _visitor.IsReturnToBus = true;
    }

    public void OnExit()
    {

    }

    public void Tick()
    {
        
    }
}
