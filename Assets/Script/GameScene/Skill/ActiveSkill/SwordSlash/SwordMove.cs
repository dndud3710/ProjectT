using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordMove : ThrowSkill
{
    /// <summary>
    /// swordslash �������� up�������� �̵��ϴ� ��ũ��Ʈ
    /// 3f ��ŭ Up�������� �̵�, 3f�� �� �ڵ����� �����
    /// </summary>
    public ParticleSystem p1;

    protected override void Start()
    {
        base.Start();
        p1.Play();
    }
}
