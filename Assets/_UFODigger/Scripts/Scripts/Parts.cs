using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parts : MonoBehaviour
{
    Animator _animator;

    int _id;

    private void Awake()
    {
        Application.targetFrameRate = 60;
    }

    void Start()
    {
        _animator = GetComponent<Animator>();

        _id = 1;
    }



    public void Click()
    {
        Debug.Log("Button");

        _animator.SetInteger("id", _id);
        _animator.SetTrigger("active");
        _id++;
    }
}
