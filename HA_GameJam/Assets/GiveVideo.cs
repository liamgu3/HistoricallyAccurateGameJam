using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class GiveVideo : MonoBehaviour
{
	private VideoPlayer video;

    // Start is called before the first frame update
    void Start()
    {
		video = GetComponent<VideoPlayer>();
		video.url = System.IO.Path.Combine(Application.streamingAssetsPath, "HA-Intro.mp4");
		gameObject.SetActive(false);
	}

    // Update is called once per frame
    void Update()
    {
        
    }
}
