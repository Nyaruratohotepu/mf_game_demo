using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AKM : Gun
{
    public override GunEnum.GunType GunType { set; get; }
    public override GunEnum.FireType FireType { set; get; }
    public override GunEnum.Rarity Rarity { set; get; }
    public override string GunName { set; get; }
    public override int Damage { set; get; }
    public override float DamageRange { set; get; }
    public override int Magazine { set; get; }
    public override int MagazineLeft { set; get; }
    public override float Cd { set; get; }
    public override float AimTime { set; get; }
    public override bool IsTriggerPulled { set; get; }
    public override float NextFireTime { set; get; }
    public override Vector3 AimDirection { set; get; }
    public override GameObject GunObject { set; get; }
    public override GameObject Muzzle { set; get; }
    public override GameObject MuzzleFlare { set; get; }
    public override GameObject Owner { set; get; }
    public override string BulletPrefabPath { set; get; }

    public override Transform LeftHandIK { set; get; }
    public override Animator AC { set; get; }
    //public override ParticleSystem Bullet { set; get; }

    //以下为AKM特有属性
    private LineRenderer aimPath;
    private Vector3 direction;
    private Light muzzleLight;
    private float muzzleLightTime;
    private float muzzleLightTimeLeft;
    //构造函数内对其赋值
    public AKM(GameObject gun, GameObject owner)
    {
        GunType = GunEnum.GunType.assault_rifle;
        FireType = GunEnum.FireType.automatic;
        Rarity = GunEnum.Rarity.r;
        GunName = "AKM";
        Damage = 50;
        DamageRange = 70f;
        Magazine = 150;
        MagazineLeft = Magazine;
        Cd = 0.1f;
        AimTime = 0f;
        NextFireTime = AimTime;
        IsTriggerPulled = false;
        GunObject = gun;
        Owner = owner;
        Muzzle = GunObject.transform.Find("Muzzle").gameObject;
        LeftHandIK = GunObject.transform.Find("LeftHandIK");
        MuzzleFlare = Muzzle.transform.Find("MuzzleFlare").gameObject;
        muzzleLight = Muzzle.transform.Find("MuzzleLight").gameObject.GetComponent<Light>();
        muzzleLightTime = muzzleLightTimeLeft = 0.03f;
        //Asset目录下创建一个名为Resources的文件夹（系统命名规范），此处输入后面的路径
        //不能加后缀名
        BulletPrefabPath = "Prefabs/Bullets/Bullet_AKM";
        AC = owner.GetComponent<Animator>();
        aimPath = Muzzle.GetComponent<LineRenderer>();
        //子弹轨迹，LineRenderer必须绑定到物体上才有效

        //Bullet = Muzzle.GetComponent<ParticleSystem>();

    }

    public override void OnUpdateCallback()
    {
        RaycastHit mouse;
        if (IOTool.GetMousePosition(out mouse))
        {
            direction = mouse.point;
            direction.y = Owner.transform.position.y;
            Aim(direction);
            Trigger();


        }

        if (Input.GetButtonDown("Reload") && (!AC.GetCurrentAnimatorStateInfo(1).IsName("Reload")))
        {
            Reload();
        }
    }

    public override void OnFixedUpdateCallback()
    {

    }

    public override void Aim(Vector3 aim)
    {
        //显示瞄准方向
        aimPath.SetPositions(new Vector3[] { Owner.transform.position, aim });
        aimPath.startColor = Color.blue;
        aimPath.endColor = Color.blue;
        aimPath.enabled = true;
        aim.y = Muzzle.transform.position.y;
        Muzzle.transform.forward = aim - Muzzle.transform.position;

    }
    //扣动扳机时每帧调用一次，负责处理时间
    public override void Trigger()
    {
        if (muzzleLightTime > 0)
        {
            muzzleLightTimeLeft -= Time.deltaTime;
            if (muzzleLightTimeLeft <= 0) muzzleLight.intensity = 0;
        }
        if (Input.GetMouseButton(0))
        {
            IsTriggerPulled = true;
            NextFireTime -= Time.deltaTime;
            if (NextFireTime <= 0) Fire();
        }
        else if (IsTriggerPulled)
            ReleaseTrigger();



        //其余情况无操作
    }

    //发射一枚子弹
    public override void Fire()
    {
        AC.SetBool("isFiring", true);
        if (MagazineLeft > 0)
        {

            //每发射子弹一次，播放动画一次
            AC.Play("Fire", 1, 0);
            //枪口闪光
            MuzzleFlare.GetComponent<ParticleSystem>().Play();
            muzzleLight.intensity = 5;
            muzzleLightTimeLeft = muzzleLightTime;

            //重新计算cd
            NextFireTime = Cd;

            MagazineLeft--;

            //Bullet.Play();
            //发射子弹
            GameObject bullet = GameObject.Instantiate(Resources.Load<GameObject>(BulletPrefabPath));

            bullet.transform.position = Muzzle.transform.position;

            Bullet_AKM bullet_AKM = bullet.GetComponent<Bullet_AKM>();

            Vector3 target = direction;
            target.y = Muzzle.transform.position.y;
            bullet_AKM.Target = target;
            bullet_AKM.direction = (target - bullet.transform.position).normalized;
            bullet_AKM.Fired = true;



            MonoBehaviour.print(MagazineLeft + "Fire!");
        }
        else ReleaseTrigger();
    }

    //上子弹
    public override void Reload()
    {

        //上弹需要先松开扳机
        ReleaseTrigger();
        AC.SetTrigger("trReload");
        MonoBehaviour.print("reloading");

        //开始检测上弹动作是否完成的协程，该协程会在上弹动作完成后调用ReloadDone
        Owner.GetComponent<PlayerFire>().StartCoroutine(WaitReloadDone());

    }

    //上弹动画完毕后由动画控制器回调
    public override void ReloadDone()
    {
        Owner.GetComponent<PlayerFire>().StopCoroutine(WaitReloadDone());
        MagazineLeft = Magazine;
        MonoBehaviour.print("Reload Done");
    }


    //负责时间归位
    public override void ReleaseTrigger()
    {
        AC.SetBool("isFiring", false);
        IsTriggerPulled = false;
        NextFireTime = AimTime;
    }


    public override void OnAnimatorIKCallback()
    {
        if (!AC.GetCurrentAnimatorStateInfo(1).IsName("Reload"))
        {
            AC.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1);
            AC.SetIKPosition(AvatarIKGoal.LeftHand, LeftHandIK.position);
            AC.SetIKRotation(AvatarIKGoal.LeftHand, LeftHandIK.rotation);
        }
    }

    public IEnumerator WaitReloadDone()
    {

        yield return null;
        if ((AC.GetCurrentAnimatorStateInfo(1).IsName("Reload")) && (AC.GetCurrentAnimatorStateInfo(1).normalizedTime >= 0.8f))
        {
            ReloadDone();   //执行完后就不会继续执行了
        }
        else
            Owner.GetComponent<PlayerFire>().StartCoroutine(WaitReloadDone());
        //我 调 我 自 己
        //相当于从头循环
    }


}


