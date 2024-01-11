using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveSkills : MonoBehaviour
{
    [Tooltip("스킬이 몇개 나가는지 개수")]
    public int count = 1;
    [Tooltip("스킬 쿨타임")]
    public float coolDown = 4f;
    [Tooltip("오브젝트 관통 횟수 (-10일경우 무한)")]
    public int Duration = 20;
    [Tooltip("오브젝트 파괴 시간")]
    public float ClearPrefabsTime = 20f;
    [Tooltip("오브젝트가 날라가는 속도")]
    public float Speed = 2f;
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
