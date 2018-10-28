using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [Tooltip("移动速度")]
    public float speed;

    [Tooltip("旋转速度，0-1之间，1为完全跟随鼠标")]
    public float rotate_speed;

    private CharacterController cc;
    private Animator ac;
    // Use this for initialization
    void Start()
    {
        cc = GetComponent<CharacterController>();
        ac = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        FollowMouse();
        Reposition();
    }

    //跟随鼠标转身
    private void FollowMouse()
    {
        RaycastHit hit;
        //如果能得到鼠标位置
        if (IOTool.GetMousePosition(out hit))
        {
            Vector3 directionPoint = hit.point;
            Debug.DrawLine(Camera.main.transform.position, directionPoint, Color.red);
            directionPoint.y = transform.position.y;  //只做平面旋转，指定人物旋转位置
            Debug.DrawLine(Camera.main.transform.position, directionPoint, Color.green);

            if (hit.transform.tag != "Player")
            {
                Vector3 direction = directionPoint - transform.position;
                //此处使用LookRotation方便插值

                //transform.rotation = Quaternion.LookRotation(direction);//不插值
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(direction), rotate_speed);
            }
        }
    }
    private void Reposition()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        if (Mathf.Abs(h) > 0.1f || Mathf.Abs(v) > 0.1f)
        {
            ac.SetBool("isRunning", true);

            Vector3 movement = transform.forward * v + transform.right * h;
            if (movement.magnitude > 1) movement.Normalize();   //保证人物速度不超过上限
            cc.SimpleMove(movement * speed * Time.deltaTime);

            //播放正确的动作动画
            ac.SetFloat("Direction", Mathf.Rad2Deg * Mathf.Atan2(h, v));

        }
        else ac.SetBool("isRunning", false);
    }
}
