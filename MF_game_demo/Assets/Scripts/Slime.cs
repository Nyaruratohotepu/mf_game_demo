using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//此层处理动画等前台逻辑
public class Slime : MeleeMonster
{
    public Slime(int id, GameObject self)
    {

        base.Id = id;
        base.Hp = 100;
        base.Atk = 10;
        base.AtkBetween = 1f;
        base.AtkBetweenLeft = 0;
        base.Name = "Slime_Green";
        base.ChineseName = "绿色史莱姆";
        base.Introduction = "非常弱小的怪物";
        base.MoveSpeed = 10f;
        base.NowState = MonsterEnum.MonsterState.idle;
        base.Target = null;
        base.HuntRange = 5;
        base.GameManager = GameObject.Find("GameManager").GetComponent<Manager>();
        base.Player = GameObject.Find("Maruko") as GameObject;
        base.Self = self;
        base.CC = self.GetComponent<CharacterController>();
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
