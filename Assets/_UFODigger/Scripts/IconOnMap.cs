using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class IconOnMap : MonoBehaviour
{
    public AlienForMapData AlienForMapData;
    public Alien Alien;
    public Alien OpenAfterAlien;

    public Image Icon;
    public GameObject NameObject;
    public TextMeshProUGUI NameText;

    public GameObject Back;
    public GameObject AvailableBack;
    public GameObject UnavailableBack;

    public Button ChooseButton;

    public UIExcavationPrice UIExcavationPrice;


    private void Start()
    {
        AlienForMapData.Load();
        if (Alien != null)
        {
            Alien.LoadAlienData();
            Alien.AllAlienParts.LoadPartsStatus();
        }

        if (OpenAfterAlien != null)
        {
            //not available
            if (!OpenAfterAlien.IsAlienOpen && !Alien.IsAlienOpen)
            {
                NameText.text = "????";

                UnavailableBack.SetActive(true);
                Back.SetActive(false);
                AvailableBack.SetActive(false);
                ChooseButton.enabled = false;
            }

            //available
            if (OpenAfterAlien.IsAlienOpen && !Alien.IsAlienOpen)
            {
                Icon.sprite = Alien.RarityIcon;
                NameText.text = "????";
                if (!AlienForMapData.IsJustLook)
                {
                    Back.SetActive(false);
                    UnavailableBack.SetActive(false);
                    AvailableBack.SetActive(true);
                    ChooseButton.onClick.AddListener(ShowStartExcavationPrice);
                }
            }

            //not available and open
            if (OpenAfterAlien.IsAlienOpen && Alien.IsAlienOpen)
            {
                Icon.sprite = Alien.RarityIcon;
                NameText.text = Alien.name;
                
                UnavailableBack.SetActive(false);
                Back.SetActive(true);
                AvailableBack.SetActive(false);
                ChooseButton.enabled = false;
            }
        }

        //start alien
        if (OpenAfterAlien == null && Alien != null)
        {
            Icon.sprite = Alien.RarityIcon;

            if (Alien.IsAlienOpen)
            {
                NameText.text = Alien.name;
            }
            else
            {
                NameText.text = "????";
            }

            ChooseButton.enabled = false;
        }

        //fake alien
        if (OpenAfterAlien == null && Alien == null)
        {
            NameText.text = "????";

            ChooseButton.enabled = false;
            UnavailableBack.SetActive(true);
            Back.SetActive(false);
            AvailableBack.SetActive(false);
        }
    }

    private void ShowStartExcavationPrice()
    {
        UIExcavationPrice.Setup(Alien);
    }
}