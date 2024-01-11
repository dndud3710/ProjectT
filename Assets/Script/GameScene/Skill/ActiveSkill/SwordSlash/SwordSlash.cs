using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SwordSlash : ActiveSkills
{
    //Gun���� ���⸦ ������� �Ѿ� �߻�
    [Tooltip("��ų�� � �������� ����")]
    public int count = 2;
    [Tooltip("��ų ��Ÿ��")]
    public float coolDown = 2f;
    [Tooltip("������Ʈ ���� Ƚ�� (-10�ϰ�� ����)")]
    public int Duration = -10;
    [Tooltip("������Ʈ �ı� �ð�")]
    public float ClearPrefabsTime = 3f;
    [Tooltip("������Ʈ�� ���󰡴� �ӵ�")]
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
    /// �ڷ�ƾ ����
    /// </summary>
    /// <returns></returns>
    IEnumerator AttackSwordSlashStart()
    {
        int c = count;
        Quaternion currentRotation=Quaternion.identity;
        int turn = 1;
        while (true)
        {
            //��ų ���� �ð�
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
               
                
                //�ݴ�������� �����
                if (turn == -1)
                    g.transform.rotation = currentRotation * Quaternion.Euler(0, 0, 180);
                turn *= -1;
                yield return new WaitForSeconds(0.5f);
            }
        }
    }
}
