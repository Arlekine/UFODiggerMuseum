using System;
using UnityEngine;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour
{
    public bool IsCameraStop;
    public GameObject TutorialPopup;

    public Button[] DisableButtons;
    public TutorialTurn[] Turns;
    public PLayerData PlayerData;

    private int _turnNow;

    [SerializeField] private bool isGallery;

    private void Start()
    {
        if (!PlayerData.IsTutorialComplete)
        {
            for (var i = 0; i < DisableButtons.Length; i++)
            {
                if (DisableButtons[i] != null)
                {
                    DisableButtons[i].enabled = false;
                }
            }

            for (int i = 0; i < Turns.Length; i++)
            {
                if (Turns[i].TurnButton != null)
                {
                    Turns[i].TurnButton.onClick.AddListener(NextTutorialTurn);
                }
            }

            _turnNow = 0;
            if (IsCameraStop)
            {
                CameraMove.Instance.TurnOffMouseCameraControl();
            }

            SetTurn(_turnNow);
        }
        else
        {
            TutorialPopup.SetActive(false);
        }
    }

    private void SetTurn(int turn)
    {
        TutorialPopup.SetActive(true);
        if (IsCameraStop)
        {
            CameraMove.Instance.TurnOffMouseCameraControl();
        }

        TutorialPopup.GetComponent<RectTransform>().anchoredPosition = new Vector2(Turns[turn].PosX, Turns[turn].PosY);
    }

    public void NextTutorialTurn()
    {
        _turnNow++;
        if (_turnNow >= Turns.Length)
        {
            for (var i = 0; i < DisableButtons.Length; i++)
            {
                if (DisableButtons[i] != null)
                {
                    DisableButtons[i].enabled = true;
                }
            }

            for (int i = 0; i < Turns.Length; i++)
            {
                if (Turns[i].TurnButton != null)
                {
                    Turns[i].TurnButton.onClick.RemoveListener(NextTutorialTurn);
                }
            }

            TutorialPopup.SetActive(false);
            if (IsCameraStop)
            {
                CameraMove.Instance.TurnOnMouseCameraControl();
            }
        }
        else
        {
            SetTurn(_turnNow);
        }

        //if(isGallery)
            //AppMetricaManager.instance.TutorialState("Start to excavations");
    }
}

[Serializable]
public class TutorialTurn
{
    public Transform TargetPosition;
    public Button TurnButton;
    public bool IsTurnComplete;
    public bool NeedButton;
    public float PosX;
    public float PosY;
}