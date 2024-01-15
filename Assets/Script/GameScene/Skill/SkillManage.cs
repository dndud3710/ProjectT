using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkillManage : MonoBehaviour
{
    //skillmanager에서 스킬을 3개 받고 여기다 입력
    public TextMeshProUGUI[] SkillName;
    public Image[] SkillImage;
    public TextMeshProUGUI[] SkillDescription;

    public GameObject panel;
    public GameObject LevelUpUI;

    private GameObject[] ThreeSkills;
    private void Awake()
    {
        ThreeSkills = new GameObject[3];
    }
    private void setSkillSelect(IngameSkill skill,int i)
    {
            SkillName[i].text = skill.ESkillName;
            SkillImage[i].sprite = skill.ESkillImage;
            SkillDescription[i].text = skill.EDiscription;
    }
    //스킬이 3가지가 나오는데 여기서 
    private void getThreeSkills(GameObject[] g_)
    {
        for(int i=0;i<g_.Length;i++)
        {
            ThreeSkills[i] = g_[i];
            IngameSkill igs_ = ThreeSkills[i].GetComponent<IngameSkill>();
            setSkillSelect(igs_, i);
        }
    }
    //select 1 2 3 Ui들은 num을줘서 해당 스킬얻게하기
    public void SelectSkill(int num)
    {
        //플레이어는 getSkill로 이것을 받고
        if (num < 0 && num > 2) return;
        StageManager.Instance.playerScript.GetSkill(ThreeSkills[num]);
        OffLevelUpSelectSkills();
    }
    public void OnLevelUpSelectSkills()
    {
        
        //레벨업 UI활성화 시키고
        LevelUpUI.SetActive(true);
        panel.SetActive(true);

        //스킬 입력
        GameObject[] gg_ = new GameObject[ThreeSkills.Length];
        int enumlen = Enum.GetValues(typeof(EActiveSkillType)).Length;
        for (int i = 0; i < gg_.Length;i++)
            gg_[i] = DataManager.Instance.getActiveSkillObject((EActiveSkillType)UnityEngine.Random.Range(0, enumlen));
        getThreeSkills(gg_);
    }
    private void OffLevelUpSelectSkills()
    {
        LevelUpUI.SetActive(false);
        panel.SetActive(false);
    }
}
