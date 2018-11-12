using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PreGameManager : MonoBehaviour {

    public static PreGameManager instance;
    //canvas에 넣으세요!!!!
    //게임오버됐을때 GameOver를 보여줄 UI

    //player
    bool gameover;
    float Additional_speed = 3.0f;
    float endTimer = 3.0f;


    [Header("Game Properties")]
    //처음 시작시간

    [Header("UI Elements")]
    


    [SerializeField]
    Text GameResult;


    void Awake()
    {
        
        instance = this;
        gameover = false;
    }
    // Use this for initialization
    void Start () {
       

        GameResult.text = "";
	}
	
	// Update is called once per frame
	void Update () {
        //gameover가 true이면 Update함수를 나온다
        if (gameover)
        {
            GameOver();
            
        }
        

	}

    //벽이나 장애물에 닿았을 경우 발생(폭발)
    public void Explode(GameObject p)
    {
        
        p.GetComponent<Rigidbody>().velocity = Vector3.zero;
        //p.GetComponent<GameObject>().enabled = false;
        p.GetComponent<ParticleSystem>().Play();
        p.GetComponent<AudioSource>().Play();
        Destroy(p.gameObject, 1.0f);
        GameOver();
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
