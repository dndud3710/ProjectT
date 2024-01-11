using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ThrowHammerSkill : ActiveSkills
{
    //해머가 돌아가면서 몬스터맞추면 방향을 틈 -> 탕특 축구공과 같음
    public GameObject HammerPrefabs;
    //이 오브젝트가 null이 될 때는 해머가 파괴되었을때
    //shield Throw와 다른점은 공이 여러개면 전부 사라질때까지 새로 스킬이 발동되지 않음
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
    /// 코루틴 패턴
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
            //남아있는 해머가 없을시에만 시작
            // 문제 1: cooldowngo => 남아있는 해머가 0일경우를 어떻게 확인하느냐
            // 제안 1: cooldowngo를 리스트에 놓고 instantiate할때마다 gameobject를 list에 넣어주고 count만큼의 list에담겨진 gameobject들이 전부 null일때 다시 시작
            // -> while할동안 계속 반복문이 수행되는 안좋은 현상 (하지만 count가 많아봤자 한자리수이고 순차적접근이라 괜찮을수도 있음)
            foreach (GameObject go in cooldowngo)
            {
                //하나라도 null이 아닐경우
                if (go != null)
                {
                    print("현재 있음");
                    ret = false;
                    break;
                }
            }
            if (ret)
            {
                cooldowngo.Clear();
                print("생성");
                yield return new WaitForSeconds(coolDown);
                //남아있는 공이 있을때
                while (c > 0)
                {
                    //스킬 재사용 시간
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
