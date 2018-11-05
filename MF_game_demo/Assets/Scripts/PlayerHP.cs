using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHP : MonoBehaviour
{
    public float HP { set; get; }
    public PlayerHP()
    {
        HP = 100;
    }
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (HP <= 0)
            Die();
    }
    public void Die()
    {

    }
}
