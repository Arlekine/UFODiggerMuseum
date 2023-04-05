using UnityEngine;
using UnityEngine.UI;

public class UISettingsLink : MonoBehaviour
{
    [SerializeField] private string _link;

    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(OpenLink);
    }

    private void OpenLink()
    {
        Application.OpenURL(_link);
    }

}
