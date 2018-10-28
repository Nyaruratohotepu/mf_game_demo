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
