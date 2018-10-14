using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public static GameManager instance;
    //canvas에 넣으세요!!!!
    //게임오버됐을때 GameOver를 보여줄 UI

    //player
    bool gameover;
    float Additional_speed = 40.0f;
    float endTimer = 3.0f;


    [Header("Game Properties")]
    //처음 시작시간
    [SerializeField]
    float timeAmount;
    [Header("UI Elements")]
    
    //시간이 지나가는 것을 보여주는 UI
    [SerializeField]
    Text timecount;

    [SerializeField]
    Text GameResult;


    void Awake()
    {
        
        instance = this;
        gameover = false;
    }
    // Use this for initialization
    void Start () {
       
        
        timecount.text = ((int)timeAmount).ToString();
        GameResult.text = "";
	}
	
	// Update is called once per frame
	void Update () {
        //gameover가 true이면 Update함수를 나온다
        if (gameover)
        {
            GameOver();
            
        }
        //60프레임에 1초씩 줄어든다.
        timeAmount -= Time.deltaTime;
        //timeAmount가 0이면 gameover된다
        if (timeAmount <= 0f)
        {
            timeAmount = 0f;
            gameover = true;
            Explode(Player.instance);
        }

        timecount.text = "0:" + ((int)timeAmount).ToString();
        
	}

    //벽이나 장애물에 닿았을 경우 발생(폭발)
    public void Explode(Player p)
    {
        gameover = true;
        p.GetComponent<Rigidbody>().velocity = Vector3.zero;
        p.GetComponent<Player>().enabled = false;
        p.GetComponent<ParticleSystem>().Play();
        Destroy(p.gameObject, 1.0f);
    }

    //gameover일 경우 text로 띄우기.
    public void GameOver()
    {
        GameResult.text = "Game Over";
    }

    public void GameClear()
    {
       GameResult.text = "Game Clear";
       Destroy(Player.instance.gameObject, 1.0f);
    }


    public void getBigger(Player p)
    {
        p.transform.position = new Vector3(p.transform.position.x, p.transform.position.y * 2, p.transform.position.z);
        iTween.ScaleTo(p.gameObject, p.transform.localScale * 2, 1);
        Invoke("backupBig", endTimer);
    }

    public void getBlind(Player p)
    {
        p.Blind_Wall.SetActive(true);
        Invoke("backupBlind", endTimer);
    }

    //플레이어의 속도가 2배가 되는 함수
    public void doubleSpeed(Player p)
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
