using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class ItemChest : MonoBehaviour
{

    private void Start()
    {
        
    }
    public void detroy()
    {
        StageManager.Instance.makeItem(transform,(EInGameItemType)Random.Range(3,6));
        Destroy(gameObject);
    }
}
