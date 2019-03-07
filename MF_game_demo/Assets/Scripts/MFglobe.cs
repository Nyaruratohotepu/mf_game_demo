using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GunEnum
{
    //枪械类型
    public enum GunType
    {
        pistol,
        shotgun,
        submachine_gun,
        assault_rifle,
        sniper_rifle,
        machine_gun
    }

    //击发方式半自动、全自动、栓动
    public enum FireType
    {
        semi_automatic,
        automatic,
        turn_bolt
    }

    //稀有度
    public enum Rarity
    {
        n,
        r,
        sr,
        ssr,
        ur
    }
    //枪械状态图对应状态
    public enum GunState
    {
        Idling,
        Aiming,
        Reloading
    }

}
public static class MonsterEnum
{
    public enum MonsterType
    {
        slime   //史莱姆
    }//怪物种类
    public enum MonsterState//状态，平静，追逐玩家，濒死，致盲，减速，麻痹，混乱
    {
        idle,
        chasing,
        dying,
        blind,
        slowed,
        cantmove,
        crazy
    }
}
public static class IOTool
{
    public static bool GetMousePosition(out RaycastHit mousePoint)
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);    //Camera.main表示Tag为MainCamera的Camera，此句从摄像机向鼠标位置发射一条射线


        //如果射线碰撞
        if (Physics.Raycast(ray, out mousePoint, 200))
            return true;

        else
            return false;
    }
}
public static class InventoryEnum
{
    public enum ItemType
    {
        /// <summary>
        /// 武器
        /// </summary>
        Weapon,
        /// <summary>
        /// 投掷物
        /// </summary>
        Grenade,
        /// <summary>
        /// 弹药
        /// </summary>
        Ammo,
        /// <summary>
        /// 剧情物体
        /// </summary>
        StoryItem,
        /// <summary>
        /// 可使用道具，药剂等
        /// </summary>
        PropertItem,
        /// <summary>
        /// 其他物体，如测试用
        /// </summary>
        Other
    }
}
