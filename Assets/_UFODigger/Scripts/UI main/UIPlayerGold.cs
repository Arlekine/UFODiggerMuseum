using TMPro;
using UnityEngine;

public class UIPlayerGold : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _count;
    [SerializeField] private PLayerData _playerData;
    private void Start()
    {
        _count.text = _playerData.GoldCount.ToString();
        _playerData.OnGoldCountChange.AddListener(SetGemCount);
    }

    private void SetGemCount()
    {
        _count.text = _playerData.GoldCount.ToString();
    }
}
