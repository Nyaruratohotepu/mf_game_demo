﻿using Assets.Scripts.Weapons;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon
{
    //枪械基本数据
    public abstract WeaponData Data { set; get; }
    //绑定的游戏物体
    public abstract GameObject WeaponObj { set; get; }
    public abstract GameObject Owner { set; get; }
    public abstract IWeaponAnimation Animation { set; get; }

    public abstract IWeaponBarrage Barrage { set; get; }
    public abstract IWeaponAction Action { set; get; }

    public abstract void OnFixedUpdate();
    public abstract void OnUpdate();
    public abstract void OnAnimatorIK();
}
public class WeaponData
{

    public string Name { set; get; }

    public GunEnum.Rarity Rarity { set; get; }

    public GunEnum.GunType Type { set; get; }

    public float Damage { set; get; }

    public float BulletSpeed { set; get; }

    //射击间隔
    public float FireCd { set; get; }

    //抬手时间
    public float AimTime { set; get; }

    //上弹时间
    public float ReloadTime { set; get; }

    //弹夹容量
    public int MagazineCapacity { set; get; }

    //弹夹残弹量
    public int MagazineLeft { set; get; }

    //子弹容量
    public int BulletCapacity { set; get; }

    //残弹余量
    public int BulletLeft { set; get; }


}