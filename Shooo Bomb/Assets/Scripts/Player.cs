using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    //plus속도(곱하기)
    public float add_speed;
    
    Rigidbody rb;
	// Use this for initialization
	void Start () {
        //Rigidbody라는 컴포넌트를 생성한다.
        rb = GetComponent<Rigidbody>();
	}

    // Update is called once per frame
    void Update ()
    {
        //player가 직진할 때의 움직임 구현. touch할 때 옆으로 이동
        if(Input.touchCount >0 && Input.GetTouch(0).position.x<Screen.width/2)
        {
            this.transform.Translate((Vector3.left + Vector3.forward) * add_speed * Time.deltaTime);
        }
        else if(Input.touchCount >0 && Input.GetTouch(0).position.x >=Screen.width/2)
        {
            this.transform.Translate((Vector3.right + Vector3.forward) * add_speed * Time.deltaTime);
        }
        else
        {
            this.transform.Translate(Vector3.forward * add_speed * Time.deltaTime);

        }
        //터치슬라이드를 했을때 양옆으로 90도 꺾기


        //전진하는 속도

	}
}
