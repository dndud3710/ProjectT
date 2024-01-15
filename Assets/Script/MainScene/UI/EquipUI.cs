using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EquipUI : MonoBehaviour
{
    public GameObject equiponoff;
    public GameObject equipWindow;

    //아이템 획득해서 슬롯이 늘어날때 부모 transform
    public Transform SlotParent;

    public EquipSlot[] equipIamges;
    public List<GameObject> Slots;
    public TextMeshProUGUI damage;
    public TextMeshProUGUI health;

    private EquipItem curSelectItem;
    public Image itemImage;
    public TextMeshProUGUI ItemName;
    public TextMeshProUGUI ItemDamage;
    public TextMeshProUGUI ItemHealth;

    public PlayerInfo player;


    private void Start()
    {
        damage.text = $"{player.getDamage()}";
        health.text = $"{player.getHealth()}";
    }


    // 초기화 : 인벤토리에 변화가 일어났을때 발동
    public void EquipImagesInit(EquipItem items)
    {
         equipIamges[(int)items.type].image.sprite = items.ItemImage.sprite;
    }
    public void AddSlot(GameObject g__)
    {
        GameObject g_= Instantiate(GameManager.Instance.SlotPrefab, SlotParent);
        Slots.Add(g_);
        slotScript img_ = g_.GetComponent<slotScript>();
        EquipItem ei_ = g__.GetComponent<EquipItem>();

        img_.change(ei_.ItemImage.sprite);
        img_.Item = ei_;
    }

    public GameObject getSlot(GameObject g_)
    {
        GameObject gg=null;
        foreach (GameObject obj in Slots)
        {
            if (obj.GetComponent<slotScript>().Item.gameObject == g_)
            {
                gg = obj;
            }
                
        }
        
        return gg;
    }
    public  void InitStat(int damage,int health)
    {
        this.damage.text = $"{damage}";
        this.health.text = $"{health}";
    }

    public void SelectItem(EquipItem eq_)
    {
        equipWindow.SetActive(true);
        curSelectItem = eq_;
        itemImage.sprite = eq_.ItemImage.sprite;
        ItemName.text = eq_.ItemName;
        ItemDamage.text = eq_.Damage.ToString();
        ItemHealth.text = eq_.Health.ToString();
    }
    public void EquipItem()
    {
        if (curSelectItem == null) { return; }
        player.setEquipItem(curSelectItem);
    }
}
