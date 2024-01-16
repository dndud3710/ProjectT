using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// stage1 stage2 stage3의 서로다른 이름의 스크립트를 한번에 부르기 위한 인터페이스
/// </summary>
public interface IStages
{
    public GameObject[] getMonsters();
}
public interface IngameSkill
{
    public Sprite ESkillImage { get; }
    public string ESkillName { get; }
    public string EDiscription { get; }
    public int ESkillLevel { get; }
    void Use() { } //패시브도 레벨업하거나 얻는순간 Use로 하게하려고 만듦
    void SkillLevelUp() { }

}

public interface WeaponSkillType
{
    //웨폰들은 각자 스킬 스크립트에 이것을 상속하고
    public EActiveSkillType getType();
}

/// <summary>
/// enum 모음
/// </summary>
public enum EActiveSkillType
{
    SwordSlash = 0,
    GunFire,
    Shield,
    ThrowHammer,
    BubbleBomb
}
public enum PointType
{
    UI,
    World,
}

public enum EInGameItemType
{
    EXP1,
    EXP2,
    EXP3,
    Heal,
    Magnet,
    Money
}

public enum EEquipItemType
{
    Weapon,
    Accessories,
    clothes,
    pants,
    belt,
    helmet
}
