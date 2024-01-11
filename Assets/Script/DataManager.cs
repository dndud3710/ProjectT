using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public static DataManager Instance;
    public GameObject[] stage;

    /// <summary>
    /// 스킬 프리팹들
    /// </summary>
    public GameObject[] ActiveSkillsObj;
    Dictionary<int, GameObject> ActiveSkillDictionary;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            Init();
        }
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        
    }

    #region 스킬 프리팹 관리
    void Init()
    {
        ActiveSkillDictionary = new Dictionary<int, GameObject>();

        for(int i=0;i< ActiveSkillsObj.Length;i++)
            ActiveSkillDictionary.Add(i, ActiveSkillsObj[i]);
        
    }

    public GameObject getActiveSkillObject(EActiveSkillType type)
    {
        return ActiveSkillDictionary[(int)type];
    }
    #endregion



    //스테이지마다 다른 이름의 스크립트가 있다 ex) stage1 stage2 stage3... 그럼 여기서 특정 gameobject의 스테이지를 불러올 방법은?
    public GameObject[] getStageMonsters(int num)
    {
        IStages gett= stage[num-1].GetComponent<IStages>();
        return gett.getMonsters();
    }
}
