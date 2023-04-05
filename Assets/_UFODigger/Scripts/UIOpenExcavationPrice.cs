using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIOpenExcavationPrice : MonoBehaviour
{
    public UIAlienMenu AlienMenu;
    public AlienForExcavateData AlienData;
    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(ShowExcavatonPriceWindow);
    }

    private void ShowExcavatonPriceWindow()
    {
        AlienMenu.AlienOnStand.SetAlienForExcavate();
        AlienData.IsAlienExcavate = true;
        AlienData.Save();
        SceneManager.LoadScene(3);
    }
}
