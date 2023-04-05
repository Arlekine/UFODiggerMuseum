using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIMap : MonoBehaviour
{
    public Button ShowLowRarityAliens;
    public Button ShowMIddleRarityAliens;
    public Button ShowHighRarityAliens;

    public GameObject[] LowRarityAliens;
    public GameObject[] MiddleRarityAliens;
    public GameObject[] HighRarityAliens;

    private void Start()
    {
        ShowLowRarityAliens.onClick.AddListener(ShowLowRarity);
        ShowMIddleRarityAliens.onClick.AddListener(ShowMiddleRarity);
        ShowHighRarityAliens.onClick.AddListener(ShowHighRarity);

        ShowLowRarity();
    }

    private void ShowLowRarity()
    {
        Show(LowRarityAliens);
        Hide(MiddleRarityAliens);
        Hide(HighRarityAliens);

        ScaleUp(ShowLowRarityAliens);
        ScaleDown(ShowMIddleRarityAliens);
        ScaleDown(ShowHighRarityAliens);
    }

    private void ScaleUp(Button objects)
    {
        objects.transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);
    }
    private void ScaleDown(Button objects)
    {
        objects.transform.localScale = new Vector3(1, 1, 1);
    }
    private void Show(GameObject[] objects)
    {
        for (int i = 0; i < objects.Length; i++)
        {
            objects[i].SetActive(true);
        }
    }

    private void Hide(GameObject[] objects)
    {
        for (int i = 0; i < objects.Length; i++)
        {
            objects[i].SetActive(false);
        }
    }

    private void ShowMiddleRarity()
    {
        Show(MiddleRarityAliens);
        Hide(LowRarityAliens);
        Hide(HighRarityAliens);

        ScaleUp(ShowMIddleRarityAliens);
        ScaleDown(ShowLowRarityAliens);
        ScaleDown(ShowHighRarityAliens);
    }

    private void ShowHighRarity()
    {
        Show(HighRarityAliens);
        Hide(MiddleRarityAliens);
        Hide(LowRarityAliens);

        ScaleUp(ShowHighRarityAliens);
        ScaleDown(ShowLowRarityAliens);
        ScaleDown(ShowMIddleRarityAliens);
    }
}
