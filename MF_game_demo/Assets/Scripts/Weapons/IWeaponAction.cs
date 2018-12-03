using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEditor.Animations;
using UnityEngine;

namespace Assets.Scripts.Weapons
{
    //此接口定义模型和动画相关行为，对应单手、双手等动作相关的区别
    public interface IWeaponAnimation
    {
        //绑定IK
        void HandIKHandler();

        //武器瞄准（枪口对准鼠标）
        void GunAim(Vector3 target);

        //武器开火时的动画响应
        void GunFire();

        //枪械状态转换时的动画响应
        void AnimatorTo(GunEnum.GunState state);
        void UpdateHandler();
        void FixedUpdateHandler();
    }

    //双手武器动作类
    public class TwoHandGunAction : IWeaponAnimation
    {
        private GameObject gunObj;

        private Transform leftHandIK;
        private Animator ownerAnimator;

        //处理枪口闪光的变量
        private ParticleSystem muzzleFlare;
        private Light muzzleLight;
        private float muzzleLightTime;
        private float muzzleLightTimeLeft;

        //默认对应的动画控制器存储位置
        private string defaultAnimatorPath = "Animation/player.controller";

        //构造函数依赖注入
        TwoHandGunAction(GameObject gunObject, Animator ownerAnimator)
        {
            gunObj = gunObject;
            leftHandIK = gunObj.transform.Find("LeftHandIK");
            this.ownerAnimator = ownerAnimator;
            //将动画设定为双手武器的动画控制器
            ownerAnimator.runtimeAnimatorController = (RuntimeAnimatorController)Resources.Load(defaultAnimatorPath);

            Transform muzzle = gunObj.transform.Find("Muzzle");
            muzzleFlare = muzzle.Find("MuzzleFlare").gameObject.GetComponent<ParticleSystem>();
            muzzleLight = muzzle.Find("MuzzleLight").gameObject.GetComponent<Light>();
            //枪口火焰闪光时间
            muzzleLightTime = muzzleLightTimeLeft = 0.03f;
        }

        void IWeaponAnimation.GunAim(Vector3 target)
        {
            gunObj.transform.LookAt(target);
        }

        void IWeaponAnimation.GunFire()
        {
            ownerAnimator.Play("Fire", 1, 0);
            muzzleFlare.Play();
            muzzleLight.intensity = 5;
            muzzleLightTimeLeft = muzzleLightTime;
        }

        void IWeaponAnimation.HandIKHandler()
        {
            //除非换弹，左手握护木
            if (!ownerAnimator.GetCurrentAnimatorStateInfo(1).IsName("Reload"))
            {
                ownerAnimator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1);
                ownerAnimator.SetIKPosition(AvatarIKGoal.LeftHand, leftHandIK.position);
                ownerAnimator.SetIKRotation(AvatarIKGoal.LeftHand, leftHandIK.rotation);
            }
        }

        void IWeaponAnimation.AnimatorTo(GunEnum.GunState state)
        {
            switch (state)
            {
                case GunEnum.GunState.Aiming:
                    ownerAnimator.SetBool("isFiring", true);
                    ownerAnimator.SetBool("isReloading", false);
                    break;
                case GunEnum.GunState.Idling:
                    ownerAnimator.SetBool("isFiring", false);
                    ownerAnimator.SetBool("isReloading", false);
                    break;
                case GunEnum.GunState.Reloading:
                    ownerAnimator.SetBool("isReloading", true);
                    ownerAnimator.SetBool("isFiring", false);
                    break;
                default:
                    break;
            }

        }

        //动画处理放在update中
        void IWeaponAnimation.UpdateHandler()
        {
            if (muzzleLightTimeLeft > 0)
            {
                muzzleLightTimeLeft -= Time.deltaTime;
                if (muzzleLightTimeLeft <= 0)
                    muzzleLight.intensity = 0;
            }
        }

        void IWeaponAnimation.FixedUpdateHandler()
        {

        }
    }
}
