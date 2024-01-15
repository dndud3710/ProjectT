using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : EquipItem,WeaponSkillType
{

    //기본공격도 나누었어야했는데 그냥 이렇게 할듯
    public EActiveSkillType skilltype;

    public EActiveSkillType getType()
    {
        return skilltype;
    }
}
