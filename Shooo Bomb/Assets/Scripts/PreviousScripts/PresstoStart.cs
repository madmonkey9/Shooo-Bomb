using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PresstoStart : MonoBehaviour {

	public UnityEngine.GameObject StartUI;
	private bool paused = true;
    
	void Start () {
		StartUI.SetActive(false);

	}


	void Update () {
		if(Input.GetButtonDown("Start"))
		{
			paused = !paused;
		}
        if(paused)
		{
			StartUI.SetActive(true);
			Time.timeScale = 0.001f;
		}
        if(!paused)
		{
			StartUI.SetActive(false);
			Time.timeScale = 1f;
		}
		
	}
}
