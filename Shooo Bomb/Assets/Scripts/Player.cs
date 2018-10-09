using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    //plus속도
    public float add_speed;
    //fast아이템을 획득한 후 속도
    public float Additional_speed;
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
    //전진 방향
    Vector3 dir_forward = Vector3.forward;
    Vector3 dir_left = Vector3.left;
    Vector3 dir_right = Vector3.right;
    //리지드 바디;
    Rigidbody rb;
    //파티클 시스템
    ParticleSystem particle;
    //트랜스폼
    Transform tr;
    //각 아이템의 사용시간
    float endTimer = 3.0f;
    public GameObject Blind_Wall; //Blind_Wall의 오브젝트


    
    // Use this for initialization
    void Start ()
    {
        rb = GetComponent<Rigidbody>();
        particle = GetComponent<ParticleSystem>();
        tr = GetComponent<Transform>();
        //벽의 활성화 금지
        Blind_Wall.SetActive(false);
        
    }

    // Update is called once per frame

    void Update ()
    {
        Vector3 vel = new Vector3();
        //터치를 하고있는지 여부
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

        if (click && Input.mousePosition.x < Screen.width / 2) // 스크린 왼쪽 터치하면 왼쪽으로 틀음.
        {
            BallControllLeft();
        }
        else if (click && Input.mousePosition.x >= Screen.width / 2) //스크린 오른쪽 터치하면 오른쪽으로 틀음.
        {
            BallControllRight();
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow)) //왼쪽방향키 눌렀을때 -90도 회전하고 진행방향 바꿈.
        {
            BallTurnLeft();
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow)) //오른쪽방향키 눌렀을때 90도 회전하고 진행방향 바꿈.
        {
            BallTurnRight();
        }
        BallMovement(dir_forward);

        

    }

    //Translate로 공을 움직이게 하는 함수
    //인자값은 방향을 나타내는 벡터
    void BallMovement(Vector3 dir)
    {
        rb.AddForce(dir * add_speed * Time.deltaTime );
    }
    //터치했을때 직진하면서 왼쪽으로 움직이는 함수
    void BallControllLeft()
    {
        rb.AddForce(dir_left * add_speed * Time.deltaTime);
    }
    //터치했을때 직진하면서 오른쪽으로 움직이는 함수
    void BallControllRight()
    {
        rb.AddForce(dir_right * add_speed * Time.deltaTime);
    }
    //터치슬라이드했을때 -90도로 진행방향 바꾸는 함수
    void BallTurnLeft()
    {
        Vector3 dir = rb.velocity;
        dir.Normalize();
        dir_forward = Quaternion.Euler(0, -90, 0) * dir;
        dir_left = Quaternion.Euler(0, -90, 0) * dir_left;
        dir_right = Quaternion.Euler(0, -90, 0) * dir_right;
    }
    //터치슬라이드했을때 90도로 진행방향 바꾸는 함수
    void BallTurnRight()
    {
        Vector3 dir = rb.velocity;
        dir.Normalize();
        dir_forward = Quaternion.Euler(0, 90, 0) * dir;
        dir_left = Quaternion.Euler(0, 90, 0) * dir_left;
            
        dir_right = Quaternion.Euler(0, 90, 0) * dir_right;
    }

    private void OnTriggerEnter(Collider other)
    {
        //트리거의 태그와 이름
        string tagName = other.gameObject.tag;
        string itemName = other.gameObject.name;

        other.gameObject.SetActive(false);
        switch (tagName)
        {
            
            case "item":
                if (itemName.Equals("item_bigger")) 
                    getBigger();
                else if (itemName.Equals("item_speed"))
                    doubleSpeed();
                else if (itemName.Equals("item_blind"))
                    getBlind();
                break;
            default:
                break;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "obstacle") {
            Explode();
        }

        if(collision.gameObject.tag.Equals("wall"))
        {
            Explode();
        }
    }


    //플레이어의 크기를 2배로 증가시키는 함수
    void getBigger()
    {
        tr.position = new Vector3(tr.position.x, tr.position.y * 2, tr.position.z);
        
        iTween.ScaleTo(gameObject, tr.localScale * 2, 1);
        Invoke("backupBig", endTimer);
    }

    //플레이어의 시야를 가리는 함수
    void getBlind()
    {
        Blind_Wall.SetActive(true);
        Invoke("backupBlind", endTimer);
    }

    //플레이어의 속도가 2배가 되는 함수
    void doubleSpeed()
    {
        add_speed += Additional_speed;
        Invoke("backupSpeed", endTimer);
        
    }

    //크기 복구 함수
    void backupBig()
    {
        tr.position = new Vector3(tr.position.x, tr.position.y / 2, tr.position.z);
       
        iTween.ScaleBy(gameObject, tr.localScale / 2, 1);


    }

    //속도 복구 함수
    void backupSpeed()
    {
        add_speed -= Additional_speed;
    }

    //시야차단막 비활성화
    void backupBlind()
    {
        Blind_Wall.SetActive(false);
    }

    //벽이나 장애물에 닿았을 경우 발생(폭발)
    void Explode()
    {
        rb.isKinematic = true;
        particle.Play();
        Destroy(gameObject, particle.duration);
    }

    
}
