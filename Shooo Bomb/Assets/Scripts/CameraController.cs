using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CameraController : MonoBehaviour {
    //player 오브젝트
    GameObject player;
    //카메라의 이동속도
    public float smoothRotate = 5f;
    //카메라가 player를 바라보고 있는 방향
    Vector3 offsetDir;
    //카메라와 player의 거리
    public float offset = 3f;
    //카메라의 높이
    public Vector3 height = new Vector3(0,1.5f,0);
    //도는 시간.
    bool turning;
    float turningTime =0;
	// Use this for initialization
	void Start () {
        //플레이어 태그를로 플레이어 게임 오브젝트를 찾는다.
        player = GameObject.FindGameObjectWithTag("Player");
        
        offsetDir =  transform.position - player.transform.position;
        offsetDir.Normalize();
    }
	
	// Update is called once per frame
	void Update () {

        Debug.Log(turning);
        //Slerp를 이용해 처음 위치와 바뀔 위치를 사이를 이동한다.
        if (offsetDir != player.transform.forward)
        {
            offsetDir = player.transform.forward;
            turning = true;
        }

        if(turning)
        {
            turningTime += Time.deltaTime;
            transform.position = Vector3.Slerp(transform.position, player.transform.position - offsetDir * offset + height, Time.deltaTime * smoothRotate);
            if (turningTime > smoothRotate)
            {
                turning = false;
                turningTime = 0;
            }
        }
        else
            transform.position = player.transform.position - offsetDir * offset + height;
        transform.LookAt(player.transform);
        

    }
}
