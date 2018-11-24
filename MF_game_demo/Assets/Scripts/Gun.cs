using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//具体的枪械来实现这个抽象数据类
public abstract class Gun
{
    //
    //以下为枪械的属性
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
    public abstract float CD { set; get; }
    //两发间隔
    public abstract float AimTime { set; get; }
    //瞄准时间、前摇
    public abstract float ReloadTime { set; get; }
    //上弹耗时
    public abstract int BulletCapacity { set; get; }
    //允许携带弹量
    public abstract GameObject Owner { set; get; }
    //拥有者

    //
    //以下为状态量
    protected abstract GunEnum.GunState GunState { set; get; }
    //枪械状态
    protected abstract bool IsTriggered { set; get; }
    //扳机状态
    protected abstract float AimTimeLeft { set; get; }
    //瞄准时间剩余
    protected abstract float CDLeft { set; get; }
    //下一发子弹将在几秒后射出
    public abstract int MagazineLeft { set; get; }
    //弹夹余量
    public abstract int BulletCapacityLeft { set; get; }
    //残弹余量
    protected abstract float ReloadTimeLeft { set; get; }
    //上弹还需多少时间

    //
    //其他属性
    protected abstract Vector3 AimDirection { set; get; }
    //枪口方向
    protected abstract GameObject GunObject { set; get; }
    //指向此枪
    protected abstract GameObject Muzzle { set; get; }
    //枪口物体
    protected abstract GameObject MuzzleFlare { set; get; }
    //枪焰
    protected abstract string BulletPrefabPath { set; get; }
    //子弹预设体路径
    protected abstract Transform LeftHandIK { set; get; }
    //左手IK位置 
    protected abstract Animator AC { set; get; }
    //绑定的animation controller

    //
    //以下为方法

    //Idling状态入口
    protected abstract void Idling();
    //Aiming状态入口
    protected abstract void Aiming();
    //Reloading状态入口
    protected abstract void Reloading();

    //以下三个方法执行状态转换 同时初始化相关状态量
    protected abstract void StateToIdling();
    protected abstract void StateToAiming();
    protected abstract void StateToReloading();

    //发射子弹
    protected abstract void Fire();

    //由PlayerFire在Update中调用
    public abstract void OnUpdateCallback();

    //由PlayerFire在FixedUpdate中调用，同时更新输入类状态量（其余状态量由其他相关方法处理），根据Gunstate进入不同的入口
    public abstract void OnFixedUpdateCallback();

    //由PlayerFire在OnAnimatorIK中调用
    public abstract void OnAnimatorIKCallback();

}

