using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EXP : InGameItem
{
    //EXP�������� ���򺰷� level�� �ٸ��� �޴� ����ġ�� �ٸ�
    public ParticleSystem starts; //��¦��¦
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
