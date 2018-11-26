using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleScript : MonoBehaviour {
    public ObstacleScript instance;

    float maintainTime;
    float speed;
    Transform tr;


    void Awake()
    {
        instance = this;
        tr = GetComponent<Transform>();

    }
    // Use this for initialization
    void Start () {
        speed = 5.0f;
	}
	
	// Update is called once per frame
	void Update () {
        tr.position -= new Vector3(0,0, speed * Time.deltaTime);
        maintainTime += Time.deltaTime;
        if(maintainTime > 2.0f)
        {
            Destroy(gameObject);
        }
	}


    private void OnTriggerEnter(Collider other)
    {
        
    }
}
