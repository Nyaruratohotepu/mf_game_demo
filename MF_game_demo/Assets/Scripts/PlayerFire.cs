using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFire : MonoBehaviour
{

    public GameObject gunObj;
    public Gun gun;

    // Use this for initialization
    void Start()
    {
        gun = new AKM(gunObj, gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        gun.OnUpdateCallback();
    }

    private void OnAnimatorIK(int layerIndex)
    {
        gun.OnAnimatorIKCallback();
    }
    private void FixedUpdate()
    {
        gun.OnFixedUpdateCallback();
    }

}
