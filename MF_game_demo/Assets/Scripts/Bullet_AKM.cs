using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_AKM : MonoBehaviour
{
    public float Speed { set; get; }
    //在AKM中指定
    private string ImpactPath;
    public bool Fired { set; get; }
    private Vector3 Direction;
    public Bullet_AKM()
    {
        Speed = 15;
        ImpactPath = "Prefabs/Bullets/Bullet_AKM_Impact";
        Fired = false;
    }
    public void Init(Vector3 muzzlePosition, Vector3 TargetPosition)
    {
        gameObject.transform.position = muzzlePosition;
        gameObject.transform.LookAt(TargetPosition);
        Direction = TargetPosition - muzzlePosition;
    }


    // Use this for initialization

    // Update is called once per frame
    void Update()
    {
        if (Fired)
            transform.position = transform.position + Direction * Time.deltaTime * Speed;
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
