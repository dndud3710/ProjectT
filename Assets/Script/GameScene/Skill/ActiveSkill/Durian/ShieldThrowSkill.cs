using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldThrowSkill : ActiveSkills
{
    //�ǵ尡 duration�� �� ������ ������ ��, �ƴϸ� ������Ʈ �ı� �Ŀ� ��Ÿ���� ����
    //�ǵ�� ī��Ʈ 1 ���� (��� ���۽� ũ�Ⱑ �þ)
    
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
    /// �ڷ�ƾ ����
    /// </summary>
    /// <returns></returns>
    IEnumerator ShieldThrow()
    {
        GameObject startCooldown = null; //�� ������Ʈ�� null�� �� ���� �ǵ尡 �ı��Ǿ�����
        while (true)
        {
            //�ǵ尡 ������ ��쿡�� �������� �ʱ�
            if (startCooldown == null)
            {
                //��ų ���� �ð�
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
