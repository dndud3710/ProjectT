using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipItem : MonoBehaviour
{
    public EEquipItemType type;

    public string ItemName;
    public SpriteRenderer ItemImage;
    public int Health;
    public int Damage;

    //���߿� �߰���ų �߰��Ҽ� ������ �ϱ�

    //�����ϱ� ������ ������
    public void getStat(out int health_,out int damage_)
    {
        health_ = Health;
        damage_ = Damage;

    }

}
