using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SwordSlash : ActiveSkills
{
    //검기 기본공격중에 하나
    
    public GameObject SlashPrefabs;
    protected override void Start()
    {
        base.Start();
    }
    private void Reset()
    {
        count = 2;
        coolDown = 2f;
        Duration = -10;
        ClearPrefabsTime = 3f;
        Speed = 4f;
    }
    public override void Use()
    {
        StartCoroutine(AttackSwordSlashStart());
    }
    /// <summary>
    /// 코루틴 패턴
    /// </summary>
    /// <returns></returns>
    IEnumerator AttackSwordSlashStart()
    {
        int c = count;
        Quaternion currentRotation=Quaternion.identity;
        int turn = 1;
        while (true)
        {
            //스킬 재사용 시간
            yield return new WaitForSeconds(coolDown);
            c = count;
            turn = 1;

            if (getPlayerRot() != null) { 
            currentRotation = getPlayerRot().rotation;
            }
            while (c > 0)
            {
                c--;
                GameObject g = Instantiate(SlashPrefabs);
                SwordMove b = g.GetComponent<SwordMove>();
                b.setThrowSkills(StageManager.Instance.playerScript.getDamage(),
                   Duration, ClearPrefabsTime, Speed
                   );
                g.transform.position = getPlayerTF().position;
                g.transform.rotation = currentRotation;
               
                
                //반대방향으로 만들기
                if (turn == -1)
                    g.transform.rotation = currentRotation * Quaternion.Euler(0, 0, 180);
                turn *= -1;
                yield return new WaitForSeconds(0.5f);
            }
        }
    }
}
