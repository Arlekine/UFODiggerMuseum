using UnityEngine;
using TMPro;
using UnityEngine.Events;
using UnityEngine.UI;
public class  WikiDescription : MonoBehaviour
{
    public UnityEvent OnDescriptionClose;
    public Button CloseDescriptionButton;

    public GameObject Root;

    public TextMeshProUGUI NameText;
    public TextMeshProUGUI DescriptionText;

    public Image AlienIcon;
    public Image RarityIcon;
 
    private void Start()
    {
        CloseDescriptionButton.onClick.AddListener(Close);
        Root.SetActive(false);
    }

    private void Close()
    {
        Root.SetActive(false);
        OnDescriptionClose.Invoke();
    }

    public void Setup(Alien alien)
    {
        AlienIcon.sprite = alien.Shot;
        RarityIcon.sprite = alien.RarityIcon;
        NameText.text = alien.name;
        DescriptionText.text = alien.Discriprion;
        
        Root.SetActive(true);
        
    }
}
