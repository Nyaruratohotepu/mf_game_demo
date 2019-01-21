using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Weapons
{
    class AK47 : Weapon
    {
        public override WeaponData Data { set; get; }
        public override GameObject WeaponObj { set; get; }
        public override GameObject Owner { set; get; }
        public override IWeaponAnimation Animation { set; get; }
        public override IWeaponBarrage Barrage { set; get; }
        public override IWeaponAction Action { set; get; }

        //独有属性
        public float SpearAngleMax { set; get; }
        public string BulletPath { set; get; }
        public string BulletImpactPath { set; get; }

        public AK47(GameObject weaponObject, GameObject owner)
        {
            Data = new WeaponData();
            Data.AimTime = 0.1f;
            Data.BulletCapacity = 360;
            Data.BulletLeft = Data.BulletCapacity;
            Data.BulletSpeed = 30;
            Data.Damage = 20;
            Data.FireCd = 0.1f;
            Data.MagazineCapacity = 50;
            Data.MagazineLeft = Data.MagazineCapacity;
            Data.Name = "AK47";
            Data.Rarity = GunEnum.Rarity.n;
            Data.ReloadTime = 3.0f;
            Data.Type = GunEnum.GunType.assault_rifle;


            SpearAngleMax = 5;
            BulletPath = "Prefabs/Bullets/Bullet_AKM";
            BulletImpactPath = "Prefabs/Bullets/Bullet_AKM_Impact";

            WeaponObj = weaponObject;

            Animation = new TwoHandGunAnimation(weaponObject, owner.GetComponent<Animator>());

            Barrage = new RifleBarrageFast(Data, SpearAngleMax, BulletPath, BulletImpactPath, owner);

            Action = new FullAutoRifileAction(this);
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
