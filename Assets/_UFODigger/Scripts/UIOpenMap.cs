using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class UIOpenMap : MonoBehaviour
{
    public AlienForMapData AlienForMapData;
    public bool IsButtonForChoose;
    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(OpenMap);
    }

    private void OpenMap()
    {
        if (IsButtonForChoose)
        {
            UIAlienMenu.Instance.AlienOnStand.SetAlienOnMapKey();
            SceneManager.LoadScene(4);
        }
        else
        {
            AlienForMapData.StandSaveAlienKey = "";
            AlienForMapData.IsJustLook = true;
            AlienForMapData.Save();
            SceneManager.LoadScene(4);
        }
    }
}
