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

    //나중에 추가스킬 추가할수 있으면 하기

    //장착하기 누르면 장착됨
    public void getStat(out int health_,out int damage_)
    {
        health_ = Health;
        damage_ = Damage;

    }

}
