using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    //player forward 속도
    public float forward_speed;
    //player 양옆 이동 속도
    public float controll_speed;
    //jump 속도
    public float jump_speed;
    //턴할수있는 임계값속도
    public float threshold;
    public bool isStarted = false;
    
    //player forward 방향
    Vector3 dir_forward = Vector3.forward;
    //player left 방향
    Vector3 dir_left = Vector3.left;
    //player right 방향
    Vector3 dir_right = Vector3.right;
    
    //누적회전값.
    float rotateY;
    
    //마우스 눌렀을 때의 위치.
    Vector3 prevPos;
    Vector3 diffPos;
    
    //click 지속상태
    bool click;
    //turn할 수 있는지
    bool canTurn = false;
    //바닥에 있는지 확인 할 Y값
    bool isJump = false;
    //리지드 바디;
    Rigidbody rb;
    //파티클 시스템
    ParticleSystem particle;
    

    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody>();
	}

    // Update is called once per frame
    void Update() {
        

        Vector3 vel = new Vector3();

        if (Input.GetMouseButtonDown(0))
        {
            prevPos = Input.mousePosition;
            click = true;
        }
        if (Input.GetMouseButtonUp(0))
        {
            click = false;

        }
        //click하고 있을 때 전프레임과 지금 프레임과의 위치차이 구하고 속도 구하기
        if (click)
        {
            diffPos = Input.mousePosition - prevPos;
            vel = diffPos / Time.deltaTime;
            prevPos = Input.mousePosition;
        }

        //입력값을 통해 player 움직이기
        //turn을 할 수 있는 지역에 들어왔는가. 확인하고 true면 turn만, false면 controll만
        if (vel.x < -threshold && canTurn)
        {
            BallTurnLeft();
            canTurn = false; //한 구역에서 턴은 한번밖에 못함.
        }
        else if (vel.x > threshold && canTurn)
        {
            BallTurnRight();
            canTurn = false;
        }
        else if (vel.y > threshold && !isJump)
        {
                Jump();
        }
        else if (click && Input.mousePosition.x < Screen.width / 2 && !canTurn)
        {
            BallControllLeft();
        }
        else if (click && Input.mousePosition.x >= Screen.width / 2 && !canTurn)
        {
            BallControllRight();
        }
        //바닥에 충돌하면 그때부터 전진
        if(isStarted)
            BallMovement(dir_forward);
    }

    //player를 왼쪽으로 이동할 수 있는 함수
    void BallControllLeft()
    {
        transform.Translate(dir_left * controll_speed * Time.deltaTime);
    }
    
    //player를 오른쪽으로 이동할 수 있는 함수
    void BallControllRight()
    {
        transform.Translate(dir_right * controll_speed * Time.deltaTime);
    }

    //player를 왼쪽으로 -90도 Turn할 수 있는 함수
    void BallTurnLeft()
    {
        Debug.Log("leftturn");
        rotateY += -90;
        rotateY = rotateY % 360;
        transform.eulerAngles = new Vector3(0, rotateY, 0);

    }
    //player를 왼쪽으로 90도 Turn할 수 있는 함수
    void BallTurnRight()
    {
        rotateY += 90;
        rotateY = rotateY % 360;
        transform.eulerAngles = new Vector3(0, rotateY, 0);
        
    }

    //전진하는 함수
    void BallMovement(Vector3 dirForward)
    {
        transform.Translate(dirForward * forward_speed * Time.deltaTime);
    }

    //player jump함수
    void Jump()
    {
        rb.AddForce(Vector3.up * jump_speed, ForceMode.Impulse);
        isJump = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "floor")
        {
            isJump = false;
            isStarted = true;
        }
    }

    //Turn할 수 있는 공간에 들어왔는가 확인.
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("on");
        if (other.tag == "Turnfloor")
        {
            canTurn = true;
            Debug.Log("canTurn trigger true");
        }

    }

   








}
