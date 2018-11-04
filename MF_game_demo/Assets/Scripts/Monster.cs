using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class Monster
{
    public abstract int Id { set; get; }//ID
    public abstract GameObject Player { set; get; }//玩家
    public abstract GameObject Self { set; get; }//自己绑定的怪物物体
    public abstract int Hp { set; get; }//血量
    public abstract int Atk { set; get; }//攻击力
    public abstract float AtkBetween { set; get; }//攻击冷却
    public abstract float AtkBetweenLeft { set; get; }//下一次攻击时间
    public abstract string Name { set; get; }//英文名
    public abstract string ChineseName { set; get; }//中文名
    public abstract string Introduction { set; get; }//介绍
    public abstract float MoveSpeed { set; get; }//移动速度
    public abstract MonsterEnum.MonsterState NowState { set; get; }//当前状态
    public abstract GameObject Target { get; set; }//目标
    public abstract float HuntRange { set; get; }//发现玩家的极限距离
    public abstract Manager GameManager { set; get; }
    public abstract CharacterController CC { set; get; }




    public abstract void Attack();  //攻击
    public abstract void Die(); //死亡
    //由脚本在Update中调用
    public abstract void OnUpdateCallback();
    //由脚本在FixedUpdate中调用
    public abstract void OnFixedUpdateCallback();
}
