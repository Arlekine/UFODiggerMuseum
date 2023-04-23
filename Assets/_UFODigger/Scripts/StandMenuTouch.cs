using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class StandMenuTouch : MonoBehaviour
{
    public PLayerData PlayerData;
    public Stand Stand;

    private PointerEventData _pointerEventData = new PointerEventData(EventSystem.current);
    private List<RaycastResult> _touchResults = new List<RaycastResult>();
    
    private void OnMouseDown()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;
        
        foreach (var touch in Input.touches)
        {
            if (touch.phase == TouchPhase.Began)
            {
                if (IsAboveUI(touch.position))
                {
                    return;
                }
            }
        }
        
        if(Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began ){
            if(EventSystem.current.IsPointerOverGameObject(Input.touches[0].fingerId))
                return;
        }
        
        if (!PlayerData.IsTutorialComplete)
        {
            gameObject.SetActive(false);
            FindObjectOfType<TutorialManager>().NextTutorialTurn();
            //AppMetricaManager.instance.TutorialState("Click to stand");
        }

        Stand.ShowAlienOnStandMenu(Stand);
    }
    
    private bool IsAboveUI(Vector2 touchPos)
    {
        _pointerEventData.position = touchPos;
        EventSystem.current.RaycastAll(_pointerEventData, _touchResults);
        return _touchResults.Count > 0;
    }
}