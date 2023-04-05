using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Bus : MonoBehaviour
{
    public PLayerData PLayerData;
    [Header("Events")] public UnityEvent OnVisitorSpawn;
    public UnityEvent OnVisitorDestroy;

    [Header("Bus settings")] public float TimeMoveBeetwinPoints = 2f;
    public float SpawnVisitorTime = 0.2f;
    public Transform VisitorSpawnPoint;
    public Visitor visitor;
    public float CameraOnBusZOffset = 25f;

    [Header("Reference")] public GiftController giftController;
    public CameraMove CameraMove;

    public Transform StartPoint;
    public Transform MuseumPoint;
    public Transform FinishPoint;
    public Transform EndPoint;

    public BusVisitorDetector VisitorDetector;

    private Vector3 _busScale;
    private const float _busYScaleFactor = 0.5f;

    public int VisitorExitBus { get; private set; }

    private int _visitorsInBus = 0;

    private void Start()
    {
        _visitorsInBus = giftController.VisitorCount;
        transform.position = EndPoint.position;
        _busScale = transform.localScale;

        VisitorDetector.VisitorInBus.AddListener(RemoveVisitor);
    }

    private void RemoveVisitor()
    {
        VisitorExitBus--;
        OnVisitorDestroy.Invoke();
        if (VisitorExitBus <= 0)
        {
            StartCoroutine(Move(MuseumPoint.position, FinishPoint.position, false));
        }
    }

    public void StartBus()
    {
        VisitorExitBus = 0;
        StartCoroutine(Move(StartPoint.position, MuseumPoint.position, true));

        if (!PLayerData.IsBusTutorialComplete)
        {
            CameraMove.TurnOffMouseCameraControl();
        }
    }

    IEnumerator Move(Vector3 from, Vector3 to, bool museumStop = false)
    {
        var elapsedTime = 0f;

        while (TimeMoveBeetwinPoints > elapsedTime)
        {
            transform.position = Vector3.Lerp(from, to, elapsedTime / TimeMoveBeetwinPoints);

            transform.localScale = new Vector3(transform.localScale.x,
                Random.Range(_busScale.y - _busYScaleFactor, _busScale.y + _busYScaleFactor),
                transform.localScale.z);


            if (!PLayerData.IsBusTutorialComplete)
            {
                CameraMove.MoveCamera(transform.position, CameraOnBusZOffset);
            }

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = to;
        transform.localScale = _busScale;

        if (museumStop)
        {
            if (!PLayerData.IsBusTutorialComplete)
            {
               PLayerData.IsBusTutorialComplete = true;
                PLayerData.SaveData();

                CameraMove.TurnOnMouseCameraControl(); 
            }

            giftController.ShowCounter();
            StartCoroutine(SpawnVisitors());
        }
        else
        {
            giftController.ShowGift();
            transform.position = EndPoint.position; //retrun bus to start
        }
    }

    IEnumerator SpawnVisitors()
    {
        while (_visitorsInBus > VisitorExitBus)
        {
            var visitorObject = Instantiate(visitor.gameObject, VisitorSpawnPoint.position, Quaternion.identity);
            visitorObject.GetComponent<Visitor>().IsFromBus = true;
            visitorObject.GetComponent<Visitor>().BusPoint = VisitorSpawnPoint;
            VisitorExitBus++;
            OnVisitorSpawn.Invoke();
            yield return new WaitForSeconds(SpawnVisitorTime);
        }
    }
}