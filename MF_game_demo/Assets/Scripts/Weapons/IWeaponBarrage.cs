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
        bool FireBarrage(Vector3 muzzlePoint, Vector3 target);
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

        public RifleBarrageFast(float damageSingleBullet, float bulletSpeed, float angleSpreadMax, string bulletPrefabPath, string bulletImpactPrefabPath, GameObject owner)
        {
            damage = damageSingleBullet;
            speed = bulletSpeed;
            maxSpreadAngle = angleSpreadMax;
            bulletPath = bulletPrefabPath;
            bulletImpactPath = bulletImpactPrefabPath;
            this.owner = owner;
        }


        bool IWeaponBarrage.FireBarrage(Vector3 muzzlePoint, Vector3 targetPosition)
        {
            float spreadAngle = UnityEngine.Random.Range(-1f, 1f) * maxSpreadAngle;

            //射击方向是标准方向绕y轴旋转一个随机角度
            Vector3 direction = (Quaternion.AngleAxis(spreadAngle, Vector3.up) * (targetPosition - muzzlePoint)).normalized;

            RaycastHit hit = new RaycastHit();
            if (Physics.Raycast(muzzlePoint, direction, out hit))
            {
                Debug.DrawLine(muzzlePoint, hit.point, Color.red, 3);
                //hitTime秒后子弹打击到物体
                float hitTime = (hit.point - muzzlePoint).magnitude / speed;

                if (hit.collider != null)
                {
                    MonoBehaviour.print("HitObj:" + hit.collider.gameObject);
                    GameObject bullet = GameObject.Instantiate(Resources.Load<GameObject>(bulletPath));
                    bullet.transform.position = muzzlePoint;
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
}
