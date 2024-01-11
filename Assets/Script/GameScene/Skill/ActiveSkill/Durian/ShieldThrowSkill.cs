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
    public float coolDown = 2f;
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
        StartCoroutine(AttackSwordSlashStart());
    }
    /// <summary>
    /// �ڷ�ƾ ����
    /// </summary>
    /// <returns></returns>
    IEnumerator AttackSwordSlashStart()
    {
        Quaternion currentRotation = Quaternion.identity;

        while (true)
        {
            //�ǵ尡 ������ ��쿡�� �������� �ʱ�
            //��ų ���� �ð�
            yield return new WaitForSeconds(coolDown);
            yield return new WaitForSeconds(0.2f);
            GameObject g = Instantiate(Shield);
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
