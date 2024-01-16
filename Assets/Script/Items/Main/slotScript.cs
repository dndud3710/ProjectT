using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class slotScript : MonoBehaviour
{
    public Image spr;
    public Button button;
    public EquipItem Item;

    private void Start()
    {
        button = gameObject.GetComponent<Button>();
        button.onClick.AddListener(delegate { GameManager.Instance.equipUI.SelectItem(Item); });
        button.onClick.AddListener(delegate { AudioManager.Instance.MenuBeepPlay(); });
    }
    public void change(Sprite spr_)
    {
        spr.sprite = spr_;
    }
}
