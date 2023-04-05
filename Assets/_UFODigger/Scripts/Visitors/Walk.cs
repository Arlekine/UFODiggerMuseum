using UnityEngine;
using UnityEngine.AI;

public class Walk : IState
{

    private Visitor _visitor;

    private NavMeshAgent _navMeshAgent;

    private Transform _leftZone;
    private Transform _rightZone;

    private float _chanceToGoInMuseum;

    private Transform _targetPoint;

    private float _timeToNewCheck = 15f;
    private float _time = 0f;

    public Walk(Visitor visitor, NavMeshAgent navMeshAgent, Transform leftWalkPoint, Transform rightWalkPoint, float chanceToGoInMuseum)
    {
        _visitor = visitor;
        _navMeshAgent = navMeshAgent;
        _leftZone = leftWalkPoint;
        _rightZone = rightWalkPoint;
        _chanceToGoInMuseum = chanceToGoInMuseum;
    }

    public void OnEnter()
    {
        // Debug.Log($"Visitor walk state - {_visitor.gameObject.name}");

        _navMeshAgent.isStopped = false;
        CheckIsVisitorGoToMuseum();

        if (!_visitor.IsVisitorGoToMuseum)
        {
            var randomPoint = Random.Range(0, 2);

            if (randomPoint == 1)
            {
                SetDestination(_leftZone);
            }
            else
            {
                SetDestination(_rightZone);
            }
        }

    }

    private void SetDestination(Transform zone)
    {
        _navMeshAgent.SetDestination(zone.GetComponent<PointOnPlain>().GetRandomPoint());
        _targetPoint = zone;
    }

    public void OnExit()
    {
        _time = 0f;
    }

    public void Tick()
    {
        CheckDistance();
        WaitForNewGoToMuseumCheck();
    }

    private void WaitForNewGoToMuseumCheck()
    {
        _time += Time.deltaTime;
        if (_time >= _timeToNewCheck)
        {
            _time = 0f;
            CheckIsVisitorGoToMuseum();
        }
    }

    private void CheckDistance()
    {
        if (!_visitor.IsVisitorGoToMuseum)
        {
            if (!_navMeshAgent.hasPath)
            {
                if (_targetPoint == _leftZone)
                {
                    SetDestination(_rightZone);
                }
                else
                {
                    SetDestination(_leftZone);
                }
            }
        }
    }

    private void CheckIsVisitorGoToMuseum()
    {
        var randomCnance = Random.Range(0f, 1f);

        if (_chanceToGoInMuseum > randomCnance)
        {
            _visitor.GoToMuseum();
        }
    }
}
