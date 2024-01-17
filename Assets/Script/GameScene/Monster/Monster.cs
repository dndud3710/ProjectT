using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Monster : MonoBehaviour
{
    public EInGameItemType rootItem;
    protected Transform Player_tf;
    protected Player player_sc;

    public int MonsterID;
    public string Name;
    public int maxHp;
    public int curHp;
    public int Damage;
    public float Speed;
    public int Wave;
    Animator animator;
    protected virtual void Start()
    {
        Player_tf = StageManager.Instance.Player.transform;
        player_sc = StageManager.Instance.Player.GetComponent<Player>();

        animator = GetComponentInChildren<Animator>();
    }
    private void Update()
    {
    }
    public virtual void TakeDamage(int damage_)
    {
        curHp -= damage_;
        animator.SetTrigger("Hit");
        Dead();
    }
    public void Dead()
    {
        if (curHp <= 0)
        {
            StageManager.Instance.Monsterkill();
            StageManager.Instance.deadPrticlePlay(transform);
            ObjectPool.Instance.DeadMonster(Wave, this.gameObject);
        }
    }
}
