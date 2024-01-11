using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldThrowSkill : ActiveSkills
{
    //�ǵ尡 duration�� �� ������ ������ ��, �ƴϸ� ������Ʈ �ı� �Ŀ� ��Ÿ���� ����
    //�ǵ�� ī��Ʈ 1 ���� (��� ���۽� ũ�Ⱑ �þ)
    [Tooltip("��ų�� � �������� ����")]
    public int count = 1;
    [Tooltip("��ų ��Ÿ��")]
    public float coolDown = 4f;
    [Tooltip("������Ʈ ���� Ƚ�� (-10�ϰ�� ����)")]
    public int Duration = 20;
    [Tooltip("������Ʈ �ı� �ð�")]
    public float ClearPrefabsTime = 20f;
    [Tooltip("������Ʈ�� ���󰡴� �ӵ�")]
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
