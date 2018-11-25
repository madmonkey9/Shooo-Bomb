using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour {
    //player 상태
    public enum PlayState
    {
        playing,
        dead,
        clear
    }
    //시작 시간
    public float startTime =30f;
    //현재 남은 시간
    public float currentTime;
    //시간 슬라이더
    public Slider timeSlider;
    //PlayerMovement 스크립트 
    PlayerMovement playerMovement;
    //현재 player상태
    PlayState currentState;
    public GameObject Blind_Wall;
    //자신의 게임오브젝트
    public static PlayerHealth instance;

    //메인카메라에 붙어있는 cameraController 스크립트
    public CameraController main_Camera;
    // Use this for initialization
    
    void Start()
    {
        instance = this;
        playerMovement = GetComponent<PlayerMovement>();
        main_Camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraController>();
        currentTime = startTime;
        
    }

	// Update is called once per frame
	void Update () {
        if(currentState == PlayState.dead)
            return;
        currentTime -= Time.deltaTime;
        timeSlider.value = currentTime;
        if(currentTime <= 0)
        {
            Death();
        }
	}

    //죽었을때 이펙트 넣어주고 플레이어 파괴하기.
    void Death()
    {
        GetComponent<ParticleSystem>().Stop();
        GetComponent<ParticleSystem>().Play();
        GetComponent<AudioSource>().Stop();
        GetComponent<AudioSource>().Play();
        GetComponent<PlayerMovement>().enabled = false;
        main_Camera.enabled = false;
        Destroy(gameObject, 1.0f);

        GameObject.Find("UICanvas").transform.Find("GameOverPanel").gameObject.SetActive(true); // 죽었을 때 GameOverUI 표시
        Time.timeScale = 0.01f;
    }
    //현재 플레이어의 상태
    public PlayState GetCurrentState()
    {
        return currentState;
    }

    //아이템을 먹었을때 
    private void OnTriggerEnter(Collider other)
    {
        //트리거의 태그와 이름
        string itemTag = other.gameObject.tag;
        string itemName = other.gameObject.name;

        other.gameObject.SetActive(false);
        if (itemTag.Equals("item"))
            Item.instance.itemEffects(instance);
        else if (itemTag.Equals("obstacle")) //장애물 부딫혔을 때 5초를 깎음
        {
            currentTime -= 5;
        }
        //GameManager.instance.
    }

    //장애물충돌했을때.
    private void OnCollisionEnter(Collision collision)
    { 
        if (collision.gameObject.tag.Equals("wall"))
        {
            Death();
            currentState = PlayState.dead;
        }
        if (collision.gameObject.tag.Equals("Stage1exit")) // Stage1 Clear
        {
            currentState = PlayState.clear;
            GameObject.Find("UICanvas").transform.Find("Stage1ClearPanel").gameObject.SetActive(true);
            Time.timeScale = 0.01f;
        }

        if (collision.gameObject.tag.Equals("Stage2exit")) // Stage2 Clear
        {
            currentState = PlayState.clear;
            GameObject.Find("UICanvas").transform.Find("Stage2ClearPanel").gameObject.SetActive(true);
            Time.timeScale = 0.01f;
        }

    }
}
