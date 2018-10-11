using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class GameManager : MonoBehaviour {
   
    //canvas에 넣으세요!!!!
    //게임오버됐을때 GameOver를 보여줄 UI
    public Text gameovertext;
    //player
    public GameObject player;
    [Header("Game Properties")]
    //처음 시작시간
    [SerializeField]
    float timeAmount = 20f;
    [Header("UI Elements")]
    
    //시간이 지나가는 것을 보여주는 UI
    [SerializeField]
    Text timecount;
    bool gameover;
	// Use this for initialization
	void Start () {
       
        
        timecount.text = ((int)timeAmount).ToString();
        gameovertext.text = "";
	}
	
	// Update is called once per frame
	void Update () {
        //gameover가 true이면 Update함수를 나온다
        if (gameover)
        {
            gameovertext.text = "GAME OVER";
            
        }
        //60프레임에 1초씩 줄어든다.
        timeAmount -= Time.deltaTime;
        //timeAmount가 0이면 gameover된다
        if (timeAmount <= 0f)
        {
            timeAmount = 0f;
            gameover = true;
        }

        timecount.text = ((int)timeAmount).ToString();
	}

    
}
