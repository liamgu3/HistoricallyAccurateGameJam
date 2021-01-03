﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class MenuButtons : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	public void StartGame()
	{
		GetComponent<VideoPlayer>().Play();
		//SceneManager.LoadScene(1);
	}

	public void ExitGame()
	{
		Application.Quit();
	}
}
