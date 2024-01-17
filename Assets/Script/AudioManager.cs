using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
    public AudioClip[] SFXSound; //1 æ∆¿Ã≈€∏‘¥¬º“∏Æ
    public float SFXVolume;
    public int chanels;
    public AudioSource[] SFXSource;

    public AudioSource MenuBeeps;

    public GameObject sori;
    List<GameObject> sfxs;
    GameObject BGMobj;
    GameObject SFXobj;
    public enum BGMList
    {
        ∏ﬁ¿Œ¿Ωæ« = 0,
        ¿¸≈ı¿Ωæ«
    }
    public enum SFXList
    {
        ∏ﬁ¥∫º±≈√ = 0,
        æ∆¿Ã≈€»πµÊ,
        ΩØµÂΩ√¿€,
        ΩØµÂæÓ≈√,
        √—πﬂªÁ,
        √—∏¬¿Ω,
        ∞À±‚πﬂªÁ,
        ∞À±‚∏¬¿Ω,
        ∏¡ƒ°¥¯¡¸,
        ∏¡ƒ°∏¬¿Ω

    }

    private void Awake()
    {
        if (Instance == null)
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
        BGMPlay(BGMList.∏ﬁ¿Œ¿Ωæ«);
    }
    void Init()
    {

        prevBgmindex = -1;
        BGMobj = new GameObject("BGM");
        SFXobj = new GameObject("SFX");

        sfxs = new List<GameObject>();
        SFXSource = new AudioSource[100];

        for (int i = 0; i < SFXSource.Length; i++)
        {
            GameObject g_ = new GameObject("SFXs");
            g_.transform.parent = transform;
            sfxs.Add(g_);
        }


        BGMSource = new AudioSource[BGMSound.Length];

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
        MenuBeeps = SFXobj.AddComponent<AudioSource>();
        MenuBeeps.clip = SFXSound[0];
        MenuBeeps.volume = 0.5f;
        MenuBeeps.playOnAwake = false;
        MenuBeeps.loop = false;
        for(int i=0;i<SFXSource.Length;i++)
        {
            SFXSource[i]= sfxs[i].AddComponent<AudioSource>();
            SFXSource[i].clip = null;
            SFXSource[i].volume = 0.2f;
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
        MenuBeeps.Play();
    }

    public void AudioPlaying(AudioClip c_)
    {
        foreach (AudioSource au_ in SFXSource)
        {
            print(au_.clip);
            print(au_.isPlaying);
           if(au_.clip==null||au_.isPlaying == false)
            {
                print("≥÷¿Ω");
                au_.clip = c_;
                au_.Play();
                break;
            }
           
        }
    }

}
