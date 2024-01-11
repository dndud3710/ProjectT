using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour

{
    public static AudioManager Instance;
    [Header("GameBGM")]
    public AudioClip[] BGMSound;
    public float BGMVolume;
    int prevBgmindex;
    public AudioSource[] BGMSource;

    [Header("SFX")]
    public AudioClip[] SFXSound;
    public float SFXVolume;
    public int chanels;
    public AudioSource[] SFXSource;


    GameObject BGMobj;
    GameObject SFXobj;
    public enum BGMList
    {
        메인음악=0,
        전투음악
    }
    public enum SFXList
    {
        메뉴선택=0
    }

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        Init();
        BGMPlay(BGMList.메인음악);
    }
    void Init()
    {
        prevBgmindex = -1;
        BGMobj = new GameObject("BGM");
        SFXobj = new GameObject("SFX");
        BGMSource = new AudioSource[BGMSound.Length];
        SFXSource = new AudioSource[SFXSound.Length];

        BGMobj.transform.parent = transform;
        SFXobj.transform.parent = transform;

        for (int i = 0; i < BGMSound.Length; i++)
        {
            BGMSource[i] = BGMobj.AddComponent<AudioSource>();
            BGMSource[i].clip = BGMSound[i];
            BGMSource[i].volume = 0.15f;
            BGMSource[i].playOnAwake = false;
            BGMSource[i].loop = true;
        }

        for (int i = 0; i < SFXSound.Length; i++)
        {
            SFXSource[i] = SFXobj.AddComponent<AudioSource>();
            SFXSource[i].clip = SFXSound[i];
            SFXSource[i].volume = 0.5f;
            SFXSource[i].playOnAwake = false;
            SFXSource[i].loop = false;
        }


    }
    public void BGMPlay(BGMList bg)
    {
        if (prevBgmindex != -1)
        {
            BGMSource[prevBgmindex].Stop();
        }
        BGMSource[(int)bg].Play();
        prevBgmindex = (int)bg;
    }
    public void SFXPlay(SFXList sf)
    {
        SFXSource[(int)sf].Play();
    }
    public void MenuBeepPlay()
    {
        SFXSource[(int)SFXList.메뉴선택].Play();
    }

}
