using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraAction : MonoBehaviour {
    public GameObject player;
    public Vector3 offset;  //限定摄像机离角色位置

    [Tooltip("摄像机跟随速度，0-1，1为锁死位置")]
    public float speed; 
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.position =Vector3.Lerp(transform.position, player.transform.position + offset,speed);
	}
}
