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

    // Start is called before the first frame update
    void Start()
    {
		videoStarted = false;
		video = GetComponent<VideoPlayer>();
		video.url = System.IO.Path.Combine(Application.streamingAssetsPath, "HA-Outro.mp4");
	}

    // Update is called once per frame
    void Update()
    {
		if (videoStarted)
		{
			if (!video.isPlaying)
			{
				SceneManager.LoadScene(1);
			}
		}

    }

	public void StartGame()
	{
		//GameObject.Find("Canvas").SetActive(false);
		GetComponent<AudioSource>().Stop();
		video.Play();
		videoStarted = true;
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
		video.Stop();
	}
}
