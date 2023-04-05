using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MapSceneClose : MonoBehaviour
{
    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(LoadMuseum);
    }

    private void LoadMuseum()
    {
        SceneManager.LoadScene(1);
    }
}