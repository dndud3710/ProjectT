using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapTile : MonoBehaviour
{
    void Start()
    {
        //게임 시작시 캐릭터 위치 가운데에 생성
        transform.position =  StageManager.Instance.Player.transform.position;
    }
   
}
