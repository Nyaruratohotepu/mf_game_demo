using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class monstermanager : MonoBehaviour {
    public List<GameObject> normalmonsteralive = new List<GameObject>();
	// Use this for initialization
	void Start () {
        createSlime_Green(new Vector3(-5, 0.5f, 0));
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    void createSlime_Green(Vector3 pos)
    {
        GameObject mon = Instantiate(Resources.Load("monster/Slime_Green") as GameObject, pos, Quaternion.identity);
        normalmonsteralive.Add(mon);
        NormalMonster n = mon.GetComponent<NormalMonster>();
        n.id = 1;
        n.hp = 100;
        n.atk = 10;
        n.atkbetween = 2;
        n.name = "Slime_Green";
        n.chinesename = "绿色史莱姆";
        n.introduction = "非常弱小的怪物";
        n.movespeed = 3;
        n.nowstate = Monsterbase.state.idle;
        n.targetpos = new Vector3(0,0,100);
        n.isshoot = false;
    }
}
