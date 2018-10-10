using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    public static PlayerController instance;
    float h, v, t;
    int moveSpeed = 1;
    Quaternion toRotate;
    float rotateTime;
    bool rotating;
    float aimT;

    void Awake()
    {
        instance = this;
    }
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        
            if(Input.GetKeyDown(KeyCode.LeftArrow))
            {
                toRotate = Quaternion.LookRotation(-transform.right);
                rotating = true;
            }
                
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                toRotate = Quaternion.LookRotation(transform.right);
                rotating = true;
            }
                

        Move();
        Rotate();
        
	}

    void Rotate()
    {
        if(rotating == true)
        {
            rotateTime = Mathf.Clamp(rotateTime + Time.deltaTime, 0, 1);
            transform.rotation = Quaternion.Slerp(transform.rotation, toRotate, rotateTime);
        }

        if(transform.rotation == toRotate || rotateTime > 0.4f)
        {
            rotating = false;
            rotateTime = 0;
        }
    }

    void Move()
    {
        if(rotating == false)
        {
            transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
            rotateTime = 0;
        }
    }
}
