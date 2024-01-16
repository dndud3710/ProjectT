using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GEtItem : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<InGameItem>(out InGameItem igi_))
        {
            //INgameItem을 따로 두는 이유, 이렇게 게임중일때 먹었을때 Use한번에하려고
            igi_.Use();
        }
    }
}
