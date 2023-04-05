using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
[RequireComponent(typeof(Button))]
public class UISkillsBag : MonoBehaviour
{
    [SerializeField] private GameObject _skillsWindow;

    [SerializeField] private BagSkill[] _bagSkills;

    [SerializeField] private float _skillMoveTime;

    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(OpenSkillWindow);
        _skillsWindow.SetActive(false);
    }

    private void OpenSkillWindow()
    {

        if (_skillsWindow)
            _skillsWindow.SetActive(true);
    }

    [Serializable]
    public struct BagSkill
    {
        public Transform Position;
        public ISkill Skill;
        public GameObject SkillImage;
        public bool IsFree;
    }

    public void AddSkill(ISkill skill, GameObject skillImage)
    {
        for (int i = 0; i < _bagSkills.Length; i++)
        {
            if (_bagSkills[i].IsFree)
            {
                _bagSkills[i].IsFree = false;
                _bagSkills[i].Skill = skill;
                _bagSkills[i].SkillImage = skillImage;
                StartCoroutine(MoveSkillInPosition(skillImage, _bagSkills[i].Position));
                break;
            }
        }
    }

    IEnumerator MoveSkillInPosition(GameObject skill, Transform target)
    {
        var moveTime = 0f;
        var startPosition = skill.transform.position;

        while (_skillMoveTime > moveTime)
        {
            skill.transform.position = Vector3.Lerp(startPosition, target.position, moveTime / _skillMoveTime);
            moveTime += Time.deltaTime;
            yield return null;
        }

    }

    public void RemoveSkill(ISkill skill)
    {
        for (int i = 0; i < _bagSkills.Length; i++)
        {
            if (!_bagSkills[i].IsFree && _bagSkills[i].Skill == skill)
            {
                Destroy(_bagSkills[i].SkillImage);
                _bagSkills[i].IsFree = true;
                _bagSkills[i].Skill = null;
                _bagSkills[i].SkillImage = null;

            }
        }
    }
}
