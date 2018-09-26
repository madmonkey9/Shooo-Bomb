using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombController : MonoBehaviour {

 
	void Start () {
	}
	
	void Update () {
        //디폴트로 전진하기
        iTween.MoveBy(this.gameObject, iTween.Hash("z", 1.1f,                              //z축 1.1 위치 까지
                                                        "easeType", iTween.EaseType.linear,     //일정한 속도로
                                                        "speed", 1.0f                           //1의 스피드로 이동
                                                        ));
		
	}
}
