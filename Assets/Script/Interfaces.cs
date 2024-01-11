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
    World
}
