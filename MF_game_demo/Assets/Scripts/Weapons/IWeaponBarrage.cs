using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Weapons
{
    //弹幕接口，用于存储弹幕类型，发射子弹
    public interface IWeaponBarrage
    {
        //发射弹幕，返回是否成功射出
        bool FireBarrage(Vector3 muzzlePosition, Vector3 targetPosition);
    }

    //单发高速步枪弹幕（使用射线检测）
    public class RifleBarrageFast : IWeaponBarrage
    {
        //单发子弹伤害
        private float damage;

        //子弹每秒移动距离
        private float speed;

        //水平散布范围，单位：度
        //子弹发射时会离轴心偏离最多maxSpreadAngle度
        private float maxSpreadAngle;

        //子弹预制体路径
        private string bulletPath;
        //子弹火花路径
        private string bulletImpactPath;
        private GameObject owner;

        public RifleBarrageFast(WeaponData data, float angleSpreadMax, string bulletPrefabPath, string bulletImpactPrefabPath, GameObject owner)
        {
            damage = data.Damage;
            speed = data.BulletSpeed;
            maxSpreadAngle = angleSpreadMax;
            bulletPath = bulletPrefabPath;
            bulletImpactPath = bulletImpactPrefabPath;
            this.owner = owner;
        }


        bool IWeaponBarrage.FireBarrage(Vector3 muzzlePosition, Vector3 targetPosition)
        {
            float spreadAngle = UnityEngine.Random.Range(-1f, 1f) * maxSpreadAngle;

            //射击方向是标准方向绕y轴旋转一个随机角度
            Vector3 direction = (Quaternion.AngleAxis(spreadAngle, Vector3.up) * (targetPosition - muzzlePosition)).normalized;

            RaycastHit hit = new RaycastHit();
            if (Physics.Raycast(muzzlePosition, direction, out hit))
            {
                Debug.DrawLine(muzzlePosition, hit.point, Color.red, 3);
                //hitTime秒后子弹打击到物体
                float hitTime = (hit.point - muzzlePosition).magnitude / speed;

                if (hit.collider != null)
                {
                    MonoBehaviour.print("HitObj:" + hit.collider.gameObject);
                    GameObject bullet = GameObject.Instantiate(Resources.Load<GameObject>(bulletPath));
                    bullet.transform.position = muzzlePosition;
                    bullet.AddComponent<BulletBase>();
                    bullet.GetComponent<BulletBase>().Init(owner, damage, speed, hitTime, new NomalBulletHandler(direction, hit.point, hit.collider.gameObject, bulletImpactPath));

                    return true;
                }

                else
                {

                    return false;
                }

            }

            else
            {

                return false;
            }
        }
    }

    //霰弹枪弹幕，扇形水平散开
    public class ShotgunBarrageFast : IWeaponBarrage
    {
        //每发子弹的伤害
        private float damage;

        //子弹运动距离每秒
        private float speed;

        //无误差情况下弹幕散开的扇形角度
        private float barrageAngle;

        //每一发霰弹散射出多少弹丸
        private int bulletCount;

        //水平散布范围，单位：度
        //子弹发射时会离轴心偏离最多maxSpreadAngle度
        private float maxSpreadAngle;

        //子弹预制体路径
        private string bulletPath;
        //子弹火花路径
        private string bulletImpactPath;
        private GameObject owner;

        public ShotgunBarrageFast(WeaponData data, float barrageAngle, int barrageBulletCount, float maxSpreadAngle, string bulletPrefabPath, string bulletImpactPrefabPath, GameObject owner)
        {
            this.damage = data.Damage;
            this.speed = data.BulletSpeed;
            this.barrageAngle = barrageAngle;
            this.bulletCount = barrageBulletCount;
            this.maxSpreadAngle = maxSpreadAngle;
            this.bulletPath = bulletPrefabPath;
            this.bulletImpactPath = bulletImpactPrefabPath;
            this.owner = owner;
        }

        bool IWeaponBarrage.FireBarrage(Vector3 muzzlePosition, Vector3 targetPosition)
        {
            //directions存储每一枚子弹发射方向
            //计算方法：先算出发射方向相对于直瞄方向的偏移角度，然后旋转直瞄方向
            Vector3[] directions = new Vector3[bulletCount];
            Vector3 directAngle = (targetPosition - muzzlePosition).normalized;
            float leftAngle = barrageAngle / (-2);
            float devAngle = barrageAngle / (bulletCount - 1);


            for (int i = 0; i < bulletCount; i++)
            {
                //计算偏移角度
                float spreadAngle = leftAngle + devAngle * i + UnityEngine.Random.Range(-1f, 1f) * maxSpreadAngle;
                //旋转
                directions[i] = Quaternion.AngleAxis(spreadAngle, Vector3.up) * directAngle;
            }

            RaycastHit hit = new RaycastHit();
            for (int i = 0; i < bulletCount; i++)
            {
                if (Physics.Raycast(muzzlePosition, directions[i], out hit))
                {
                    Debug.DrawLine(muzzlePosition, hit.point, Color.red, 3);
                    //hitTime秒后子弹打击到物体
                    float hitTime = (hit.point - muzzlePosition).magnitude / speed;

                    if (hit.collider != null)
                    {
                        MonoBehaviour.print("HitObj:" + hit.collider.gameObject);
                        GameObject bullet = GameObject.Instantiate(Resources.Load<GameObject>(bulletPath));
                        bullet.transform.position = muzzlePosition;
                        bullet.AddComponent<BulletBase>();
                        bullet.GetComponent<BulletBase>().Init(owner, damage, speed, hitTime, new NomalBulletHandler(directions[i], hit.point, hit.collider.gameObject, bulletImpactPath));
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            return true;
        }


    }
}
