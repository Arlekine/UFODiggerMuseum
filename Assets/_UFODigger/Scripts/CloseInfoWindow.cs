using UnityEngine;
using UnityEngine.UI;

public class CloseInfoWindow : MonoBehaviour
{
    [SerializeField] private GameObject Info;
    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(Close);
    }

    private void Close()
    {
        Info.SetActive(false);
    }
}
