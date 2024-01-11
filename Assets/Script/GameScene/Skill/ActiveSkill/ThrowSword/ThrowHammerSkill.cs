using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ThrowHammerSkill : ActiveSkills
{
    //�ظӰ� ���ư��鼭 ���͸��߸� ������ ƴ -> ��Ư �౸���� ����
    public GameObject HammerPrefabs;
    //�� ������Ʈ�� null�� �� ���� �ظӰ� �ı��Ǿ�����
    //shield Throw�� �ٸ����� ���� �������� ���� ����������� ���� ��ų�� �ߵ����� ����
    private List<GameObject> cooldowngo;
    bool ret;
    protected override void Start()
    {
        base.Start();
        cooldowngo = new List<GameObject>();
    }
    private void Reset()
    {
        count = 1;
        coolDown = 4f;
        Duration = 8;
        ClearPrefabsTime = 20f;
        Speed = 5f;
    }
    public override void Use()
    {
        StartCoroutine(HammerThrow());
    }
    /// <summary>
    /// �ڷ�ƾ ����
    /// </summary>
    /// <returns></returns>
    IEnumerator HammerThrow()
    {
        int c = count;
        while (true)
        {
            yield return new WaitForSeconds(0.01f);
            ret = true;
            c = count;
            //�����ִ� �ظӰ� �����ÿ��� ����
            // ���� 1: cooldowngo => �����ִ� �ظӰ� 0�ϰ�츦 ��� Ȯ���ϴ���
            // ���� 1: cooldowngo�� ����Ʈ�� ���� instantiate�Ҷ����� gameobject�� list�� �־��ְ� count��ŭ�� list������� gameobject���� ���� null�϶� �ٽ� ����
            // -> while�ҵ��� ��� �ݺ����� ����Ǵ� ������ ���� (������ count�� ���ƺ��� ���ڸ����̰� �����������̶� ���������� ����)
            foreach (GameObject go in cooldowngo)
            {
                //�ϳ��� null�� �ƴҰ��
                if (go != null)
                {
                    print("���� ����");
                    ret = false;
                    break;
                }
            }
            if (ret)
            {
                cooldowngo.Clear();
                print("����");
                yield return new WaitForSeconds(coolDown);
                //�����ִ� ���� ������
                while (c > 0)
                {
                    //��ų ���� �ð�
                    yield return new WaitForSeconds(0.1f);
                    GameObject g = Instantiate(HammerPrefabs, StageManager.Instance.StageSkills.transform);
                    cooldowngo.Add(g);
                    HammerMove b = g.GetComponent<HammerMove>();
                    g.transform.position = getPlayerTF().position;
                    b.setThrowSkills(StageManager.Instance.playerScript.getDamage(),
                    Duration, ClearPrefabsTime, Speed
                    );
                    c--;
                }
            }
            
    }
}
}
