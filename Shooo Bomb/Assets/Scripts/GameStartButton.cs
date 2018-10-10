using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStartButton : MonoBehaviour {

	// Use this for initialization
	void Start () {
      		
	}
	
    // Start_Game() 함수는 IntroScene이 있는 Scenes 폴더 내의 GameScene을 불러온다.
	public void Start_Game()
    {
        SceneManager.LoadScene("GameScene");
    }
}
