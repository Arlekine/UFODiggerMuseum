using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIWikiIcon : MonoBehaviour
{
    [SerializeField] private Image AlienImage;
    [SerializeField] private TextMeshProUGUI myString;

    public void SetLocalizateReference(string name)
    {
        myString.text = name;
    }

    public void SetSprite(Sprite sprite)
    {
        AlienImage.sprite = sprite;
    }
}
