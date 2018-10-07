using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    //plus속도
    public float add_speed;
    //좌우로 움직일때 힘;
    public float controll_force;
    //mouse(touch)가 지속되고 있나.
    bool click;
    // 터치슬라이드를 한 기준 속도(임계값)
    public float threshold;
    //전프레임위치값
    Vector3 prevPos;
    //현재프레임위치와 전프레임위치값.
    Vector3 diffPos;
    Vector3 dir_forward = Vector3.forward;
    //리지드 바디;
    Rigidbody rb;
    //파티클 시스템
    ParticleSystem particle;
    // Use this for initialization
    void Start ()
    {
        rb = GetComponent<Rigidbody>();
        particle = GetComponent<ParticleSystem>();
    }

    // Update is called once per frame

    void Update ()
    {
        //Vector3 vel = new Vector3();
        //if (Input.GetMouseButtonDown(0))
        //{
        //    click = true;
        //}
        //if (Input.GetMouseButtonUp(0))
        //{
        //    click = false;

        //}
        ////터치입력을 통해 player의 방향을 결정해서 움직이게 한다.
        //if (click) //직진할때 터치를 통해 양옆으로 움직이기
        //{
        //    float moveHorizontal = Input.GetAxis("Mouse X");
           
            
            
        //    //vel = diffPos / Time.deltaTime;
        //    //prevPos = Input.mousePosition.position;
        //}
        
        //if (vel.x > threshold)//왼쪽으로 회전이동하고 그 방향으로 직진
        //{
               
        //}
        //else if(vel.y < -threshold) // 오른쪽으로 회전이동 그 방향으로 직진
        //{

        //}
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            BallControllLeft();
        }
        else if(Input.GetKeyDown(KeyCode.Alpha2))
        {
            BallControllRight();
        }
        else if(Input.GetKeyDown(KeyCode.Alpha3))
        {
            BallTurnLeft();
        }
        else if(Input.GetKeyDown(KeyCode.Alpha4))
        {
            BallTurnRight();
        }
        BallMovement(dir_forward);


    }

    //Translate로 공을 움직이게 하는 함수
    //인자값은 방향을 나타내는 벡터
    void BallMovement(Vector3 dir)
    {
        rb.AddForce(dir * add_speed);
    }
    void BallControllLeft()
    {
        rb.AddForce(Vector3.left * controll_force);
    }
    void BallControllRight()
    {
        rb.AddForce(Vector3.right * controll_force);
        }
    void BallTurnLeft()
    {
        Vector3 dir = rb.velocity;
        dir.Normalize();
        dir_forward = Quaternion.Euler(0, -90, 0) * dir;
    }
    void BallTurnRight()
    {
        Vector3 dir = rb.velocity;
        dir.Normalize();
        dir_forward = Quaternion.Euler(0, 90, 0) * dir;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "obstacle")
        {
            rb.isKinematic = true;
            particle.Play();
            Destroy(gameObject, particle.duration);
        }
    }
}
