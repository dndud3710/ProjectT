using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemMoney : InGameItem
{
    private int coins;
    protected override void Start()
    {
    base.Start();
        coins = Random.Range(0, 126);
    }
    public override void Use()
    {
        base.Use();
        StageManager.Instance.getCoins(coins);
        Destroy(gameObject);
    }
}
