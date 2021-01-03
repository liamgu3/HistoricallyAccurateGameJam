using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class MenuButtons : MonoBehaviour
{
	private bool videoStarted;
	private VideoPlayer video;

	public GameObject background;
	public GameObject start;
	public GameObject exit;
	public GameObject skip;

	public GameObject videoObject;

    // Start is called before the first frame update
    void Start()
    {
		videoStarted = false;
		video = GetComponent<VideoPlayer>();
		video.url = System.IO.Path.Combine(Application.streamingAssetsPath, "HA-Intro.mp4");
		//video.Prepare();
	}

    // Update is called once per frame
    void Update()
    {
		if (videoObject.GetComponent<VideoPlayer>().isPlaying && !videoStarted)
		{
			videoStarted = true;
		}

		if (videoStarted)
		{
			if (!videoObject.GetComponent<VideoPlayer>().isPlaying)
			{
				SceneManager.LoadScene(1);
			}
		}

	}

	public void StartGame()
	{
		//GameObject.Find("Canvas").SetActive(false);
		GetComponent<AudioSource>().Stop();
		videoObject.SetActive(true);
		ChangeUI();
	}

	public void ExitGame()
	{
		Application.Quit();
	}

	public void ChangeUI()
	{
		background.SetActive(false);
		start.SetActive(false);
		exit.SetActive(false);
		skip.SetActive(true);
	}

	public void Skip()
	{
		videoObject.GetComponent<VideoPlayer>().Stop();
	}
}
