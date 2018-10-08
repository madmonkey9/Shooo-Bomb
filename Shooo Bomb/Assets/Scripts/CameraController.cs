using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraConroller : MonoBehaviour
{
    public GameObject player;
    public float distance = 1;
    public Vector3 angle;
    //Vector3 offset;

    // Use this for initialization
    void Start()
    {
        // 캐릭터와 오프셋을 저장한다
        //offset = transform.position - player.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        // 저장한 오프셋을 반영한다.
        //transform.position = player.transform.position + offset;

        // 공의 진행방향이 바뀌면 카메라도 같이 바뀐다.
        //velocity는 속도여서 방향을 가지고 있다.
        Vector3 vel = player.GetComponent<Rigidbody>().velocity;
        //카메라는 player의 뒤쪽에 위치하므로 방향을 반대쪽으로 바꿔준다.
        Vector3 dir = -vel;
        //단위벡터로 만들기
        dir.Normalize();
        transform.position = player.transform.position + dir * distance + angle;
        transform.forward = player.transform.position - transform.position;

    }
}