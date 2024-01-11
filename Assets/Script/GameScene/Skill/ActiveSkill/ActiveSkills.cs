using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveSkills : MonoBehaviour
{
    [Tooltip("��ų�� � �������� ����")]
    public int count = 1;
    [Tooltip("��ų ��Ÿ��")]
    public float coolDown = 4f;
    [Tooltip("������Ʈ ���� Ƚ�� (-10�ϰ�� ����)")]
    public int Duration = 20;
    [Tooltip("������Ʈ �ı� �ð�")]
    public float ClearPrefabsTime = 20f;
    [Tooltip("������Ʈ�� ���󰡴� �ӵ�")]
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
