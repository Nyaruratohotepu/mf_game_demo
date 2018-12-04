using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Weapons
{
    public class BulletBase : MonoBehaviour
    {

        //共有属性
        //谁发出的弹药
        public GameObject SourceObject { set; get; }
        public float Damage { set; get; }
        public float Speed { set; get; }
        public float LifeTime { set; get; }
        //实现交给这个接口对象
        private BulletHandler bulletHandler;

        public void Init(GameObject sourceObject, float damage, float speed, float lifeTime, BulletHandler bulletHandler)
        {
            SourceObject = sourceObject;
            Damage = damage;
            Speed = speed;
            LifeTime = lifeTime;
            this.bulletHandler = bulletHandler;
            bulletHandler.SetHost(this);
        }
        void Update()
        {
            bulletHandler.UpdateHandler();
        }
        void FixedUpdate()
        {
            bulletHandler.FixedUpdateHandler();
        }
        public void DestroySelf()
        {
            Destroy(gameObject);
        }
    }

    //弹药逻辑处理接口，不同类型的弹药有不同的实现
    public interface BulletHandler
    {
        void SetHost(BulletBase host);
        //响应bullet的调用
        void UpdateHandler();
        void FixedUpdateHandler();
    }

    //射线判断的，不穿透的弹药
    public class NomalBulletHandler : BulletHandler
    {
        private BulletBase host;
        private GameObject targetObj;
        private string impactPath;
        private Vector3 direction;
        private Vector3 hitPosition;
        public NomalBulletHandler(Vector3 direction, Vector3 hitPosition, GameObject targetObject, string impactPath)
        {
            this.direction = direction.normalized;
            this.targetObj = targetObject;
            this.impactPath = impactPath;
            this.hitPosition = hitPosition;
        }

        void Attack()
        {
            //动画效果部分
            host.gameObject.GetComponent<ParticleSystem>().Stop();
            GameObject impact = GameObject.Instantiate(Resources.Load<GameObject>(impactPath));
            impact.transform.position = hitPosition;
            //调整火花方向还需修改
            impact.transform.Rotate(new Vector3(0, 180, 0), Space.Self);

            //音效部分
            //事件处理部分
            MonoBehaviour.print("HitBy:" + host.SourceObject);
        }

        void BulletHandler.FixedUpdateHandler()
        {
            //判断是否撞击
            host.LifeTime -= Time.deltaTime;

            //处理动画
            if (host.LifeTime > 0)
                host.gameObject.transform.position += (direction * host.Speed * Time.deltaTime);
            else
            {
                Attack();
                host.DestroySelf();
            }
        }

        void BulletHandler.SetHost(BulletBase host)
        {
            this.host = host;
        }

        void BulletHandler.UpdateHandler()
        {
            host.gameObject.transform.LookAt(hitPosition);
        }
    }
    //射线判断的，穿透的弹药
    public class SnipeBulletHandler : BulletHandler
    {
        void BulletHandler.FixedUpdateHandler()
        {
            throw new NotImplementedException();
        }

        void BulletHandler.SetHost(BulletBase host)
        {
            throw new NotImplementedException();
        }

        void BulletHandler.UpdateHandler()
        {
            throw new NotImplementedException();
        }
    }
    //自己碰撞判断，范围伤害的弹药
    public class RocketHandler : BulletHandler
    {
        void BulletHandler.FixedUpdateHandler()
        {
            throw new NotImplementedException();
        }

        void BulletHandler.SetHost(BulletBase host)
        {
            throw new NotImplementedException();
        }

        void BulletHandler.UpdateHandler()
        {
            throw new NotImplementedException();
        }
    }
    //自己碰撞判断，跟踪的，范围伤害弹药
    public class MissileHandler : BulletHandler
    {
        void BulletHandler.FixedUpdateHandler()
        {
            throw new NotImplementedException();
        }

        void BulletHandler.SetHost(BulletBase host)
        {
            throw new NotImplementedException();
        }

        void BulletHandler.UpdateHandler()
        {
            throw new NotImplementedException();
        }
    }

    //存储攻击信息
    public class DamageInfo
    {
        public readonly GameObject sourceObject;
        public readonly GameObject targetObject;
        public readonly float damageValue;
        public DamageInfo(GameObject sourceObject, GameObject targetObject, float damageValue)
        {
            this.sourceObject = sourceObject;
            this.targetObject = targetObject;
            this.damageValue = damageValue;
        }
    }

}
