using TMPro;
using UnityEngine;

public class UIInfo : MonoBehaviour
{
    public TextMeshProUGUI Description;

    public void Setup(string Discription)
    {
        gameObject.SetActive(true);
        Description.text = Discription;
    }
  
}