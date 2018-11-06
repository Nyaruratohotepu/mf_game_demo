using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour {
    public GameObject cube;
	// Use this for initialization
	void Start () {
        int[,] a = new int[3, 4] { { 1, 0, 1, 1 },     //地图信息
                                   { 1, 1, 0, 1 },
                                   { 1, 1, 1, 0 } };
        createmap(a);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    void createmap(int[,] a)
    {
        int i = 0, j = 0;
        Vector3 pos = new Vector3(0, 0, 0);
        while (i < a.GetLength(0))//第一遍铺地板
        {
            while (j < a.GetLength(1))
            {
                Instantiate(cube, pos, Quaternion.identity);
                pos.x++;
                j++;
            }
            j = 0;
            i++;
            pos.x = 0;
            pos.z++;
        }
        i = 0;
        j = 0;
        pos = new Vector3(0, 1, 0);
        while (i < a.GetLength(0))//第二遍垒墙
        {
            while (j < a.GetLength(1))
            {
                if (a[i, j] == 1)
                {
                    Instantiate(cube, pos, Quaternion.identity);
                }
                pos.x++;
                j++;
            }
            j = 0;
            i++;
            pos.x = 0;
            pos.z++;
        }
    }
}
