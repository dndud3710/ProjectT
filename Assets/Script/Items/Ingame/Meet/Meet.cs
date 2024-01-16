using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meet : InGameItem
{
    public override void Use()
    {
        StageManager.Instance.playerScript.TakeHeal((float)(StageManager.Instance.playerScript.maxHP * (40f / 100f)));
        Destroy(gameObject);
    }
}
