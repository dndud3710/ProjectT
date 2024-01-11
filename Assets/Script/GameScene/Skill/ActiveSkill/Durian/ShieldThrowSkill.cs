using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldThrowSkill : ActiveSkills
{
    //실드가 duration이 다 끝나고 없어진 후, 아니면 오브젝트 파괴 후에 쿨타임이 돈다
    //실드는 카운트 1 고정 (대신 업글시 크기가 늘어남)
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
    public GameObject Shield;

    protected override void Start()
    {
        base.Start();

    }
    public override void Use()
    {
        StartCoroutine(ShieldThrow());
    }
    /// <summary>
    /// 코루틴 패턴
    /// </summary>
    /// <returns></returns>
    IEnumerator ShieldThrow()
    {
        GameObject startCooldown = null; //이 오브젝트가 null이 될 때는 실드가 파괴되었을때
        while (true)
        {
            //실드가 존재할 경우에는 실행하지 않기
            if (startCooldown == null)
            {
                //스킬 재사용 시간
                yield return new WaitForSeconds(coolDown);
                GameObject g = Instantiate(Shield);
                startCooldown = g;
                DurianMove b = g.GetComponent<DurianMove>();
                g.transform.position = getPlayerTF().position;
                b.setThrowSkills(StageManager.Instance.playerScript.getDamage() * 2,
                Duration, ClearPrefabsTime, Speed
                );
                
                
            }
            yield return null;
        }
    }
}
