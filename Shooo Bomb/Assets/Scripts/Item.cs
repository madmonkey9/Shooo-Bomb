using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour {
    //Item Instance 생성
    public static Item instance;
    float endTimer = 3.0f;
    float Additional_speed = 3.0f;

    // Player에 해당하는 변수
    public Transform player;

    // Player와 Blind_Wall 사이의 거리에 해당하는 좌표변수 x, y, z 선언
    private float x;
    private float y;
    private float z;

    // 현재 모드(공의 방향)를 저장하기 위한 변수
    private int Mode;

    // Mode에 따라 벽을 한번 변경 시키기 위해서 사용하는 임시 변수
    private int tmp;


    //Blind_Wall
    public GameObject B;
    
    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        // Mode 값 초기화
        Mode = 0;

        B.SetActive(false);

        // 임시변수의 값 초기화
        tmp = 0;

        // Player와 Blind_Wall 사이의 거리를 저장해두는 좌표변수 x, y, z
        x = B.transform.position.x - player.transform.position.x;
        y = B.transform.position.y - player.transform.position.y;
        z = B.transform.position.z - player.transform.position.z;
    }

    void Update()
    {
        //모드를 4 -> 0 으로 바꾸기 위함
        if (Mode > 3)
        {
            Mode = Mode % 4;
        }

        //모드를 음수에서 양수로 바꾸기 위함
        if (Mode < 0)
        {
            Mode = Mode + 4;
        }

        // 현재 모드를 통해 Blind_Wall의 위치를 이동시키기 위함
        // 처음에 저장해두었던 Player와 Blind_Wall 사이의 거리를 더해서 벽의 위치를 이동시킴
        switch (Mode)
        {
            // 모드 0는 Z가 증가하는 방향
            case 0:
                if (tmp == 0)
                {
                    B.transform.localEulerAngles = new Vector3(-45, 0, 0);
                    tmp++;
                }
                B.transform.position =
                    Vector3.Lerp(B.transform.position,
                    player.transform.position + new Vector3(x, y, z),
                    Time.deltaTime * 10);
                break;
            // 모드 1은 X가 감소하는 방향
            case 1:
                if (tmp == 0)
                {
                    B.transform.localEulerAngles = new Vector3(0, 0, -45);
                    tmp++;
                }
                B.transform.position =
                    Vector3.Lerp(B.transform.position,
                    player.transform.position + new Vector3(-z, y, x),
                    Time.deltaTime * 10);
                break;
            // 모드 2는 Z가 감소하는 방향
            case 2:
                if (tmp == 0)
                {
                    B.transform.localEulerAngles = new Vector3(45, 0, 0);
                    tmp++;
                }
                B.transform.position =
                    Vector3.Lerp(B.transform.position,
                    player.transform.position + new Vector3(-x, y, -z),
                    Time.deltaTime * 10);
                break;
            // 모드 3은 X가 증가하는 방향
            case 3:
                if (tmp == 0)
                {
                    B.transform.localEulerAngles = new Vector3(0, 0, 45);
                    tmp++;
                }
                B.transform.position =
                    Vector3.Lerp(B.transform.position,
                    player.transform.position + new Vector3(z, y, -x),
                    Time.deltaTime * 10);
                break;
        }

        // 왼쪽 방향키를 입력 받았을 때 벽의 각도를 바꾸기 위해서 Mode와 Blind_Wall을 변동시킴
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            Mode += 1;
            tmp = 0;
        }

        // 오른쪽 방향키를 입력 받았을 때 벽의 각도를 바꾸기 위해서 Mode와 Blind_Wall을 변동시킴
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            Mode -= 1;
            tmp = 0;
        }
    }

    //item을 먹었을 때 아이템의 이름에 따른 효과
    public void itemEffects(Player p, string itemName)
    {
        switch(itemName)
        {
            case "item_bigger": {
                    getBigger(p);
                    break;
                }
            case "item_blind": {
                    getBlind(B);
                    break;
                }
            case "item_speed": {
                    getSpeed(p);
                    break;
                }
            default:
                break;
        }
    }

    //공의 크기가 두배가 커진다.
    public void getBigger(Player p)
    {
        p.transform.position = new Vector3(p.transform.position.x, p.transform.position.y * 2, p.transform.position.z);
        iTween.ScaleTo(p.gameObject, p.transform.localScale * 2, 1);
        Invoke("backupBig", endTimer);
    }

    //카메라 앞에 장막이 생성된다.
    public void getBlind(GameObject B)
    {
        B.SetActive(true);
        Invoke("backupBlind", endTimer);
    }

    //플레이어의 속도가 2배가 되는 함수
    public void getSpeed(Player p)
    {
        p.speed += Additional_speed;
        Invoke("backupSpeed", endTimer);

    }

    //크기 복구 함수
    public void backupBig()
    {
        Player.instance.transform.position = new Vector3(Player.instance.transform.position.x, Player.instance.transform.position.y / 2, Player.instance.transform.position.z);
        iTween.ScaleBy(Player.instance.gameObject, Player.instance.transform.localScale / 2, 1);
    }

    //속도 복구 함수
    public void backupSpeed()
    {
        Player.instance.speed -= Additional_speed;
    }

    public void backupBlind()
    {
        B.SetActive(true);
    }
}
