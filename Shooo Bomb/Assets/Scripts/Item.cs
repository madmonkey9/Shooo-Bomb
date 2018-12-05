using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour {
    //Item Instance 생성
    public static Item instance;
    float endTimer = 3.0f;
    float Additional_speed = 3.0f;
    bool isStopped = false;

    void Awake()
    {
    
        instance = this;
    }
    
    //item을 먹었을 때 아이템의 이름에 따른 효과
    public void itemEffects(PlayerHealth p)
    {
        int itemNum = randomItem();
        switch(itemNum)
        {
            // 플레이어의 크기가 커지는 아이템
            case 1:
                getBigger(p);
                break;
            // 가림막이 활성화되는 아이템
            case 2: 
                getBlind(p);
                break;
            // 플레이어의 이동속도가 증가하는 아이템
            case 3: 
                getSpeed();
                break;
            // 방향키가 반대로 되는 아이템
            case 4:
                reverse();
                break;
            // 시간을 멈추는 아이템
            case 5:
                stopTime();
                break;
            // 플레이어가 뒤로가게 하는 아이템
            case 6:
                reverseForward();
                break;
            // 플레이어가 미끄러지도록 하는 아이템
            case 7:
                slip();
                break;
            default:
                break;
        }
        Debug.Log(itemNum + "called");
    }

    public int randomItem()
    {
        int itemNum = (int) (Random.value * 10 - 3);
        if(itemNum<= 0)
        {
            itemNum += 3;
            return itemNum;
        }
        else
        {
            return itemNum;
        }
    }

    //공의 크기가 두배가 커진다.
    public void getBigger(PlayerHealth p)
    {
        PlayerHealth.instance.transform.position = new Vector3(p.transform.position.x, p.transform.position.y * 2, p.transform.position.z);
        iTween.ScaleTo(p.gameObject, p.transform.localScale * 2, 1);
        Invoke("backupBig", endTimer);
    }

    //카메라 앞에 장막이 생성된다.
    public void getBlind(PlayerHealth p)
    {
        p.Blind_Wall.SetActive(true);
        Invoke("backupBlind", endTimer);
    }

    //플레이어의 속도가 2배가 되는 함수
    public void getSpeed()
    {
        PlayerHealth.instance.GetComponent<PlayerMovement>().forward_speed += Additional_speed;
        Invoke("backupSpeed", endTimer);

    }

    //플레이어의 컨트롤러가 반대로 바뀌는 함수
    public void reverse()
    {
        PlayerHealth.instance.GetComponent<PlayerMovement>().dir_left = Vector3.right;
        PlayerHealth.instance.GetComponent<PlayerMovement>().dir_right = Vector3.left;
        Invoke("backupController", endTimer);
    }

    // default값을 후진으로 바꿔주는 함수
    public void reverseForward()
    {
        PlayerHealth.instance.GetComponent<PlayerMovement>().dir_forward = Vector3.back;
        Invoke("backupForward", endTimer);
    }

    // 플레이어가 미끄러지도록 하는 함수
    public void slip()
    {
        PlayerHealth.instance.GetComponent<PlayerMovement>().rb.drag = 1.8f;
        Invoke("backupSlip", endTimer);
    }

    //크기 복구 함수
    public void backupBig()
    {
        PlayerHealth.instance.transform.position = new Vector3(PlayerHealth.instance.transform.position.x
            , PlayerHealth.instance.transform.position.y / 2, PlayerHealth.instance.transform.position.z);
        iTween.ScaleBy(PlayerHealth.instance.gameObject, PlayerHealth.instance.transform.localScale / 2, 1);
    }

    //속도 복구 함수
    public void backupSpeed()
    {
        PlayerHealth.instance.GetComponent<GameObject>().GetComponent<PlayerMovement>().forward_speed -= Additional_speed;
    }

    //컨트롤러 복구 함수
    public void backupController()
    {
        PlayerHealth.instance.GetComponent<PlayerMovement>().dir_left = Vector3.left;
        PlayerHealth.instance.GetComponent<PlayerMovement>().dir_right = Vector3.right;
    }

    // 후진 복구 함수
    public void backupForward()
    {
        PlayerHealth.instance.GetComponent<PlayerMovement>().dir_forward = Vector3.forward;
    }

    // 미끄러짐 복구 함수
    public void backupSlip()
    {
        PlayerHealth.instance.GetComponent<PlayerMovement>().rb.drag = 3;
    }

    //시야차단막 비활성화
    public void backupBlind()
    {
        PlayerHealth.instance.Blind_Wall.SetActive(false);
    }

    //시간 멈추는 함수
    public void stopTime()
    {
        isStopped = !isStopped;
        if (isStopped == true)
        {
            PlayerHealth.instance.currentTime += Time.deltaTime;
            Invoke("stopTime", endTimer);
        }
        else
            return;
    }
}
