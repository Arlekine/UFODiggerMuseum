using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(VisitorModel))]
public class Visitor : MonoBehaviour
{
    [Header("Visitor from bus gift Setting")]
    public bool IsFromBus;
    public float InMuseumTime = 10f;
    public Transform BusPoint;
    public bool IsReturnToBus = false;
    [Space]
    [Header("Coin Settings")]

    public GameObject Coin;
    public Transform CoinSpawnPoint;

    public Vector3 MoveStandPoint { get; set; }
    public bool IsVisitorGoToMuseum { get; private set; }
    public Stand TargetStand { get; private set; }

    [Range(0f, 1f)]
    [SerializeField] private float _chanceToGoInMuseum;

    private Transform _leftWalkZone;
    private Transform _rightWalkZone;

    public StateMachine StateMachine { get; private set; }
    private Stand[] _stands;

    private NavMeshAgent _navMeshAgent;

    private VisitorModel _visitorModel;
    private Animator _animator;

    private float _timeInMuseum = 0f;


    private void Awake()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _stands = FindObjectsOfType<Stand>();

        _navMeshAgent.speed = UnityEngine.Random.Range(2f, 3.5f);

        var walkZones = FindObjectOfType<WalkZone>();
        _leftWalkZone = walkZones.LeftZone;
        _rightWalkZone = walkZones.RightZone;

        _visitorModel = GetComponent<VisitorModel>();
        _visitorModel.OnModelInstantiate.AddListener(OnModelInstantiate);
    }

    private void OnModelInstantiate(GameObject visitor)
    {
        SetupStates(visitor);
    }

    private void SetupStates(GameObject visitor)
    {
        StateMachine = new StateMachine();

        _animator = visitor.GetComponent<Animator>();

        var walk = new Walk(this, _navMeshAgent, _leftWalkZone, _rightWalkZone, _chanceToGoInMuseum);
        var searchStand = new SearchStand(this, _stands);
        var moveToStand = new MoveToStand(this, _navMeshAgent);
        var reaction = new Reaction(this, _chanceToGoInMuseum, _navMeshAgent);
        var returnToBus = new ReturnToBus(this, _navMeshAgent, BusPoint);

        /*
        _stateMachine.AddTransition(reaction, searchStand, IsVisitorHasnotTarget()); //TODO: if stands can remove from museums add this
        */

        Func<bool> IsGoToMuseum() => () => IsVisitorGoToMuseum;
        Func<bool> IsVisitorHasTarget() => () => TargetStand != null;
        Func<bool> IsVisitorHasNoTarget() => () => TargetStand == null;
        Func<bool> IsVisitorReachStand() => () => Vector3.Distance(transform.position, MoveStandPoint) < 1.5f;
        Func<bool> IsGoodReaction() => () => reaction.IsGoodReaction;
        Func<bool> IsBadReaction() => () => reaction.IsBadReaction;

        Func<bool> IsVisitorHasTimeInMuseum() => () => _timeInMuseum < InMuseumTime && (reaction.IsGoodReaction || reaction.IsBadReaction);
        Func<bool> IsVisitorHasNoTimeInMuseum() => () => _timeInMuseum >= InMuseumTime && (reaction.IsGoodReaction || reaction.IsBadReaction);

        if (!IsFromBus)
        {
            StateMachine.AddTransition(walk, searchStand, IsGoToMuseum());
            StateMachine.AddTransition(searchStand, moveToStand, IsVisitorHasTarget());
            StateMachine.AddTransition(moveToStand, searchStand, IsVisitorHasNoTarget());

            StateMachine.AddTransition(moveToStand, reaction, IsVisitorReachStand());

            StateMachine.AddTransition(reaction, searchStand, IsGoodReaction());
            StateMachine.AddTransition(reaction, walk, IsBadReaction());

            StateMachine.SetState(walk);
        }
        else
        {
            StateMachine.AddTransition(searchStand, moveToStand, IsVisitorHasTarget());
            StateMachine.AddTransition(moveToStand, reaction, IsVisitorReachStand());
            StateMachine.AddTransition(reaction, searchStand, IsVisitorHasTimeInMuseum());
            StateMachine.AddTransition(reaction, returnToBus, IsVisitorHasNoTimeInMuseum());

            StateMachine.SetState(searchStand);
            StartCoroutine(InMuseumTimer());
        }
    }

    IEnumerator InMuseumTimer()
    {
        while (_timeInMuseum <= InMuseumTime)
        {
            _timeInMuseum++;
            yield return new WaitForSeconds(1f);
        }

    }
    public void GoToMuseum()
    {
        IsVisitorGoToMuseum = true;
    }

    public void VisitorInMuseum()
    {
        IsVisitorGoToMuseum = false;
    }

    public void SetTargetStand(Stand stand)
    {
        TargetStand = stand;
    }
    public void RemoveTargetStand()
    {
        TargetStand = null;
    }

    public void SpawnCoin()
    {
        var coinObject = Instantiate(Coin, CoinSpawnPoint.transform.position, Coin.transform.rotation);

        if (coinObject.TryGetComponent<Coin>(out Coin coin))
        {
            coin.MoveUp();
        }
        else
        {
            Debug.LogWarning("Coin game object has not reference to coin script");
        }
    }

    private void Update()
    {
        if (StateMachine != null)
            StateMachine.Tick();

        if (_animator != null && _navMeshAgent != null)
        {
            _animator.SetFloat("speed", _navMeshAgent.velocity.magnitude);
        }
    }
}
