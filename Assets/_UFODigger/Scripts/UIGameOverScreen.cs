using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Utils;

public class UIGameOverScreen : Singleton<UIGameOverScreen>
{
    public TextMeshProUGUI GameOverText;
    public GameObject root;
    private void Start()
    {
        root.SetActive(false);
    }
    public void ShowGameOverScreen(int ItemCollect) {

        GameOverText.text = $"You collect {ItemCollect} items!";
        root.SetActive(true);
    }
}
