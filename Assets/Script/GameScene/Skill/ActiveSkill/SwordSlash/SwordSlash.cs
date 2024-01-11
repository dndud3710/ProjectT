using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SwordSlash : ActiveSkills
{
    //Gun종류 무기를 꼇을경우 총알 발사
    [Tooltip("스킬이 몇개 나가는지 개수")]
    public int count = 2;
    [Tooltip("스킬 쿨타임")]
    public float coolDown = 2f;
    [Tooltip("오브젝트 관통 횟수 (-10일경우 무한)")]
    public int Duration = -10;
    [Tooltip("오브젝트 파괴 시간")]
    public float ClearPrefabsTime = 3f;
    [Tooltip("오브젝트가 날라가는 속도")]
    public float Speed = 4f;
    public GameObject SlashPrefabs;
    protected override void Start()
    {
        base.Start();
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
