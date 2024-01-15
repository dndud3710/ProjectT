using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldThrowSkill : ActiveSkills
{
    //실드가 duration이 다 끝나고 없어진 후, 아니면 오브젝트 파괴 후에 쿨타임이 돈다
    //실드는 카운트 1 고정 (대신 업글시 크기가 늘어남)
    
    public GameObject Shield;
    protected override void Start()
    {
        base.Start();
    }
    private void Reset()
    {
        count = 1;
        coolDown = 4f;
        Duration = 20;
        ClearPrefabsTime = 20f;
        Speed = 2f;
    }
    public override void Use()
    {
        StartCoroutine(ShieldThrow());
    }
    public override void SkillLevelUp()
    {
        base.SkillLevelUp();

        Shield.transform.localScale += new Vector3(0.2f, 0.2f,0);
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
                GameObject g = Instantiate(Shield,ParentTransform);
                startCooldown = g;
                DurianMove b = g.GetComponent<DurianMove>();
                g.transform.position = getPlayerTF().position;
                b.setThrowSkills(StageManager.Instance.playerScript.getDamage() * 2,
                Duration, ClearPrefabsTime, Speed, pointtype
                );
                
                
            }
            yield return null;
        }
    }
}
