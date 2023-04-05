using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIStartExcavation : MonoBehaviour
{
    private Button StartExcavationButton;
   private void Start()
    {
        StartExcavationButton = GetComponent<Button>();
        StartExcavationButton.onClick.AddListener(StartExcavation);
    }

    private void StartExcavation()
    {
        SceneManager.LoadScene(2);
    }
}
