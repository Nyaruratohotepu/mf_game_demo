using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalMonster : Monsterbase {
    public override int id { set; get; }//ID
    public override int hp { set; get; }//血量
    public override int atk { set; get; }//攻击力
    public override float atkbetween { set; get; }//攻击冷却
    public override string name { set; get; }//英文名
    public override string chinesename { set; get; }//中文名
    public override string introduction { set; get; }//介绍
    public override int movespeed { set; get; }//移动速度
    public override state nowstate { set; get; }//当前状态
    public override Vector3 targetpos { get; set; }//追逐目标位置
    public override bool isshoot { get; set; }//是否远程
    private GameObject monsterman;//怪物管理器
	// Use this for initialization
	void Start () {
        monsterman = GameObject.Find("monstermanager");
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    
}
