using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    //Player's Transform
    public Transform obj;

    //Camera's position between player.
    float x, y, z;
    //공의 방향을 저장하기 위한 변수
    int Mode;
    // Use this for initialization
    void Start()
    {
        Mode = 0;

        x = transform.position.x - obj.position.x;
        y = transform.position.y - obj.position.y;
        z = transform.position.z - obj.position.z;
    }

    // Update is called once per frame
    void Update()
    {
        //모드를 4 -> 0 으로 바꾸기 위함
        if (Mode > 3)
        {
            Mode = Mode % 4;
        }

        //모드를 음수에서 양수로 바꾸기 위함
        if (Mode < 0)
        {
            Mode = Mode + 4;
        }

        // 현재 모드를 통해 카메라 위치를 이동시키기 위함
        switch (Mode)
        {
            // 모드 0는 Z가 증가하는 방향
            // 카메라의 위치는 Z가 음수인 방향에 있고
            // 카메라는 Z가 양수인 방향을 바라보고 있어야 한다
            case 0:
                transform.position =
                    Vector3.Lerp(transform.position,
                    obj.transform.position + new Vector3(x, y, z),
                    Time.deltaTime * 3);
                transform.LookAt(obj);
                break;
            // 모드 1은 X가 감소하는 방향
            // 카메라의 위치는 X가 양수인 방향에 있고
            // 카메라는 X가 음수인 방향을 바라보고 있어야 한다
            case 1:
                transform.position =
                    Vector3.Lerp(transform.position,
                    obj.transform.position + new Vector3(-z, y, x),
                    Time.deltaTime * 3);
                transform.LookAt(obj);
                break;
            // 모드 2는 Z가 감소하는 방향
            // 카메라의 위치는 Z가 양수인 방향에 있고
            // 카메라는 Z가 음수인 방향을 바라보고 있어야 한다
            case 2:
                // 카메라 부드럽게 이동
                transform.position =
                    Vector3.Lerp(transform.position,
                    obj.transform.position + new Vector3(-x, y, -z),
                    Time.deltaTime * 3);
                // Player 카메라 바라보기
                transform.LookAt(obj);
                break;
            // 모드 3은 X가 증가하는 방향
            // 카메라의 위치는 X가 음수인 방향에 있고
            // 카메라는 X가 양수인 방향을 바라보고 있어야 한다
            case 3:
                transform.position =
                    Vector3.Lerp(transform.position,
                    obj.transform.position + new Vector3(z, y, -x),
                    Time.deltaTime * 3);
                transform.LookAt(obj);
                break;
        }

        // 왼쪽 방향키를 입력 받았을 때
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            Mode += 1;
        }

        // 오른쪽 방향키를 입력 받았을 때
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            Mode -= 1;
        }

    }
}