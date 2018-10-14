using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour {
    //게임 오브젝트 player 
    public GameObject player;
    //왼쪽으로 턴하려고할때
    bool leftkeydown;
    //오른쪽으로 턴하려고할때
    bool rightkeydown;
    //카메라의 방향
    Vector3 offsetDir;
    //player와 카메라의 거리
    public float offset;
	// Use this for initialization
	void Start () {
        offsetDir = transform.position - player.transform.position;
        offsetDir.Normalize();
        leftkeydown = false;
        rightkeydown = false;
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = player.transform.position + offsetDir * offset;
        if (leftkeydown)
        {
            //카메라의 방향을 -90도로 돌려준다.
            offsetDir = Quaternion.Euler(0, -90, 0) * offsetDir;
            transform.position = player.transform.position + offsetDir * offset;
            transform.LookAt(player.transform);
            leftkeydown = false;
        }
        else if (rightkeydown)
        {
            //카메라의 방향을 90도 돌려준다.
            offsetDir = Quaternion.Euler(0, 90, 0) * offsetDir;
            transform.position = player.transform.position +  offsetDir * offset;
            transform.LookAt(player.transform);
            rightkeydown = false;
        }


        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            leftkeydown = true;
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            rightkeydown = true;
        }



    }
}
