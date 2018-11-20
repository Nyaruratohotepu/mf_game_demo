using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterManager 
{
    private List<Monster> monsters;
    private MonsterFactory monsterFactory;

    // Use this for initialization
    void Start()
    {
        monsters = new List<Monster>();
        monsters.Add(CreateMonster(MonsterEnum.MonsterType.slime, new Vector3(3, 0, 4)));
    }

    // Update is called once per frame
    void Update()
    {

    }

    //在location创建一个monsterType类型的怪物
    private Monster CreateMonster(MonsterEnum.MonsterType monsterType, Vector3 location)
    {
        Monster newMonster = monsterFactory.GetNewMonster(monsterType);
        newMonster.Self.transform.position = location;
        return newMonster;
    }
}
