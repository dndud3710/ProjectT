using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MisiileSkill : ActiveSkills
{

    public GameObject Bullet;
    
    protected override void Start()
    {
        base.Start();

    }
    private void Reset()
    {
        count = 1;
        coolDown = 4f;
        Duration = 1;
        ClearPrefabsTime = 7f;
        Speed = 3f;
    }
    public override void Use()
    {
        StartCoroutine(AttackSwordSlashStart());
    }

    public Quaternion SearchMonster()
    {
        return StageManager.Instance.playerScript.CastAround();
    }

    /// <summary>
    /// �ڷ�ƾ ����
    /// </summary>
    /// <returns></returns>
    IEnumerator AttackSwordSlashStart()
    {
        int c = count;
        Quaternion currentRotation = Quaternion.identity;
        
        while (true)
        {
            //��ų ���� �ð�
            yield return new WaitForSeconds(coolDown);
            c = count;
            while (c > 0)
            {
                c--;
                yield return new WaitForSeconds(1f);
                GameObject g = Instantiate(Bullet);
                MissileMove b = g.GetComponent<MissileMove>();

                b.setThrowSkills(StageManager.Instance.playerScript.getDamage(),
                    Duration, ClearPrefabsTime, Speed
                    );
                g.transform.position = getPlayerTF().position;
                currentRotation = SearchMonster();
                g.transform.rotation = currentRotation;
            }
        }
    }
    
}
