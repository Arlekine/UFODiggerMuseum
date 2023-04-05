using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class MovePart : MonoBehaviour
{
    public float MoveTime = 2f;

    public UnityEvent OnPartPlaced;
    public Animation CubeAnimation;
    private Transform _part;
    private Transform _target;

    public void PlacePart(Transform part, Transform target)
    {
        part.SetParent(target);
        _part = part;
        _target = target;
        StartCoroutine(RotateAndMovePart());
    }

    IEnumerator RotateAndMovePart()
    {
        var elapsedTime = 0f;
        var partStartRotation = _part.transform.rotation;
        var partStartPosition = _part.transform.position;
        
        while (elapsedTime < MoveTime)
        {
            _part.transform.rotation = Quaternion.Lerp(partStartRotation, _target.transform.rotation,
                elapsedTime / MoveTime);
            _part.transform.position = Vector3.Lerp(partStartPosition, _target.transform.position,
                elapsedTime / MoveTime);
            elapsedTime += Time.deltaTime;

            yield return null;
        }

        _part.transform.rotation = _target.transform.rotation;
        _part.transform.position = _target.transform.position;


        OnPartPlaced.Invoke();
    }
}