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
    public void itemEffects(Player p, string itemName)
    {
        switch(itemName)
        {
            case "item_bigger": {
                    getBigger(p);
                    break;
                }
            case "item_blind": {
                    getBlind(p);
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
    public void getBlind(Player p)
    {
        p.Blind_Wall.SetActive(true);
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

    //시야차단막 비활성화
    public void backupBlind()
    {
        Player.instance.Blind_Wall.SetActive(false);
    }
}
