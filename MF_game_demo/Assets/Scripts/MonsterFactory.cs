using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterFactory
{
    public Monster GetNewMonster(MonsterEnum.MonsterType type)
    {
        switch (type)
        {
            case MonsterEnum.MonsterType.slime:
                GameObject slimeObject = GameObject.Instantiate(Resources.Load<GameObject>("monster/Slime_Green"));
                if (slimeObject == null)
                    break;
                return slimeObject.AddComponent<SlimeScript>().Slime;
            default:
                return null;
        }
        return null;
    }
}
