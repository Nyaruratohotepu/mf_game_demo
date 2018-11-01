using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Monsterbase : MonoBehaviour {
    public abstract int id{set;get;}//ID
    public abstract int hp{set;get;}//血量
    public abstract int atk{set;get;}//攻击力
    public abstract float atkbetween { set; get; }//攻击冷却
    public abstract string name{set;get;}//英文名
    public abstract string chinesename{set;get;}//中文名
    public abstract string introduction{set;get;}//介绍
    public abstract int movespeed{set;get;}//移动速度
    public enum state//状态，平静，追逐玩家，濒死，致盲，减速，麻痹，混乱
    {
        idle,
        chasing,
        dying,
        blind,
        slowed,
        cantmove,
        crazy
    }
    public abstract state nowstate { set; get; }//当前状态
    public abstract Vector3 targetpos { get; set; }//追逐目标位置
    public abstract bool isshoot { set; get; }//是否是远程
    
	
}
