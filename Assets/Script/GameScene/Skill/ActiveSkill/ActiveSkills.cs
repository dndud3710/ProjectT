using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActiveSkills : MonoBehaviour , IngameSkill
{

    //RectTransform기준은 UI, Transform기준으로 움직이는건 World
    [Tooltip("UI기준 좌표인지 World기준 좌표인지")]
    public PointType pointtype;
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

    //만약 UI면 World면 canvas skill을 부모로할건지, 플레이어의 skill을 부모로 할건지
    protected Transform ParentTransform;

    Transform PlayerTF;
    Transform PlayerRot;
    //자동화 실패해서 그냥 만든 것
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
