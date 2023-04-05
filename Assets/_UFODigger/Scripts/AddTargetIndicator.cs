using UnityEngine;

public class AddTargetIndicator : MonoBehaviour
{
    public GameObject Indicator;
    public GameObject Canvas;
    public Transform View;

    private Indicator _indicator;

    private void Awake()
    {
        CreateTargetInCanvas();
    }

    public void Deactivate()
    {
        if (_indicator != null)
            _indicator.DeactivateIndicator();
    }
    public void Activate()
    {
        if (_indicator != null)
            _indicator.ActivateIndicator();
    }
    private void CreateTargetInCanvas()
    {
        _indicator = Instantiate(Indicator).GetComponent<Indicator>();
        _indicator.objectWorld = this;

        _indicator.transform.SetParent(Canvas.transform);
        _indicator.View = View;
        _indicator.transform.localPosition = Vector3.zero;
        _indicator.Target = this.transform;

        _indicator.padding = 30f;

        _indicator.thisObject = _indicator.GetComponent<RectTransform>();
    }
}