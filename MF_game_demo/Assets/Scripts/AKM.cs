using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    class AKM : Gun
    {
        //抽象类属性
        public override GunEnum.GunType GunType { get; set; }
        public override GunEnum.FireType FireType { get; set; }
        public override GunEnum.Rarity Rarity { get; set; }
        public override string GunName { get; set; }
        public override int Damage { get; set; }
        public override float DamageRange { get; set; }
        public override int Magazine { get; set; }
        public override float CD { get; set; }
        public override float AimTime { get; set; }
        public override int BulletCapacity { get; set; }
        public override float ReloadTime { get; set; }
        public override GameObject Owner { get; set; }
        public override int MagazineLeft { get; set; }
        public override int BulletCapacityLeft { get; set; }
        protected override GunEnum.GunState GunState { get; set; }
        protected override bool IsTriggered { get; set; }
        protected override float AimTimeLeft { get; set; }
        protected override float CDLeft { get; set; }
        protected override float ReloadTimeLeft { get; set; }
        protected override Vector3 AimDirection { get; set; }
        protected override GameObject GunObject { get; set; }
        protected override GameObject Muzzle { get; set; }
        protected override GameObject MuzzleFlare { get; set; }
        protected override string BulletPrefabPath { get; set; }
        protected override Transform LeftHandIK { get; set; }
        protected override Animator AC { get; set; }

        //以下为AKM独有内容
        //枪口闪光
        private Light muzzleLight;
        //枪口闪光持续时间
        private float muzzleLightTime;
        private float muzzleLightTimeLeft;

        public AKM(GameObject gunObj, GameObject ownerObj)
        {
            //默认构造函数
            //初始化抽象类属性
            GunType = GunEnum.GunType.assault_rifle;
            FireType = GunEnum.FireType.automatic;
            Rarity = GunEnum.Rarity.r;
            GunName = "AKM";
            Damage = 50;
            DamageRange = 70f;
            Magazine = 150;
            CD = 0.1f;
            AimTime = 0.1f;
            BulletCapacity = 360;
            ReloadTime = 3.0f;
            Owner = ownerObj;

            MagazineLeft = Magazine;
            BulletCapacityLeft = BulletCapacity;
            GunState = GunEnum.GunState.Idling;
            IsTriggered = false;
            AimTimeLeft = AimTime;
            ReloadTimeLeft = ReloadTime;
            CDLeft = 0;

            GunObject = gunObj;
            Muzzle = GunObject.transform.Find("Muzzle").gameObject;
            MuzzleFlare = Muzzle.transform.Find("MuzzleFlare").gameObject;
            BulletPrefabPath = "Prefabs/Bullets/Bullet_AKM";
            LeftHandIK = GunObject.transform.Find("LeftHandIK");
            AC = ownerObj.GetComponent<Animator>();

            //初始化AKM独有内容
            muzzleLight = Muzzle.transform.Find("MuzzleLight").gameObject.GetComponent<Light>();
            muzzleLightTime = muzzleLightTimeLeft = 0.03f;
        }
        public override void OnAnimatorIKCallback()
        {
            //除非换弹，左手握护木
            if (!AC.GetCurrentAnimatorStateInfo(1).IsName("Reload"))
            {
                AC.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1);
                AC.SetIKPosition(AvatarIKGoal.LeftHand, LeftHandIK.position);
                AC.SetIKRotation(AvatarIKGoal.LeftHand, LeftHandIK.rotation);
            }
        }

        public override void OnFixedUpdateCallback()
        {
            if (Input.GetMouseButton(0))
                IsTriggered = true;
            else
                IsTriggered = false;

            if (Input.GetButtonDown("Reload"))
                GunState = GunEnum.GunState.Reloading;

            switch (GunState)
            {
                case GunEnum.GunState.Idling:
                    Idling();
                    break;
                case GunEnum.GunState.Aiming:
                    Aiming();
                    break;
                case GunEnum.GunState.Reloading:
                    Reloading();
                    break;
            }

            //UI组件计时
            if (muzzleLightTimeLeft > 0)
            {
                muzzleLightTimeLeft -= Time.deltaTime;
                if (muzzleLightTimeLeft <= 0)
                    muzzleLight.intensity = 0;
            }
        }

        public override void OnUpdateCallback()
        {

        }

        protected override void Idling()
        {
            if (IsTriggered)
                AimTimeLeft -= Time.deltaTime;

            if (AimTimeLeft <= 0)
                StateToAiming();
            //瞄准完毕下一帧调用Aiming()
        }

        protected override void Aiming()
        {
            RaycastHit hit = new RaycastHit();
            if (IOTool.GetMousePosition(out hit))
                GunObject.transform.LookAt(hit.point);
            if (!IsTriggered)
            {
                //松扳机
                StateToIdling();
            }
            else if (MagazineLeft <= 0)
            {
                if (BulletCapacityLeft <= 0)
                {
                    //子弹打光
                    //此处播放空膛声音
                    return;
                }
                else
                {
                    //装弹
                    StateToReloading();
                    return;
                }
            }
            else
            {
                CDLeft -= Time.deltaTime;
                if (CDLeft <= 0)
                    Fire();
            }



        }

        protected override void Fire()
        {

            //每发射子弹一次，播放动画一次
            AC.Play("Fire", 1, 0);
            //枪口闪光
            MuzzleFlare.GetComponent<ParticleSystem>().Play();
            muzzleLight.intensity = 5;
            muzzleLightTimeLeft = muzzleLightTime;

            MagazineLeft--;


            //发射子弹
            GameObject bullet = GameObject.Instantiate(Resources.Load<GameObject>(BulletPrefabPath));
            bullet.transform.position = Muzzle.transform.position;
            Bullet_AKM bullet_AKM = bullet.GetComponent<Bullet_AKM>();
            RaycastHit hit = new RaycastHit();
            if (IOTool.GetMousePosition(out hit) && (hit.transform.gameObject != Owner))
            {
                Vector3 target = hit.point;
                target.y = Muzzle.transform.position.y;
                bullet_AKM.Init(Muzzle.transform.position, target);
            }
            else
            {
                bullet_AKM.Init(Muzzle.transform.position, Owner.transform.position + Owner.transform.forward);
            }
            bullet_AKM.Fired = true;

            MonoBehaviour.print(MagazineLeft + "Fire!");

            StateToAiming();
        }

        protected override void Reloading()
        {
            ReloadTimeLeft -= Time.deltaTime;
            if (ReloadTimeLeft <= 0)
            {
                int magazineNeed = Magazine - MagazineLeft;
                int bulletCount = magazineNeed > BulletCapacityLeft ? BulletCapacityLeft : magazineNeed;
                MagazineLeft += bulletCount;
                BulletCapacityLeft -= bulletCount;

                //状态转移
                if (IsTriggered) StateToAiming();
                else StateToIdling();
            }
        }

        protected override void StateToIdling()
        {
            GunState = GunEnum.GunState.Idling;
            AimTimeLeft = AimTime;
            AC.SetBool("isFiring", false);
            AC.SetBool("isReloading", false);
        }

        protected override void StateToAiming()
        {
            GunState = GunEnum.GunState.Aiming;
            CDLeft = CD;
            AC.SetBool("isFiring", true);
            AC.SetBool("isReloading", false);
        }

        protected override void StateToReloading()
        {
            GunState = GunEnum.GunState.Reloading;
            ReloadTimeLeft = ReloadTime;
            AC.SetBool("isReloading", true);
            AC.SetBool("isFiring", false);
        }
    }
}
