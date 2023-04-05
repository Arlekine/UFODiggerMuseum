using UnityEngine;
using UnityEngine.AI;

public class RobotMove :  IState
{
    private RobotCleaner _robot;
    private NavMeshAgent _navMeshAgent;

    private Animator _animator;

    public RobotMove(RobotCleaner robot, NavMeshAgent navMeshAgent)
    {
        _robot = robot;
        _navMeshAgent = navMeshAgent;
    }

    public void Tick()
    {

    }

    public void OnEnter()
    {
        _robot.MoveStandPoint = _robot.TargetStand.GetViewPoint();
        _navMeshAgent.SetDestination(_robot.MoveStandPoint);
        _robot.RemoveTarget();
    }

    public void OnExit()
    {
    
    }
}
