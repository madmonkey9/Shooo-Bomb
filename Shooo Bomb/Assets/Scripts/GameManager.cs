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
        }

        timecount.text = "0:" + ((int)timeAmount).ToString();
        
	}

    //벽이나 장애물에 닿았을 경우 발생(폭발)
    public void Explode(Player p)
    {
        gameover = true;
        p.GetComponent<Rigidbody>().velocity = Vector3.zero;
        //gameObject.SetActive(false);
        p.GetComponent<Rigidbody>().isKinematic = true;
        p.GetComponent<ParticleSystem>().Play();
        Destroy(p.gameObject, p.GetComponent<ParticleSystem>().duration);
    }

    //gameover일 경우 text로 띄우기.
    public void GameOver()
    {
        GameResult.text = "Game Over";
    }

    public void GameClear()
    {
       GameResult.text = "Game Clear";
    }

    

}
