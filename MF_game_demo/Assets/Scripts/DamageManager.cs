using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class DamageManager
{
    public PlayerHP Player { set; get; }
    public GameObject PlayerObject;

    //返回是否攻击成功
    public bool TryAttackPlayer(int damage)
    {
        //若此时player不是无敌状态
        Player.HP -= damage;
        return true;

    }
    public int DamageToMonster()
    {
        return 0;
    }

}

