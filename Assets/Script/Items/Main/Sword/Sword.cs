using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : EquipItem,WeaponSkillType
{

    //�⺻���ݵ� ����������ߴµ� �׳� �̷��� �ҵ�
    public EActiveSkillType skilltype;

    public EActiveSkillType getType()
    {
        return skilltype;
    }
}
