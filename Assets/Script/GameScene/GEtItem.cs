using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GEtItem : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<InGameItem>(out InGameItem igi_))
        {
            //INgameItem�� ���� �δ� ����, �̷��� �������϶� �Ծ����� Use�ѹ����Ϸ���
            igi_.Use();
        }
    }
}
