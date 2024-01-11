using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveSkills : MonoBehaviour
{
    
    Transform PlayerTF;
    Transform PlayerRot;
    protected virtual void Start()
    {
        PlayerTF = StageManager.Instance.Player.transform;
        PlayerRot = StageManager.Instance.playerScript.getShotPointAngle();
    }
    public virtual void Use() { }

    protected Transform getPlayerTF()
    {
        return PlayerTF;
    }
    protected Transform getPlayerRot()
    {
        return PlayerRot;
    }
}
