using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeScript : MonoBehaviour
{
    public Slime Slime { set; get; }
    // Use this for initialization
    void Start()
    {
        Slime = new Slime(1,this.gameObject);
        Slime.nav.speed = Slime.MoveSpeed;
       
    }

    // Update is called once per frame
    void Update()
    {
        Slime.OnUpdateCallback();

    }
    void OnCollisionEnter(Collision col)//当被子弹击中时，扣血，检测是否死亡
    {
        if (Slime.IsDying == false)
        {
            if (col.gameObject.tag == "FX")
            {
                Slime.Hp -= Slime.GameManager.GetDamageManager().DamageToMonster();
                if (Slime.Hp <= 0)
                {
                    Slime.Die();
                }
            }
        }
    }
}
