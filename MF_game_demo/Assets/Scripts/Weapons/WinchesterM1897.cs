﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Weapons
{
    class WinchesterM1897 : Weapon
    {
        public override WeaponData Data { get; set; }

        public override GameObject WeaponObj { get; set; }
        public override GameObject Owner { get; set; }
        public override IWeaponAnimation Animation { get; set; }
        public override IWeaponBarrage Barrage { get; set; }
        public override IWeaponAction Action { get; set; }

        //独有属性
        public float SpearAngleMax { set; get; }
        public string BulletPath { set; get; }
        public string BulletImpactPath { set; get; }

        public int BulletCountPerFire { set; get; }
        public int BarrageAngle { set; get; }

        public WinchesterM1897(GameObject weaponObject, GameObject owner)
        {
            Data = new WeaponData();
            Data.AimTime = 0f;
            Data.BulletCapacity = 90;
            Data.BulletLeft = Data.BulletCapacity;
            Data.BulletSpeed = 30;
            Data.Damage = 10;
            Data.FireCd = 0.2f;
            Data.MagazineCapacity = 10;
            Data.MagazineLeft = Data.MagazineCapacity;
            Data.Name = "温切斯特M1897";
            Data.Rarity = GunEnum.Rarity.n;
            Data.ReloadTime = 3.0f;
            Data.Type = GunEnum.GunType.shotgun;

            SpearAngleMax = 3;
            BulletPath = "Prefabs/Bullets/Bullet_AKM";
            BulletImpactPath = "Prefabs/Bullets/Bullet_AKM_Impact";
            //喷一下，30度范围内发射5发子弹
            BulletCountPerFire = 5;
            BarrageAngle = 30;

            WeaponObj = weaponObject;

            Animation = new TwoHandGunAnimation(weaponObject, owner.GetComponent<Animator>());

            Barrage = new ShotgunBarrageFast(Data, BarrageAngle, BulletCountPerFire, SpearAngleMax, BulletPath, BulletImpactPath, Owner);

            Action = new SemiAutoRifileAction(this);
        }

        public override void OnAnimatorIK()
        {
            Action.OnAnimatorIKHandler();
        }

        public override void OnFixedUpdate()
        {
            Action.OnFixedupdateHandler();
        }

        public override void OnUpdate()
        {
            Action.OnUpdateHandler();
        }
    }
}