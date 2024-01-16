using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class MapMove : MonoBehaviour
{
    private void Update()
    {
        PlayerDistance();
    }
    public void PlayerDistance()
    {
        var playerpos = StageManager.Instance.Player.transform.position;
        var mypos = this.transform.position;
        float dis_ = Vector2.Distance(playerpos, mypos);
    
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.CompareTag("PlayerMap"))
        {
            var playerpos = StageManager.Instance.Player.transform.position;
            var playerVec = StageManager.Instance.playerScript.getVector();

            float diffX = Mathf.Abs(playerpos.x - transform.position.x);
            float diffY = Mathf.Abs(playerpos.y - transform.position.y);

            float dirX = playerVec.x < 0 ? -1 : 1;
            float dirY = playerVec.y < 0 ? -1 : 1;

            if (diffX > diffY)
            {
                transform.Translate(Vector3.right * dirX * 20);
            }
            else if(diffX < diffY)
            {
                transform.Translate(Vector3.up * dirY * 20);
            }
            //if (UnityEngine.Random.Range(1, 5) == 3)
            {
                GameObject g_ = Instantiate(StageManager.Instance.ItemChests);
                g_.transform.position = ObjectPool.Instance.MonsterRegeneratorRange();
            }
        }

    }
}
