using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Reaction : IState
{
    public bool IsBadReaction { get; private set; }
    public bool IsGoodReaction { get; private set; }

    private float _timeToReaction;
    private float _time = 0f;

    private float _chanceToStayInMuseum;

    private Visitor _visitor;

    private NavMeshAgent _navMeshAgent;
    public Reaction(Visitor visitor,float chanceToGoInMuseum, NavMeshAgent navMeshAgent)
    {
        _chanceToStayInMuseum = chanceToGoInMuseum;
        _visitor = visitor;
        _navMeshAgent = navMeshAgent;
    }

    public void OnEnter()
    {
       // Debug.Log($"Visitor Reaction state - {_visitor.gameObject.name}. Stand name - {_visitor.TargetStand.gameObject.name}");
        _navMeshAgent.isStopped = true;
        _navMeshAgent.SetDestination(_visitor.transform.position);
        _visitor.transform.LookAt(_visitor.TargetStand.transform);

        _visitor.RemoveTargetStand();
        _timeToReaction = Random.Range(3f, 6f);

    }

    public void OnExit()
    {
        _time = 0;
        IsGoodReaction = false;
        IsBadReaction = false;
    }

    public void Tick()
    {
        _time += Time.deltaTime;
        if (_time >= _timeToReaction)
        {
            _time = 0;
            if (CheckIsVisitorStayInMuseum())
            {
                IsGoodReaction = true;
                _visitor.SpawnCoin();
            }
            else
            {
                IsBadReaction = true;
            }
        }
    }

    private bool CheckIsVisitorStayInMuseum()
    {
        var randomCnance = Random.Range(0f, 1f);
        return _chanceToStayInMuseum > randomCnance;
    }

}
