using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DetectPlayer : MonoBehaviour
{
	private float timer;
	private EventManager eventManager;

    // Start is called before the first frame update
    void Start()
    {
		timer = 0.0f;
		eventManager = GameObject.Find("EventManager").GetComponent<EventManager>();
    }

    // Update is called once per frame
    void Update()
    {
		
    }

	private void OnTriggerStay2D(Collider2D collision)
	{
		if (collision.tag == "Player")
		{
			timer += Time.deltaTime;
			if (timer >= 2.5f)
			{
				eventManager.reasonText.GetComponent<Text>().text = "Anja was caught by the border guards and arrested.";
				StartCoroutine(eventManager.FadeToBlack(2.0f));
				StartCoroutine(eventManager.FadeInText(eventManager.gameOverText, 2.0f));
				StartCoroutine(eventManager.FadeInText(eventManager.reasonText, 2.0f));
			}
		}
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.tag == "Player")
		{
			timer = 0.0f;
		}
	}
}
