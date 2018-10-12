using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Player : MonoBehaviour {

    //Player의 인스턴스
    public static Player instance;
    //공의 속도 조절을 위한 변수
    public float speed;
    //공의 방향을 설정하기 위한 변수
    int Mode;
    //공을 멈추기위한 타이머 시작
    float timer = -1.0f; // 타이머가 돌지 않게 하기 위해 -1로 설정
    //공을 멈추기위한 타이머의 끝
    float E_timer = 0.2f;
    //fast아이템을 획득한 후 속도
    public float Additional_speed;
    //마우스 눌렀을 때의 위치.
    Vector3 startPos;
    Vector3 endPos;
    //click 지속상태
    bool click;
    //gameover 판단
    bool gameover;

    //Game Manager
    
    
 
    
    //리지드 바디;
    Rigidbody rb;
    //파티클 시스템
    ParticleSystem particle;
    //트랜스폼
    Transform tr;
    //각 아이템의 사용시간
    float endTimer = 3.0f;
    public GameObject Blind_Wall; //Blind_Wall의 오브젝트




    private void Awake()
    {
        
        instance = this;
        
    }
    // Use this for initialization
    void Start ()
    {
        Mode = 0;
        rb = GetComponent<Rigidbody>();
        particle = GetComponent<ParticleSystem>();
        tr = GetComponent<Transform>();
        //벽의 활성화 금지
        Blind_Wall.SetActive(false);
       
        
    }

    // Update is called once per frame

    void Update ()
    {
        //모드를 4 -> 0으로 바꿈
        if(Mode > 3)
        {
            Mode %= 4;
        }
        //모드를 음수에서 양수로 바꿈
        if(Mode < 0)
        {
            Mode += 4;
        }

        switch (Mode)
        {
            case 0: // z가 증가하는 방향으로 전진
                DefaultMove(Vector3.forward);
                MoveLeft(Vector3.left);
                MoveRight(Vector3.right);
                break;
            case 1: // x가 감소하는 방향으로 전진
                DefaultMove(Vector3.left);
                MoveLeft(Vector3.back);
                MoveRight(Vector3.forward);
                break;
            case 2: //z가 감소하는 방향으로 전진
                DefaultMove(Vector3.back);
                MoveLeft(Vector3.right);
                MoveRight(Vector3.left);
                break;
            case 3: // x가 증가하는 방향으로 전진
                DefaultMove(Vector3.right);
                MoveLeft(Vector3.forward);
                MoveRight(Vector3.back);

                break;
        }

        if(Input.GetMouseButtonDown(0))
        {
            click = true;
            startPos = Input.mousePosition;
        }

        if(Input.GetMouseButtonUp(0))
        {
            click = false;
            endPos = Input.mousePosition;
        }
        
        if(Input.GetKeyDown(KeyCode.LeftArrow))
        {
            
            Mode += 1;
            timer = 0.0f;
            Turn();
        }

        if(Input.GetKeyDown(KeyCode.RightArrow))
        {
            Mode -= 1;
            timer = 0.0f;
            Turn();
        }
        
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
        

    }

    //벡터의 방향으로 player를 움직이는 함수
    //인자값은 방향을 나타내는 벡터
    void DefaultMove(Vector3 dir)
    {
        rb.AddForce(dir.normalized * speed * Time.deltaTime );
    }

    void MoveLeft(Vector3 dir)
    {
        if (click && startPos.x < Screen.width / 2)
            tr.Translate(dir * Time.deltaTime);
    }

    void MoveRight(Vector3 dir)
    {
        if (click && startPos.x >= Screen.width / 2)
            tr.Translate(dir * Time.deltaTime);
    }

    void Jump()
    {
        rb.AddForce(Vector3.up * 100.0f);
    }
    
    //터치슬라이드했을때 90도로 진행방향 바꾸는 함수
    void Turn()
    {
        
        while(timer >= 0.0f && timer < E_timer)
        {
            timer += Time.deltaTime;

            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }
        
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
                    GameManager.instance.getBigger(this);
                else if (itemName.Equals("item_speed"))
                    GameManager.instance.doubleSpeed(this);
                else if (itemName.Equals("item_blind"))
                    GameManager.instance.getBlind(this);
                break;
            default:
                break;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "obstacle") {
            GameManager.instance.Explode(this);
            GameManager.instance.GameOver();
        }

        if(collision.gameObject.tag.Equals("wall"))
        {
            GameManager.instance.Explode(this);
            GameManager.instance.GameOver();
        }
        if(collision.gameObject.tag.Equals("exit")) //탈출구
        {
            GameManager.instance.Explode(this);
            GameManager.instance.GameClear();
        }


    }
    
    
    
   

    

    
    
}
