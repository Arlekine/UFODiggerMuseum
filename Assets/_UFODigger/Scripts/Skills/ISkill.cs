using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISkill
{
    public int Count { get; set; }
    public int Level { get; set; }
    public string Description { get; set; }
    public Sprite SkillSprite { get; set; }
    public void Use();
    public void SaveSkillData();
    public void LoadSkillData();
    public void BuySkill();
}
