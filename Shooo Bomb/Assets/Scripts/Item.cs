using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour {
    //Item Instance 생성
    public static Item instance;
    float endTimer = 3.0f;
    float Additional_speed = 3.0f;

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
            case 1: {
                    getBigger(p);
                    break;
                }
            case 2: {
                    getBlind(p);
                    break;
                }
            case 3: {
                    getSpeed();
                    break;
                }
            case 4:
                {
                    break;
                }
            case 5:
                {
                    break;
                }
            default:
                break;
        }
        Debug.Log(itemNum + "called");
    }

    public int randomItem()
    {
        int itemNum = (int) (Random.value * 5 + 1);
        return itemNum;
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

    //시야차단막 비활성화
    public void backupBlind()
    {
        PlayerHealth.instance.Blind_Wall.SetActive(false);
    }
}
