using UnityEngine;
using UnityEngine.Events;

public class BusVisitorDetector : MonoBehaviour
{
    public UnityEvent VisitorInBus;
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Visitor>(out Visitor visitor))
        {
            if (visitor.IsReturnToBus == true)
            {
                Destroy(visitor.gameObject);
                VisitorInBus?.Invoke();
            }


        }
    }
}
