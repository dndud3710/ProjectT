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
    World
}
