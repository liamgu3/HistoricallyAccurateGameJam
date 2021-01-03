using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class ReachedExit : MonoBehaviour
{
	public GameObject camera1;
	public GameObject blackScreen;
	public GameObject restart;
	public GameObject exit;
	private VideoPlayer video;
	
    // Start is called before the first frame update
    void Start()
    {
        video = GetComponent<VideoPlayer>();
		video.url = System.IO.Path.Combine(Application.streamingAssetsPath, "HA-Outro.mp4");
	}

    // Update is called once per frame
    void Update()
    {
        
    }

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.tag == "Player")
		{
			camera1.GetComponents<AudioSource>()[0].Stop();
			camera1.GetComponents<AudioSource>()[1].Stop();
			GameObject.Find("Player").GetComponent<Player>().movementPause = true;
			video.Play();
			ChangeUI();
		}
	}

	public void ChangeUI()
	{
		Transform canvas1 = GameObject.Find("Canvas").transform;

		for (int i = 0; i < canvas1.childCount; i++)
		{
			canvas1.GetChild(i).gameObject.SetActive(false);
		}

		blackScreen.SetActive(true);
		restart.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, -250, 0);
		exit.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, -350, 0);
		restart.SetActive(true);
		exit.SetActive(true);
	}
}
