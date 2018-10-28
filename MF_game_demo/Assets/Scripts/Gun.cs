using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//具体的枪械来实现这个抽象数据类
public abstract class Gun
{

    public abstract GunEnum.GunType GunType { set; get; }
    //枪械类型
    public abstract GunEnum.FireType FireType { set; get; }
    //击发方式
    public abstract GunEnum.Rarity Rarity { set; get; }
    //稀有度
    public abstract string GunName { set; get; }
    //枪械名称
    public abstract int Damage { set; get; }
    //伤害量
    public abstract float DamageRange { set; get; }
    //射程
    public abstract int Magazine { set; get; }
    //弹夹容量
    public abstract int MagazineLeft { set; get; }
    //残弹余量
    public abstract float Cd { set; get; }
    //两发间隔
    public abstract float AimTime { set; get; }
    //瞄准时间、前摇
    public abstract bool IsTriggerPulled { set; get; }
    //扳机状态
    public abstract float NextFireTime { set; get; }
    //下一发子弹将在几秒后射出
    public abstract Vector3 AimDirection { set; get; }
    //枪口方向
    public abstract GameObject GunObject { set; get; }
    //指向此枪
    public abstract GameObject Muzzle { set; get; }
    //枪口
    public abstract GameObject MuzzleFlare { set; get; }
    //枪焰
    public abstract GameObject Owner { set; get; }
    //瞄准，返回瞄准位置，每帧调用一次
    public abstract string BulletPrefabPath { set; get; }
    //public abstract ParticleSystem Bullet { set; get; }
    ////子弹用粒子系统实现
    public abstract Transform LeftHandIK { set; get; }
    //左手IK位置 
    public abstract Animator AC { set; get; }
    //绑定的animation controller
    public abstract void Aim(Vector3 aim);
    //判断扳机动作
    public abstract void Trigger();
    //松开扳机
    public abstract void ReleaseTrigger();
    //子弹出膛

    public abstract void Fire();
    //上弹夹
    public abstract void Reload();

    //由PlayerFire在Update中调用
    public abstract void OnUpdateCallback();

    //由PlayerFire在FixedUpdate中调用
    public abstract void OnFixedUpdateCallback();

    //由PlayerFire在OnAnimatorIK中调用
    public abstract void OnAnimatorIKCallback();

    //上弹动作完毕后调用
    public abstract void ReloadDone();
}

