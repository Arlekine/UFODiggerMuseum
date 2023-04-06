using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class RobotCleaner : MonoBehaviour
{
    public InstrumentData RobotData;
    public GameObject Robot;
    public CapsuleCollider Collider;
    public Stand TargetStand { get; private set; }
    public StateMachine StateMachine { get; private set; }
    public Vector3 MoveStandPoint { get; set; } = Vector3.zero;

    private Stand[] _stands;
    private NavMeshAgent _navMeshAgent;

    private bool _IsRobotStart;

    private void Awake()
    {
        RobotData.LoadData();
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _stands = FindObjectsOfType<Stand>();
        Collider.enabled = false;
    }

    private void Start()
    {
        /*if (RobotData.IsInstrumentUnlock && !_IsRobotStart)
        {
            _IsRobotStart = true;
            SetupStates();
            Robot.SetActive(true);
            Collider.enabled = true;

            _navMeshAgent.speed = RobotData.Upgrades[RobotData.LevelOfUpgrade].Power[0];
        }

        RobotData.OnLevelRise.AddListener(OnRobotUpgrade);*/
    }

    private void OnRobotUpgrade()
    {
        /*if (RobotData.IsInstrumentUnlock && !_IsRobotStart)
        {
            _IsRobotStart = true;
            SetupStates();
            Robot.SetActive(true);
            Collider.enabled = true;
        }

        _navMeshAgent.speed = (RobotData.Upgrades[RobotData.LevelOfUpgrade].Power[0]); //power = speed*/
    }

    [EditorButton]
    public void StartRobot()
    {
        _IsRobotStart = true;
        SetupStates();
        Robot.SetActive(true);
        Collider.enabled = true;

        _navMeshAgent.speed = (RobotData.Upgrades[RobotData.LevelOfUpgrade].Power[0]);
    }

    private void SetupStates()
    {
        StateMachine = new StateMachine();

        var searchStand = new RobotSearch(this, _stands);
        var moveToStand = new RobotMove(this, _navMeshAgent);

        Func<bool> IsRobotHasTarget() => () => TargetStand != null;
        Func<bool> IsVisitorReachStand() => () => Vector3.Distance(transform.position, MoveStandPoint) < 1.5f;


        StateMachine.AddTransition(searchStand, moveToStand, IsRobotHasTarget());
        StateMachine.AddTransition(moveToStand, searchStand, IsVisitorReachStand());

        StateMachine.SetState(searchStand);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.TryGetComponent<Coin>(out var coin))
        {
            coin.Collect();
        }
    }

    private void Update()
    {
        if (StateMachine != null)
        {
            StateMachine.Tick();
        }
    }

    [EditorButton]
    public void SetTargetStand(Stand randomStand)
    {
        TargetStand = randomStand;
    }

    public void RemoveTarget()
    {
        TargetStand = null;
    }
}