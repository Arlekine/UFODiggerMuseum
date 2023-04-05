using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CharacterMove : MonoBehaviour
{
    public bool _stop, _start;
    public GameObject _alien_1, _alien_2;
    public GameObject _coin;
    public Transform[] _movePos;

    private NavMeshAgent _agent;
    private Animator _animator;

    Vector3 _currPos;
    Vector3 look;
    int _posID;
    public float _distance;

    bool _check;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _agent = GetComponent<NavMeshAgent>();

        _posID = Random.Range(0, _movePos.Length - 1);
        _currPos = _movePos[_posID].position;

        StartCoroutine(StartDelay());
    }

    private void Update()
    {
        if (!_start) return;

        _distance = Vector3.Distance(transform.position, _currPos);

        if(_distance <= 0.9f && !_check)
        {
            StartCoroutine(Delay());
        }

        if(_check)
        {
            float dist_Alien_1 = Vector3.Distance(transform.position, _alien_1.transform.position);
            float dist_Alien_2 = Vector3.Distance(transform.position, _alien_2.transform.position);

            if (dist_Alien_1 < dist_Alien_2)
                look = _alien_1.transform.position;
            else
                look = _alien_2.transform.position;

            transform.LookAt(look);
        }

        _agent.destination = _currPos;

        _stop = _agent.velocity.sqrMagnitude == 0;

        _animator.SetBool("stop", _stop);
    }

    IEnumerator StartDelay()
    {
        float _time = Random.Range(1, 15);
        yield return new WaitForSeconds(_time);

        _start = true;
    }

    IEnumerator Delay()
    {
        _check = true;

        float _time = Random.Range(1.5f, 2.5f);

        yield return new WaitForSeconds(_time);

        _posID = Random.Range(0, _movePos.Length - 1);
        _currPos = _movePos[_posID].position;

        Vector3 _spawnPos = new Vector3(transform.position.x + Random.Range(-1, 1), 0.1f, transform.position.z + Random.Range(-1, 1));

        Instantiate(_coin, _spawnPos, Quaternion.identity);

        _check = false;
    }
}
