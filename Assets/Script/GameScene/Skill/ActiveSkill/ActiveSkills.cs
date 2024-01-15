using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActiveSkills : MonoBehaviour , IngameSkill
{

    //RectTransform������ UI, Transform�������� �����̴°� World
    [Tooltip("UI���� ��ǥ���� World���� ��ǥ����")]
    public PointType pointtype;
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

    //���� UI�� World�� canvas skill�� �θ���Ұ���, �÷��̾��� skill�� �θ�� �Ұ���
    protected Transform ParentTransform;

    Transform PlayerTF;
    Transform PlayerRot;
    //�ڵ�ȭ �����ؼ� �׳� ���� ��
    public Sprite SkillImage;
    public string SkillName;

    [TextArea(10, 5)]
    public string Description;
    public Sprite ESkillImage { get {  return SkillImage; } }

    public string ESkillName { get { return SkillName; } }

    public string EDiscription { get { return Description; } }

    protected virtual void Start()
    {
        PlayerTF = StageManager.Instance.Player.transform;
        PlayerRot = StageManager.Instance.playerScript.getShotPointAngle();
        if(pointtype == PointType.UI)
        {
            ParentTransform = StageManager.Instance.UISkillParent;
        }
        else if(pointtype ==PointType.World)
        {
            ParentTransform = StageManager.Instance.WorldSkillParent ;
        }
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
