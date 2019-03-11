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

        //自身id为1
        public override int InventoryID
        {
            get
            {
                return 1;
            }
        }

        public override InventoryEnum.ItemType Type { get { return InventoryEnum.ItemType.Weapon; } }

        public override string InventoryName { get { return "AK47"; } }

        public override string InventoryImgDefault { get { return ""; } }

        public override bool IsStackable { get { return false; } }

        public override int StackMaxCount { get { return 1; } }

        public override bool IsTradable { get { return true; } }

        public override int Price { get { return 100; } }

        //使用弹药的物品id是10
        public override int AmmoItemId
        {
            get
            {
                return 10;
            }
        }

        private Transform muzzleFixed;

        public AK47(GameObject weaponObject, GameObject owner, Transform muzzleFixedPosition)
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
            muzzleFixed = muzzleFixedPosition;

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
            Action.OnFixedupdateHandler(muzzleFixed.position.y);
        }

        public override void OnUpdate()
        {
            Action.OnUpdateHandler();
        }
    }
}
