using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordMove : ThrowSkill
{
    /// <summary>
    /// swordslash 프리팹이 up방향으로 이동하는 스크립트
    /// 3f 만큼 Up방향으로 이동, 3f초 후 자동으로 사라짐
    /// </summary>
    public ParticleSystem p1;

    protected override void Start()
    {
        base.Start();
        p1.Play();
        //AudioManager.Instance.AudioPlaying(AudioManager.Instance.SFXSound[(int)AudioManager.SFXList.검기발사]);
        //setAU_(AudioManager.SFXList.검기맞음);
}
}
