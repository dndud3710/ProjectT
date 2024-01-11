using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public static DataManager Instance;
    public GameObject[] stage;

    /// <summary>
    /// ��ų �����յ�
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

    #region ��ų ������ ����
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



    //������������ �ٸ� �̸��� ��ũ��Ʈ�� �ִ� ex) stage1 stage2 stage3... �׷� ���⼭ Ư�� gameobject�� ���������� �ҷ��� �����?
    public GameObject[] getStageMonsters(int num)
    {
        IStages gett= stage[num-1].GetComponent<IStages>();
        return gett.getMonsters();
    }
}
