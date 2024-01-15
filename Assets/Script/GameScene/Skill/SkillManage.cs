using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkillManage : MonoBehaviour
{
    //skillmanager���� ��ų�� 3�� �ް� ����� �Է�
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
    //��ų�� 3������ �����µ� ���⼭ 
    private void getThreeSkills(GameObject[] g_)
    {
        for(int i=0;i<g_.Length;i++)
        {
            ThreeSkills[i] = g_[i];
            IngameSkill igs_ = ThreeSkills[i].GetComponent<IngameSkill>();
            setSkillSelect(igs_, i);
        }
    }
    //select 1 2 3 Ui���� num���༭ �ش� ��ų����ϱ�
    public void SelectSkill(int num)
    {
        //�÷��̾�� getSkill�� �̰��� �ް�
        if (num < 0 && num > 2) return;
        StageManager.Instance.playerScript.GetSkill(ThreeSkills[num]);
        
        OffLevelUpSelectSkills();
    }
    public void OnLevelUpSelectSkills()
    {
        int ELength = Enum.GetValues(typeof(EActiveSkillType)).Length;
        //������ UIȰ��ȭ ��Ű��
        LevelUpUI.SetActive(true);
        panel.SetActive(true);
        bool[] check = new bool[ELength];
        //��ų �Է�
        GameObject[] gg_ = new GameObject[ThreeSkills.Length];
        int enumlen = ELength;
        for (int i = 0; i < gg_.Length; i++)
        {
            EActiveSkillType ea_ = (EActiveSkillType)UnityEngine.Random.Range(0, enumlen);
            if (check[(int)ea_]== true) { i--;continue; } //�ߺ� ����
            
            check[(int)ea_]= true;
            gg_[i] = DataManager.Instance.getActiveSkillObject(ea_);
        }
        getThreeSkills(gg_);
    }
    private void OffLevelUpSelectSkills()
    {
        LevelUpUI.SetActive(false);
        panel.SetActive(false);
    }
}
