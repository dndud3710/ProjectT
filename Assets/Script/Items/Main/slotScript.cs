using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class slotScript : MonoBehaviour
{
    public Image spr;

    public void change(Sprite spr_)
    {
        spr.sprite = spr_;
    }
}
