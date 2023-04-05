using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartScene : MonoBehaviour
{
    public bool _start;

    public int _sceneNum = 1;
    public Animator _fade;

    public void StartClick()
    {
        if (!_start)
            LevelManager.instance.DestroyThis();

        StartCoroutine(Delay());
    }

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(0.3f);

        _fade.SetTrigger("in");


        yield return new WaitForSeconds(0.5f);

        Application.LoadLevel(_sceneNum);
    }
}
