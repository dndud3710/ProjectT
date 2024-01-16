using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemMoney : InGameItem
{
    private int coins;
    private void Start()
    {
        coins = Random.Range(0, 126);
    }
    public override void Use()
    {
        base.Use();
        StageManager.Instance.getCoins(coins);
        Destroy(gameObject);
    }
}
