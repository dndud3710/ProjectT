using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunFire : ActiveSkills
{
    //Gun���� ���⸦ ������� �Ѿ� �߻�
   
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
