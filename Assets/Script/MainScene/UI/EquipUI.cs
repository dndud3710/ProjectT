using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipUI : MonoBehaviour
{
    public GameObject equiponoff;

    //������ ȹ���ؼ� ������ �þ�� �θ� transform
    public Transform SlotParent;

    public Image[] equipIamges;
    public List<GameObject> Slots;


    private void Start()
    {
    }

    // �ʱ�ȭ : �κ��丮�� ��ȭ�� �Ͼ���� �ߵ�
    public void EquipImagesInit(EquipItem[] items_)
    {
        int i = 0;
        foreach (EquipItem items in items_) {
            if (items == null) {
                i++;
                continue;
            }
            equipIamges[i++].sprite = items.ItemImage.sprite;
        }
    }
    public void AddSlot(GameObject g__)
    {
        GameObject g_= Instantiate(GameManager.Instance.SlotPrefab, SlotParent);
        Slots.Add(g_);
        slotScript img_ = g_.GetComponent<slotScript>();
        EquipItem ei_ = g__.GetComponent<EquipItem>();

        img_.change(ei_.ItemImage.sprite); 
    }

}
