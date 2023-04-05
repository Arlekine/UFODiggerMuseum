using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Utils;

public class UIExcavationSkill : MonoBehaviour
{
    [SerializeField]
    [RequireInterface(typeof(ISkill))]
    private Object _skill;

    public ISkill Skill => _skill as ISkill;

    [SerializeField] private GameObject _skillsWindow;

    [SerializeField] private UISkillsBag _bag;

    [SerializeField] private TextMeshProUGUI _levelValue;
    [SerializeField] private TextMeshProUGUI _description;
    [SerializeField] private TextMeshProUGUI _countValue;

    [SerializeField] private Image _skillImage;
    [SerializeField] private Image _skillUseImage;

    [SerializeField] private Button _skillUseButton;

    [SerializeField] private Transform _takeSkillContaintainer;

    private void Start()
    {
        Skill.LoadSkillData();
        SetSkillInfo();

        _skillUseButton.onClick.AddListener(UseSkill);
    }

    private void SetSkillInfo()
    {
        _levelValue.text = $"lvl:{Skill.Level + 1}";
        _description.text = Skill.Description;

        _countValue.text = Skill.Count.ToString();

        _skillImage.sprite = Skill.SkillSprite;
        _skillUseImage.sprite = Skill.SkillSprite;
    }

    private void UseSkill()
    {
        if (Skill.Count > 0)
        {
            Skill.Use();
            SetSkillInfo();

            if (_skillsWindow)
                _skillsWindow.SetActive(false);

            SetupSkillImage();
        }
    }

    private void SetupSkillImage()
    {
        var newImage = Instantiate(_skillUseImage.gameObject, _skillUseImage.transform.position, Quaternion.identity,
            _takeSkillContaintainer);
        newImage.transform.localScale *= 1.5f;
        _bag.AddSkill(Skill, newImage);
    }
}