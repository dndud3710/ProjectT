using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunFire : ActiveSkills
{
    //Gun종류 무기를 꼇을경우 총알 발사
    [Tooltip("스킬이 몇개 나가는지 개수")]
    public int count = 1;
    [Tooltip("스킬 쿨타임")]
    public float coolDown =3f;
    [Tooltip("오브젝트 관통 횟수 (-10일경우 무한)")]
    public int Duration = 2;
    [Tooltip("오브젝트 파괴 시간")]
    public float ClearPrefabsTime = 5f;
    [Tooltip("오브젝트가 날라가는 속도")]
    public float Speed = 7f;
    public GameObject Bullet;

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
                GameObject g = Instantiate(Bullet);
                GunFireMove b = g.GetComponent<GunFireMove>();
                
                b.setThrowSkills(StageManager.Instance.playerScript.getDamage() * 2,
                    Duration, ClearPrefabsTime, Speed
                    );
                g.transform.position = getPlayerTF().position;
                currentRotation = getPlayerRot().rotation;
                g.transform.rotation = currentRotation;
            }

        }
    }
}
