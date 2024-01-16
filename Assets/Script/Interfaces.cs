using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// stage1 stage2 stage3�� ���δٸ� �̸��� ��ũ��Ʈ�� �ѹ��� �θ��� ���� �������̽�
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
    void Use() { } //�нú굵 �������ϰų� ��¼��� Use�� �ϰ��Ϸ��� ����
    void SkillLevelUp() { }

}

public interface WeaponSkillType
{
    //�������� ���� ��ų ��ũ��Ʈ�� �̰��� ����ϰ�
    public EActiveSkillType getType();
}

/// <summary>
/// enum ����
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
