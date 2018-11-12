using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    [Header("Game Properties")]
    //게임끝나고 결과 보여줄 text UI
    [SerializeField]
    Text GameResult;

    PlayerHealth playertime;
	// Use this for initialization
	void Start () {
        playertime = UnityEngine.GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
	}

    // Update is called once per frame
    void Update()
    {
        if (playertime.GetCurrentState() == PlayerHealth.PlayState.dead)
        {
            GameOver();
        }
        else if(playertime.GetCurrentState() == PlayerHealth.PlayState.clear)
        {
            GameClear();
        }

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
