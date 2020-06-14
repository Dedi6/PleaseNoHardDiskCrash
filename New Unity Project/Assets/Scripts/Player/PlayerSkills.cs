using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerSkills 
{
    public event EventHandler<OnSkillUnlockedEventArgs> OnSkillUnlocked;
    public class OnSkillUnlockedEventArgs : EventArgs    {
        public SkillType skillType;
    }

    private List<HotKeyAbility> abilityList;
    public class HotKeyAbility
    {
        public SkillType skilltype;
        public Action activateSkill;
    }
    public enum SkillType
    {
        Teleport,
        LightningBolt,
        LightningWave,

    }
    private List<SkillType> unlockedSkillTypeList;

    public PlayerSkills()
    {
        unlockedSkillTypeList = new List<SkillType>();
    }
    public void UnlockSkill(SkillType skillType)
    {
        if(!isSkillUnlocked(skillType))
        {
            unlockedSkillTypeList.Add(skillType);
            OnSkillUnlocked?.Invoke(this, new OnSkillUnlockedEventArgs { skillType = skillType });
        }
    }

    public bool isSkillUnlocked(SkillType skillType)
    {
        return unlockedSkillTypeList.Contains(skillType); // returns true if there is and false if there isnt
    }
}
