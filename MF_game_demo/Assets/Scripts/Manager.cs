using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    //伤害管理器，只读
    private DamageManager damageManager;
    public DamageManager GetDamageManager()
    {
        return damageManager;
    }
    //怪物管理器，只读
    private MonsterManager monsterManager;
    public MonsterManager GetMonsterManager()
    {
        return monsterManager;
    }

    //在timeLeft秒后摧毁该物体
    public void Destory(GameObject obj, float timeLeft)
    {
        Object.Destroy(obj, timeLeft);
    }
    //立刻摧毁该物体
    public void Destory(GameObject obj)
    {
        Object.DestroyImmediate(obj);
    }
    public Manager()
    {
        damageManager = new DamageManager();
        monsterManager = new MonsterManager();
    }
    public void Awake()
    {
        damageManager.Player= GameObject.Find("maruko").GetComponent<PlayerHP>();
    }
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
