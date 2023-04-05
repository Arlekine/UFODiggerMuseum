using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detector : MonoBehaviour
{
    public bool _open, _end, _stop;
    public Vector3 _finalPos = new Vector3(0, 4, -1);



    float targetTime = 0.3f;
    float openTime = 0.5f;


    private void Update()
    {
        if (_stop) return;

        if(_open)
        {
            openTime -= Time.deltaTime;

            if (openTime <= 0.0f)
            {
                if(transform.position == _finalPos)
                {

                    _stop = true;

                    LevelManager.instance.DetectPart();

                    Destroy(gameObject, 1);
                }
                else
                {
                    _end = true;
                    transform.position = Vector3.MoveTowards(transform.position, _finalPos, Time.deltaTime * 6);
                }
            }
        }
        else
        {
            targetTime -= Time.deltaTime;

            if (targetTime <= 0.0f)
            {
                _open = true;

                openTime = 0.3f;
                targetTime = 0.5f;
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (_end) return;

        if(other.tag == "Sota")
        {
            _open = false;

        }
    }
}
