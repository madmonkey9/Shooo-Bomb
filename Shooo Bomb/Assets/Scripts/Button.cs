using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Button : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    // Start_Game() 함수는 IntroScene이 있는 Scenes 폴더 내의 GameScene을 불러온다.
    public void Start_Game()
    {
        SceneManager.LoadScene("GameScene");
    }

    // Quit() 함수는 게임 종료를 불러온다.
    public void Quit_Game()
    {
        Application.Quit();
    }
}
