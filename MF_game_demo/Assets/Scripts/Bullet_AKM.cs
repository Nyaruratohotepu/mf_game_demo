using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_AKM : MonoBehaviour
{
    public float Speed { set; get; }
    public Vector3 Target { set; get; }
    //在AKM中指定
    public string ImpactPath { set; get; }
    public bool Fired { set; get; }
    public Vector3 direction { set; get; }
    public Bullet_AKM()
    {
        Speed = 50;
        ImpactPath = "Prefabs/Bullets/Bullet_AKM_Impact";
        Fired = false;
    }
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(Target);

        if (Fired)
            transform.position = transform.position + direction * Time.deltaTime * Speed;
    }
    private void OnCollisionStay(Collision collision)
    {
        OnCollisionEnter(collision);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player") return;
        print("Hit");
        Fired = false;

        GetComponent<ParticleSystem>().Stop();
        GameObject impact = GameObject.Instantiate(Resources.Load<GameObject>(ImpactPath));
        impact.transform.position = collision.contacts[0].point;
        impact.transform.Rotate(new Vector3(0, 180, 0), Space.Self);
        Destroy(gameObject);
    }
}
