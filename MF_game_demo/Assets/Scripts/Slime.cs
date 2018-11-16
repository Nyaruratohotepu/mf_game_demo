using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

//此层处理动画等前台逻辑
public class Slime : MeleeMonster
{
    public Slime(int id, GameObject self)
    {

        base.Id = id;
        base.Hp = 100;
        base.Atk = 10;
        base.AtkBetween = 1.5f;
        base.AtkBetweenLeft = 0;
        base.Name = "Slime_Green";
        base.ChineseName = "绿色史莱姆";
        base.Introduction = "非常弱小的怪物";
        base.MoveSpeed = 1f;
        base.NowState = MonsterEnum.MonsterState.idle;
        base.Target = null;
        base.HuntRange = 5;
        base.GameManager = GameObject.Find("GameManager").GetComponent<Manager>();
        base.Player = GameObject.Find("maruko") as GameObject;
        base.Self = self;
        base.CC = self.GetComponent<CharacterController>();
        base.AC = self.GetComponent<Animator>();
        base.IsDying = false;
        base.AtkRange = 1;
        base.nav = self.GetComponent<NavMeshAgent>();
    }


    public override void Attack()
    {
        //播放攻击动画
        base.Attack();
        
    }

    public override void Die()
    {               
            //播放死亡动画
            GameManager.Destory(Self, 2f);
            base.Die();
        
    }

}
