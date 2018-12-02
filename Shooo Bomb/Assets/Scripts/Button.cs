using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Button : MonoBehaviour {

    GameObject stage2point = null;
    GameObject stage3point = null;
    GameObject stage4point = null;

    // Start_Game() 함수는 IntroScene이 있는 Scenes 폴더 내의 GameScene을 불러온다.
    public void Start_Game()
    {
        Time.timeScale = 1.0f;
        LoadingSceneManager.LoadScene("GameScene");
    }

    // Quit_Game() 함수는 게임의 종료를 불러온다.
    public void Quit_Game()
    {
        Application.Quit();
    }

    public void Intro_Game()
    {
        Time.timeScale = 1.0f;
        LoadingSceneManager.LoadScene("IntroScene");
    }

    public void stage1clear()
    {
        stage2point = GameObject.Find("Level2StartPoint"); // Stage2 스타팅포인트를 찾아서 저장
        Time.timeScale = 1.0f;

        GameObject.Find("UICanvas").transform.Find("Stage1ClearPanel").gameObject.SetActive(false); // 클리어 UI 비활성화
        PlayerHealth.instance.transform.position = stage2point.transform.position; // 플레이어의 좌표를 스타팅포인트로 이동
    }

    public void stage2clear()
    {
        stage3point = GameObject.Find("Level3StartPoint"); // Stage3 스타팅포인트를 찾아서 저장
        Time.timeScale = 1.0f;

        GameObject.Find("UICanvas").transform.Find("Stage2ClearPanel").gameObject.SetActive(false); // 클리어 UI 비활성화
        PlayerHealth.instance.transform.position = stage3point.transform.position; // 플레이어의 좌표를 스타팅포인트로 이동
    }

    public void stage3clear()
    {
        stage4point = GameObject.Find("Level4StartPoint"); // Stage4 스타팅포인트를 찾아서 저장
        Time.timeScale = 1.0f;

        GameObject.Find("UICanvas").transform.Find("Stage3ClearPanel").gameObject.SetActive(false); // 클리어 UI 비활성화
        PlayerHealth.instance.transform.position = stage4point.transform.position; // 플레이어의 좌표를 스타팅포인트로 이동
    }
}
