using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class UIMoveAlien : MonoBehaviour
{
    [SerializeField] private GameObject menu;
    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(Close);
    }

    private void Close()
    {
        menu.SetActive(false);
    }
}
