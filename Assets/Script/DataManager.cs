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
    Dictionary<int, string> ActiveSkillNames;
    Dictionary<int, Sprite> ActiveSkillSprite;
    Dictionary<int, string> ActiveSkillDiscription;


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
        ActiveSkillNames = new Dictionary<int, string>();
        ActiveSkillSprite = new Dictionary<int, Sprite>();
        ActiveSkillDiscription = new Dictionary<int, string>();
        for (int i = 0; i < ActiveSkillsObj.Length; i++)
        {
            ActiveSkillDictionary.Add(i, ActiveSkillsObj[i]);
            IngameSkill ing_ = ActiveSkillsObj[i].GetComponent<IngameSkill>();
            ActiveSkillNames.Add(i, ing_.ESkillName);
            ActiveSkillSprite.Add(i, ing_.ESkillImage);
            ActiveSkillDiscription.Add(i, ing_.EDiscription);
        }
        
    }

    public GameObject getActiveSkillObject(EActiveSkillType type)
    {
        return ActiveSkillDictionary[(int)type];
    }

    #endregion


    #region 스테이지 관련
    /// <summary>
    /// 스테이지의 정보 불러오기
    /// </summary>
    /// <param name="num"></param>
    /// <returns></returns>
    public GameObject[] getStageMonsters(int num)
    {
        IStages gett= stage[num-1].GetComponent<IStages>();
        return gett.getMonsters();
    }
    public GameObject getMapTile(int num)
    {
        IStages gett = stage[num - 1].GetComponent<IStages>();
        return gett.getMapTile();
    }
    public string getStageName(int num)
    {
        IStages gett = stage[num - 1].GetComponent<IStages>();
        return gett.getStageName();
    }
    public Sprite getStageImage(int num)
    {
        IStages gett = stage[num - 1].GetComponent<IStages>();
        return gett.getStageImage();
    }
    #endregion
}
