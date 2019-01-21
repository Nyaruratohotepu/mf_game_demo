using Assets.Scripts;
using Assets.Scripts.Weapons;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFire : MonoBehaviour
{

    public GameObject gunObj;
    public Weapon gun;

    // Use this for initialization
    void Start()
    {

        gun = new WinchesterM1897(gunObj, gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        gun.OnUpdate();
    }

    private void OnAnimatorIK(int layerIndex)
    {
        gun.OnAnimatorIK();
    }
    private void FixedUpdate()
    {
        gun.OnFixedUpdate();
    }

}
