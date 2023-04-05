using UnityEngine;
using TMPro;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIExcavationPrice : MonoBehaviour
{
    public UnityEvent<Alien> OnExcavationStart;
    public Alien Alien;
    public TextMeshProUGUI PriceText;
    public Button AcceptAlienExcavate;
    public Button DeclineAlienExcavate;
    public PLayerData PlayerData;

    private void Awake()
    {
        gameObject.SetActive(false);
        DeclineAlienExcavate.onClick.AddListener(Hide);
        AcceptAlienExcavate.onClick.AddListener(GoToExcavate);
    }

    private void GoToExcavate()
    {
        if (Alien != null)
        {
            if (PlayerData.GoldCount >= Alien.ExcavationPrice)
            {
                OnExcavationStart.Invoke(Alien);
                PlayerData.AddGold(-Alien.ExcavationPrice);
                SceneManager.LoadScene(2);
                gameObject.SetActive(false);
            }
            else
            {
                PriceText.color = Color.red;
            }
        }
    }

    public void Setup(Alien alien)
    {
        Alien = alien;
        PriceText.text = Alien.ExcavationPrice.ToString();
        gameObject.SetActive(true);
        if (PlayerData.GoldCount < Alien.ExcavationPrice)
        {
            PriceText.color = Color.red;
        }
    }

    private void Hide()
    {
        gameObject.SetActive(false);
        SceneManager.LoadScene(1);
    }
}