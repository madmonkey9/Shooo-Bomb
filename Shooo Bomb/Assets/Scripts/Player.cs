using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Player : MonoBehaviour {

    public Text gameovertext;
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
        Mode = 0;
        rb = GetComponent<Rigidbody>();
        particle = GetComponent<ParticleSystem>();
        tr = GetComponent<Transform>();
        //벽의 활성화 금지
        Blind_Wall.SetActive(false);
        gameovertext.text = "";
        
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
            IsGameOver();
        }

        if(collision.gameObject.tag.Equals("wall"))
        {
            Explode();
            IsGameOver();
        }
        if(collision.gameObject.tag.Equals("exit")) //탈출구
        {
            Explode();
            gameovertext.text = "GAME CLEAR";
        }


    }
    //gameover일 경우 text로 띄우기.
    void IsGameOver()
    {
        gameovertext.text = "GAME OVER";
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
        speed += Additional_speed;
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
       speed -= Additional_speed;
    }

    //시야차단막 비활성화
    void backupBlind()
    {
        Blind_Wall.SetActive(false);
    }

    //벽이나 장애물에 닿았을 경우 발생(폭발)
    void Explode()
    {
        speed = 0;
        //gameObject.SetActive(false);
        rb.isKinematic = true;
        particle.Play();
        Destroy(gameObject, particle.duration);
    }

    
    
}
