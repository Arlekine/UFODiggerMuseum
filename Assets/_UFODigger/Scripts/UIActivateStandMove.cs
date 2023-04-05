using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIActivateStandMove : MonoBehaviour
{
    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(ActivateMove);
    }

    private void ActivateMove()
    {
        UIAlienMenu.Instance.AlienOnStand.ActivateMove();
    }
}
