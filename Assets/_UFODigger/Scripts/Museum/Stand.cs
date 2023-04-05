using System.Linq;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Events;
using Utils;
using Button = UnityEngine.UI.Button;
using Random = UnityEngine.Random;

public class Stand : MonoBehaviour
{
    public float AlienYRotate;
    public Vector3 AlienOffsetPos = Vector3.zero;
    public AlienForMapData AlienForMapData;
    
    public AlienForExcavateData AlienForExcavation;
    public bool BuildOnStart;
    public bool IsStandBuild { get; private set; }

    [Header("Stand settings")]
    //stand size must equals alien size
    public AlienSize StandSize;

    public int BuildPrice;

    public GameObject SmallStand;
    public GameObject MidlStand;
    public GameObject BigStand;

    public GameObject[] ObjectsRemovedAfterBuild;
    public GameObject[] ObjectsAddedAfterBuild;

    public StadMove SmallStandMove;
    public StadMove MidlStandMove;
    public StadMove BigStandMove;
    private StadMove _standMove;

    public PointOnPlain[] BotViewPoints;
    
    [Header("Alien on stand settings")]
    public Alien AvailableAfterAlienFind;
    public Alien AlienOnStand;
    public Aliens Aliens;

    [Header("Layer excavation settings")] public LayerOfSota LayerType;
    public int LayerCount;
    public int SotaHealth;

    [Header("Stand UI settings")] public GameObject StandMenuButton;
    public float CameraOnStandZOffset = 6f;

    public UnityEvent OnStandPositionChange;
    public UnityEvent OnStandRemove;

    public PLayerData PLayerData;

    private UIBuildStand _uiBuild;

    private string _buildKey;
    private string _alienKey;

    private string _posXKey;
    private string _posZKey;

    private GameObject _standBuilding;

    private void Awake()
    {
        for (int i = 0; i < Aliens.AllAliens.Length; i++)
        {
            Aliens.AllAliens[i].LoadAlienData();
        }
        StandMenuButton.SetActive(false);

        switch (StandSize)
        {
            case AlienSize.small:
                _standBuilding = SmallStand;
                _standMove = SmallStandMove;
                break;
            case AlienSize.midle: _standBuilding = MidlStand;
                _standMove = MidlStandMove;
                break;
            case AlienSize.big: _standBuilding = BigStand;
                _standMove = BigStandMove;
                break;
            default:
                Debug.LogWarning("No stand for this size alien!");
                break;
        }

        var standGameObject = gameObject;
        _buildKey = $"Build {standGameObject.name}";
        _alienKey = $"Alien {standGameObject.name}";

        _posXKey = $"XStand{standGameObject.name}";
        _posZKey = $"ZStand{standGameObject.name}";

         StandMenuButton.GetComponent<Button>().onClick.AddListener(ShowAlienOnStandMenu);
        LoadStandData();
    }


    private void Start()
    {
        if (!IsStandBuild)
        {
            _uiBuild = FindObjectOfType<UIBuildStand>();
        }

        if (AlienOnStand != null)
        {
            SetupAlien();
        }
    }

    public void SetAlienOnMapKey()
    {
        AlienForMapData.StandSaveAlienKey = _alienKey;
        AlienForMapData.IsJustLook = false;
        AlienForMapData.Save();
    }
    
    private void SetupAlien()
    {
        AlienOnStand.AllAlienParts.LoadPartsStatus();
        var alienObject = Instantiate(AlienOnStand.AlienPrefab,transform.position,quaternion.identity,transform);
        var alienPart = alienObject.GetComponent<AlienPart>();
        alienPart.LoadPartsStatus();
        alienPart.HideExcavationParts();
        alienPart.ShowOpenParts();
        alienObject.transform.localEulerAngles = new Vector3(0,AlienYRotate,0);
        alienObject.transform.localPosition = AlienOffsetPos;
    }

    public void ActivateMove()
    {
        _standMove.gameObject.SetActive(true);
    }
    public void DeActivateMove()
    {
        _standMove.gameObject.SetActive(false);
        
    }
    public void SetAlienForExcavate()
    {
        if (AlienOnStand != null)
        {
            AlienForExcavation.AlienForExcavate = AlienOnStand;
        }
        else
        {
            Debug.LogWarning("No ALIEN for Excavation");
        }
        
        
        AlienForExcavation.Save();
    }

    private void ShowAlienOnStandMenu()
    {
        MainMenuController.Instance.ShowAlienMenu(this);
    }
    
    public void ShowAlienOnStandMenu(Stand stand)
    {
        MainMenuController.Instance.ShowAlienMenu(stand);
    }
    public void SaveStandData()
    {
        SaveLoadSystem.Save(_buildKey, IsStandBuild);

        if (AlienOnStand != null)
        {
            SaveLoadSystem.Save(_alienKey, AlienOnStand.name);
        }

        SaveLoadSystem.Save(_posXKey, transform.position.x);
        SaveLoadSystem.Save(_posZKey, transform.position.z);
    }

    //TODO: ???????? ?????????? ??????????? ? ???????? ???????
    private void Move()
    {
        OnStandPositionChange.Invoke();
    }

    private void Remove()
    {
        OnStandRemove.Invoke();
    }

    private void ChangeAlien()
    {
    }

    public void ShowStandMenuButton()
    {
        if (IsStandBuild)
        {
            StandMenuButton.SetActive(true);
        }
    }

    public void HideStandMenuButton()
    {
        StandMenuButton.SetActive(false);
    }

    public Vector3 GetViewPoint()
    {
        var randomPlain = BotViewPoints[Random.Range(0, BotViewPoints.Length)];
        return randomPlain.GetRandomPoint();
    }

    public void Build()
    {
        IsStandBuild = true;
        PLayerData.AddGold(-BuildPrice);
        _standBuilding.GetComponent<StandFundament>().Build(true);
        SaveStandData();
        StandMenuButton.SetActive(true);

        EnvironmentsSetting();
    }

    public void EnvironmentsSetting()
    {
        if (ObjectsAddedAfterBuild.Length > 0)
            foreach (var addedObject in ObjectsAddedAfterBuild)
            {
                addedObject.SetActive(true);
            }

        if (ObjectsRemovedAfterBuild.Length > 0)
            foreach (var removedObject in ObjectsRemovedAfterBuild)
            {
                removedObject.SetActive(false);
            }
        
        if (NavMeshController.Instance != null)
            NavMeshController.Instance.RebuildNavMesh();
    }

    private void OnApplicationQuit()
    {
        SaveStandData();
    }

    private void OnDestroy()
    {
        SaveStandData();
    }

    private void LoadStandData()
    {
        
        //Load Alien
        if (SaveLoadSystem.CheckKey(_alienKey))
        {
            var alienLoad = from alien in Aliens.AllAliens
                where alien.name == SaveLoadSystem.LoadString(_alienKey)
                select alien;

            var aliens = alienLoad.ToList();
            AlienOnStand = aliens.FirstOrDefault();

            if (AlienOnStand != null)
                AlienOnStand.LoadAlienData();

            if (aliens.Count() > 1)
            {
                Debug.LogWarning("in Aliens two alien with equals names");
            }
        }

        //Load build status
        IsStandBuild = SaveLoadSystem.CheckKey(_buildKey) ? SaveLoadSystem.LoadBool(_buildKey) : BuildOnStart;
        if (IsStandBuild)
        {
            _standBuilding.GetComponent<StandFundament>().Build(false);
        }

        //Load Position
        if (SaveLoadSystem.CheckKey(_posXKey) && SaveLoadSystem.CheckKey(_posZKey))
        {
            transform.position = new Vector3(SaveLoadSystem.LoadFloat(_posXKey),
                transform.position.y,
                SaveLoadSystem.LoadFloat(_posZKey));
        }
    }

}