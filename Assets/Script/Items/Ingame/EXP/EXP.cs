using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EXP : InGameItem
{
    //EXP아이템은 색깔별로 level이 다르며 받는 경험치도 다름
    public ParticleSystem starts; //반짝반짝
    public int Level;
    private void Start()
    {
        starts.Play();
    }
    public override void Use()
    {
        StageManager.Instance.playerScript.getEXP(StageManager.Instance.getItemExp(Level));
        Destroy(gameObject);
    }
    
}
