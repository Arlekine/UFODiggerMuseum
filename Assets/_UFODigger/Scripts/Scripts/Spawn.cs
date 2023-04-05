using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    public GameObject _coin;
    public Animator animator;

    private void Start()
    {
        StartCoroutine(SpawnDelay());
        StartCoroutine(Anim());
    }

    IEnumerator Anim()
    {

        yield return new WaitForSeconds(5);
        animator.enabled = true;

    }

    IEnumerator SpawnDelay()
    {
        int count = 0;

        yield return new WaitForSeconds(3);


        while (count < 50)
        {
            Vector3 pos = new Vector3(transform.position.x + Random.Range(-15, 15), 0.1f, transform.position.z + Random.Range(-3, 6));

            Instantiate(_coin, pos, Quaternion.identity);


            yield return new WaitForSeconds(0.8f);
        }
    }
}
