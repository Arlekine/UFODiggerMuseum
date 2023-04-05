using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class UICloseMap : MonoBehaviour
{
    [SerializeField] private GameObject _map;

    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(Close);
    }

    private void Close()
    {
        _map.SetActive(false);
    }
}
 