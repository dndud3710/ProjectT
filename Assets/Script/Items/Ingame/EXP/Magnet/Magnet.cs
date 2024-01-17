using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magnet : InGameItem
{
    public override void Use()
    {
        base.Use();
        List<InGameItem> magnetList= StageManager.Instance.MagenetItem();
        foreach (InGameItem item in magnetList)
        {
            item.MagnetOn();
        }
        Destroy(gameObject);
    }
}
