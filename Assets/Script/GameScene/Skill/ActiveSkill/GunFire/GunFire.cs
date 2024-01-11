using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunFire : ActiveSkills
{
    //Gun���� ���⸦ ������� �Ѿ� �߻�
    [Tooltip("��ų�� � �������� ����")]
    public int count = 1;
    [Tooltip("��ų ��Ÿ��")]
    public float coolDown =3f;
    [Tooltip("������Ʈ ���� Ƚ�� (-10�ϰ�� ����)")]
    public int Duration = 2;
    [Tooltip("������Ʈ �ı� �ð�")]
    public float ClearPrefabsTime = 5f;
    [Tooltip("������Ʈ�� ���󰡴� �ӵ�")]
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
