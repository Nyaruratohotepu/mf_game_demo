using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class MeleeMonster : Monster  //近战怪物拥有相同的逻辑，此层处理数值、移动等近战怪物共有逻辑
{
    public override int Id { get; set; }
    public override int Hp { get; set; }
    public override int Atk { get; set; }
    public override float AtkBetween { get; set; }
    public override float AtkBetweenLeft { get; set; }
    public override string Name { get; set; }
    public override string ChineseName { get; set; }
    public override string Introduction { get; set; }
    public override float MoveSpeed { get; set; }
    public override MonsterEnum.MonsterState NowState { get; set; }
    public override GameObject Target { get; set; }
    public override float HuntRange { get; set; }
    public override Manager GameManager { get; set; }
    public override GameObject Player { get; set; }
    public override GameObject Self { get; set; }
    public override CharacterController CC { get; set; }

    //以下为近战怪物的特有属性
    //攻击范围
    public float AtkRange { get; set; }

    public override void Attack()
    {
        GameManager.GetDamageManager().TryAttackPlayer(Atk);
        AtkBetweenLeft = AtkBetween;
        //恢复cd
    }

    public override void Die()
    {

    }

    public override void OnFixedUpdateCallback()
    {

    }

    public override void OnUpdateCallback()
    {
        //攻击读秒
        if (AtkBetweenLeft > 0) AtkBetweenLeft -= Time.deltaTime;

        Vector3 playerPosition = Player.transform.position;
        if (Vector3.Distance(Self.transform.position, playerPosition) < HuntRange)
        {
            if (Vector3.Distance(Self.transform.position, playerPosition) < AtkRange)
            {
                //玩家在攻击范围内
                //攻击冷却完成，攻击玩家
                if (AtkBetweenLeft <= 0) Attack();
            }
            else
            {
                //玩家不在攻击范围内，追逐玩家
                //此处要用寻路算法重写
                playerPosition.y = 0;
                Self.transform.LookAt(playerPosition);
                CC.SimpleMove(MoveSpeed * Self.transform.forward);
            }

        }
    }


}

