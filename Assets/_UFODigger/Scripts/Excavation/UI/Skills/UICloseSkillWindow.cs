using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class UICloseSkillWindow : MonoBehaviour
{
    [SerializeField] private GameObject _skillsWindow;

    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(CloseSkillWindow);
    }

    private void CloseSkillWindow()
    {

        if (_skillsWindow)
            _skillsWindow.SetActive(false);
    }
}
