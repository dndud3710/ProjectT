using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapTile : MonoBehaviour
{
    void Start()
    {
        //���� ���۽� ĳ���� ��ġ ����� ����
        transform.position =  StageManager.Instance.Player.transform.position;
    }
   
}
