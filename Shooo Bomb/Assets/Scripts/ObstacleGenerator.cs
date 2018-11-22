using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleGenerator : MonoBehaviour {
    public ObstacleGenerator instance;
    public GameObject obstaclePrefab1;
    public GameObject obstaclePrefab2;
    public GameObject obstaclePrefab3;
    public GameObject obstaclePrefab4;
    float generateTime;
    float createTime;

    void Awake()
    {
        instance = this;    
    }
    // Use this for initialization
    void Start () {
        createTime = Random.RandomRange(0, 3);
	}
	
	// Update is called once per frame
	void Update () {
        generateTime += Time.deltaTime;
        if(generateTime > createTime)
        {
            createTime = Random.RandomRange(0, 3);
            generateTime = 0.0f;
            GameObject obstacle = Instantiate(obstaclePrefab1);
            obstacle.transform.position = instance.transform.position;
            
        }
	}
}
