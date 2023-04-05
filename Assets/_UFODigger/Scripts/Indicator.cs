using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Indicator : MonoBehaviour
{
    public TextMeshProUGUI distanceTxt;
    public Color TargetColor;

    [HideInInspector] public AddTargetIndicator objectWorld;
    [HideInInspector] public Transform Target;
    [HideInInspector] public RectTransform thisObject;
    [SerializeField] private GameObject _icons;

    [HideInInspector] public float padding;
    public float distance;
    public Transform View;

    public bool view;
    public bool isOnTop;
    Image image;

    Camera cam;
    private RectTransform _rectTransform;
    private RectTransform _textRectTransform;
    private bool _isIndicatorInLeftArea;

    private bool _active;

    private void Start()
    {
        cam = Camera.main;

        image = GetComponent<Image>();

        image.color = TargetColor;

        _rectTransform = GetComponent<RectTransform>();
        _textRectTransform = distanceTxt.GetComponent<RectTransform>();
    }

    public void ActivateIndicator()
    {
        _active = true;
    }

    public void DeactivateIndicator()
    {
        _active = false;
        if (image != null)
        {
            image.enabled = false;

            _icons.SetActive(false);
        }

        distanceTxt.enabled = false;
    }

    private void Update()
    {
        if (!_active)
            return;

        if (Target)
        {
            Vector3 convertedPosition = cam.WorldToViewportPoint(Target.position);
            if (convertedPosition.z < 0)
            {
                convertedPosition = Vector3Invert(convertedPosition);
                convertedPosition = Vector3FixEdge(convertedPosition);
            }

            convertedPosition = cam.ViewportToScreenPoint(convertedPosition);
            KeepCameraInside(convertedPosition);
        }
        else
        {
            Destroy(this.gameObject);

            return;
        }

        if (objectWorld)
            distance = Vector3.Distance(View.position, objectWorld.transform.position) * 10;

        distanceTxt.text = (int) distance + "m";

        if (_rectTransform.anchoredPosition.x >= 0)
        {
            if (_textRectTransform.localScale.x == 1)
            {
                _textRectTransform.localScale = new Vector3(-1, -1, 1);
            }
        }
        else
        {
            if (_textRectTransform.localScale.x == -1)
            {
                _textRectTransform.localScale = new Vector3(1, 1, 1);
            }
        }

        if (distance < 350)
        {
            if (view && isOnTop)
            {
                image.enabled = true;
                _icons.SetActive(true);
                //distanceTxt.enabled = true;
            }
            else
            {
                image.enabled = false;
                _icons.SetActive(false);
                distanceTxt.enabled = false;
            }
        }
        else
        {
            image.enabled = false;
            distanceTxt.enabled = false;
        }
    }

    private void KeepCameraInside(Vector2 reference)
    {
        if (reference.x > Screen.width + padding || reference.x < -30 ||
            reference.y > Screen.height + padding || reference.y < -30)
        {
            view = true;
        }
        else
        {
            view = false;
        }

        reference.x = Mathf.Clamp(reference.x, padding, Screen.width - padding);
        reference.y = Mathf.Clamp(reference.y, padding, Screen.height - padding);
        thisObject.transform.position = reference;

        if (View != null)
        {
            Vector3 dir = Target.position - View.position;
            float angle = Mathf.Atan2(-dir.x, dir.z) * Mathf.Rad2Deg;
            thisObject.eulerAngles = new Vector3(0, 0, angle);
        }
    }


    private Vector3 Vector3Invert(Vector3 viewport_position)
    {
        Vector3 invertedVector = viewport_position;
        invertedVector.x = 1f - invertedVector.x;
        invertedVector.y = 1f - invertedVector.y;
        invertedVector.z = 0;
        return invertedVector;
    }

    private Vector3 Vector3FixEdge(Vector3 vector)
    {
        Vector3 vectorFixed = vector;
        float highestValue = Vector3Max(vectorFixed);
        float lowerValue = Vector3Min(vectorFixed);
        float highestValueBetween = DirectionPreference(lowerValue, highestValue);

        if (highestValueBetween == highestValue)
        {
            vectorFixed.x = vectorFixed.x == highestValue ? 1 : vectorFixed.x;
            vectorFixed.y = vectorFixed.y == highestValue ? 1 : vectorFixed.y;
        }

        if (highestValueBetween == lowerValue)
        {
            vectorFixed.x = Mathf.Abs(vectorFixed.x) == lowerValue ? 0 : Mathf.Abs(vectorFixed.x);
            vectorFixed.y = Mathf.Abs(vectorFixed.y) == lowerValue ? 0 : Mathf.Abs(vectorFixed.y);
        }

        return vectorFixed;
    }

    private float Vector3Max(Vector3 vector)
    {
        float highestValue = Mathf.Max(vector.x, vector.y);
        return highestValue;
    }

    private float Vector3Min(Vector3 vector)
    {
        float lowerValue = 0f;
        lowerValue = vector.x <= lowerValue ? vector.x : lowerValue;
        lowerValue = vector.y <= lowerValue ? vector.y : lowerValue;

        return lowerValue;
    }

    private float DirectionPreference(float lowerValue, float highestValue)
    {
        lowerValue = Mathf.Abs(lowerValue);
        highestValue = Mathf.Abs(highestValue);

        float highestValueBetween = Mathf.Max(lowerValue, highestValue);

        return highestValueBetween;
    }
}