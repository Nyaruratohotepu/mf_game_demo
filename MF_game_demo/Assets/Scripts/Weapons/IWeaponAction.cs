using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Weapons
{
    public interface IWeaponAction
    {
        //指定武器击发方式，调用弹幕模块
        void OnUpdateHandler();
        void OnFixedupdateHandler(float shootHeight);
        void OnAnimatorIKHandler();
    }

    //类似于全自动步枪的行为
    public class FullAutoRifileAction : IWeaponAction
    {
        //控制的枪械
        private Weapon weapon;
        //下一发时间
        private float nextFire;
        private float reloadTimeLeft;
        //枪械状态机状态
        private GunEnum.GunState state;
        //扳机是否被扣动
        private bool isTrigger;
        Vector3 target;
        Transform muzzle;

        public FullAutoRifileAction(Weapon host)
        {
            this.weapon = host;
            nextFire = host.Data.AimTime;
            reloadTimeLeft = weapon.Data.ReloadTime;
            state = GunEnum.GunState.Idling;
            isTrigger = false;
            muzzle = weapon.WeaponObj.transform.Find("Muzzle");
        }

        void IWeaponAction.OnFixedupdateHandler(float shootHeight)
        {
            weapon.Animation.FixedUpdateHandler();

            isTrigger = Input.GetMouseButton(0);

            if (Input.GetButtonDown("Reload"))
                state = GunEnum.GunState.Reloading;

            switch (state)
            {
                case GunEnum.GunState.Idling:
                    Idling();
                    break;
                case GunEnum.GunState.Aiming:
                    Aiming(shootHeight);
                    break;
                case GunEnum.GunState.Reloading:
                    Reloading();
                    break;
            }
        }
        private void Idling()
        {
            if (isTrigger)
                nextFire -= Time.deltaTime;

            if (nextFire <= 0)
                StateToAiming();
            //瞄准完毕下一帧调用Aiming()
        }
        private void Aiming(float shootHeight)
        {
            //松扳机
            if (!isTrigger)
            {
                StateToIdling();
                return;
            }



            RaycastHit hit = new RaycastHit();
            if (IOTool.GetMousePosition(out hit))
            {
                target = hit.point;
                target.y = shootHeight;
                weapon.Animation.GunAim(target);
            }
            if (weapon.Data.MagazineLeft <= 0)
            {
                if (weapon.Data.BulletLeft <= 0)
                {
                    //子弹打光
                    //播放空膛音效
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
                nextFire -= Time.deltaTime;
                if (nextFire <= 0)
                {
                    //以固定高度发射弹幕避免碰撞失效
                    Vector3 muzzleFixHeight = muzzle.position;
                    muzzleFixHeight.y = shootHeight;
                    target.y = shootHeight;
                    //弹幕发射成功
                    if (weapon.Barrage.FireBarrage(muzzleFixHeight, target))
                    {
                        weapon.Data.MagazineLeft--;
                        weapon.Animation.GunFire();
                        StateToAiming();
                    }

                }
            }

        }
        private void Reloading()
        {
            reloadTimeLeft -= Time.deltaTime;
            if (reloadTimeLeft <= 0)
            {
                int magazineNeed = weapon.Data.MagazineCapacity - weapon.Data.MagazineLeft;
                //尽量多上弹
                int bulletCount = magazineNeed > weapon.Data.BulletLeft ? weapon.Data.BulletLeft : magazineNeed;
                weapon.Data.MagazineLeft += bulletCount;
                weapon.Data.BulletLeft -= bulletCount;

                //状态转移
                if (isTrigger) StateToAiming();
                else StateToIdling();
            }
        }
        private void StateToIdling()
        {
            state = GunEnum.GunState.Idling;
            nextFire = weapon.Data.AimTime;
            weapon.Animation.AnimatorTo(state);
        }
        private void StateToAiming()
        {
            state = GunEnum.GunState.Aiming;
            nextFire = weapon.Data.FireCd;
            weapon.Animation.AnimatorTo(state);
        }
        private void StateToReloading()
        {
            state = GunEnum.GunState.Reloading;
            reloadTimeLeft = weapon.Data.ReloadTime;
            weapon.Animation.AnimatorTo(state);
        }
        void IWeaponAction.OnUpdateHandler()
        {
            weapon.Animation.UpdateHandler();
        }
        void IWeaponAction.OnAnimatorIKHandler()
        {
            weapon.Animation.HandIKHandler();
        }
    }

    //类似于半自动步枪的行为
    public class SemiAutoRifileAction : IWeaponAction
    {
        //控制的枪械
        private Weapon weapon;
        //下一发时间
        private float nextFire;
        private float reloadTimeLeft;
        //枪械状态机状态
        private GunEnum.GunState state;
        //扳机是否被扣动
        private bool isTrigger;
        Vector3 target;
        Transform muzzle;

        public SemiAutoRifileAction(Weapon host)
        {
            this.weapon = host;
            //半自动枪无需前摇
            nextFire = 0;
            reloadTimeLeft = weapon.Data.ReloadTime;
            state = GunEnum.GunState.Idling;
            isTrigger = false;
            muzzle = weapon.WeaponObj.transform.Find("Muzzle");
        }

        void IWeaponAction.OnFixedupdateHandler(float shootHeight)
        {
            weapon.Animation.FixedUpdateHandler();


            isTrigger = Input.GetMouseButtonDown(0);

            if (Input.GetButtonDown("Reload"))
                state = GunEnum.GunState.Reloading;

            switch (state)
            {
                case GunEnum.GunState.Idling:
                    Idling();
                    break;
                case GunEnum.GunState.Aiming:
                    Aiming(shootHeight);
                    break;
                case GunEnum.GunState.Reloading:
                    Reloading();
                    break;
            }
        }
        private void Idling()
        {
            if (nextFire > 0)
                nextFire -= Time.deltaTime;
            else if (isTrigger)
                StateToAiming();
        }
        private void Aiming(float shootHeight)
        {
            RaycastHit hit = new RaycastHit();
            if (IOTool.GetMousePosition(out hit))
            {
                target = hit.point;
                target.y = shootHeight;
                weapon.Animation.GunAim(target);
            }
            if (weapon.Data.MagazineLeft <= 0)
            {
                if (weapon.Data.BulletLeft <= 0)
                {
                    //子弹打光
                    //播放空膛音效
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
                //以固定高度发射弹幕避免碰撞失效
                Vector3 muzzleFixHeight = muzzle.position;
                muzzleFixHeight.y = shootHeight;
                target.y = shootHeight;
                //弹幕发射成功
                if (weapon.Barrage.FireBarrage(muzzleFixHeight, target))
                {
                    weapon.Data.MagazineLeft--;
                    weapon.Animation.GunFire();
                    //重置cd
                    nextFire = weapon.Data.FireCd;
                    
                }
                else
                {
                    MonoBehaviour.print("发射失败");
                }
                StateToIdling();
            }

        }
        private void Reloading()
        {
            reloadTimeLeft -= Time.deltaTime;
            if (reloadTimeLeft <= 0)
            {
                int magazineNeed = weapon.Data.MagazineCapacity - weapon.Data.MagazineLeft;
                //尽量多上弹
                int bulletCount = magazineNeed > weapon.Data.BulletLeft ? weapon.Data.BulletLeft : magazineNeed;
                weapon.Data.MagazineLeft += bulletCount;
                weapon.Data.BulletLeft -= bulletCount;

                //状态转移
                StateToIdling();
            }
        }
        private void StateToIdling()
        {
            state = GunEnum.GunState.Idling;
            weapon.Animation.AnimatorTo(state);
        }
        private void StateToAiming()
        {
            state = GunEnum.GunState.Aiming;
            weapon.Animation.AnimatorTo(state);
        }
        private void StateToReloading()
        {
            state = GunEnum.GunState.Reloading;
            reloadTimeLeft = weapon.Data.ReloadTime;
            weapon.Animation.AnimatorTo(state);
        }
        void IWeaponAction.OnUpdateHandler()
        {
            weapon.Animation.UpdateHandler();
        }
        void IWeaponAction.OnAnimatorIKHandler()
        {
            weapon.Animation.HandIKHandler();
        }
    }

}
