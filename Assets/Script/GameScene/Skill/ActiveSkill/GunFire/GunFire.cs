using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunFire : ActiveSkills
{
    //Gun종류 무기를 꼇을경우 총알 발사
   
    public GameObject Bullet;

    protected override void Start()
    {
        base.Start();
        
    }
    private void Reset()
    {
        count = 1;
        coolDown = 3f;
        Duration = 2;
        ClearPrefabsTime = 5f;
        Speed = 7f;
    }
    public override void Use()
    {
        StartCoroutine(AttackSwordSlashStart());
    }
    public override void SkillLevelUp()
    {
        base.SkillLevelUp();

        count++;
    }
    /// <summary>
    /// 코루틴 패턴
    /// </summary>
    /// <returns></returns>
    IEnumerator AttackSwordSlashStart()
    {
        int c = count;
        Quaternion currentRotation = Quaternion.identity;
        
        while (true)
        {
            
            //스킬 재사용 시간
            yield return new WaitForSeconds(coolDown);
            c = count;
            while (c > 0)
            {
                c--;
                yield return new WaitForSeconds(0.2f);
                GameObject g = Instantiate(Bullet, ParentTransform);
                GunFireMove b = g.GetComponent<GunFireMove>();
                
                b.setThrowSkills(StageManager.Instance.playerScript.getDamage() * 2,
                    Duration, ClearPrefabsTime, Speed, pointtype
                    );
                g.transform.position = getPlayerTF().position;
                currentRotation = getPlayerRot().rotation;
                g.transform.rotation = currentRotation;
            }

        }
    }
}
