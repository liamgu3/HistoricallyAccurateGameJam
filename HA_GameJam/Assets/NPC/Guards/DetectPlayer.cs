using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DetectPlayer : MonoBehaviour
{
	private float timer;
	private EventManager eventManager;

	private bool deathStrip;    //true if death strip guard
	private bool uniform;       //true if player is wearing uniform

	public GameObject restartButton;
	public GameObject exitButton;

    // Start is called before the first frame update
    void Start()
    {
		timer = 0.0f;
		eventManager = GameObject.Find("EventManager").GetComponent<EventManager>();
		deathStrip = transform.parent.GetComponent<NPC>().deathStrip;
    }

    // Update is called once per frame
    void Update()
    {
		
    }

	private void OnTriggerStay2D(Collider2D collision)
	{
		if (collision.tag == "Player")
		{
			if (deathStrip || !Player.uniform)		//should detect in death strip regardless, or if player is not in uniform
			{
				timer += Time.deltaTime;
				if (timer >= 2.5f)
				{
					eventManager.reasonText.GetComponent<Text>().text = "Anja was caught by the border guards and arrested.";
					StartCoroutine(eventManager.FadeToBlack(2.0f));
					StartCoroutine(eventManager.FadeInText(eventManager.gameOverText, 2.0f));
					StartCoroutine(eventManager.FadeInText(eventManager.reasonText, 2.0f));
					eventManager.restartButton.SetActive(true);
					eventManager.exitButton.SetActive(true);
				}
			}
		}
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.tag == "Player")
		{
			//timer = 0.0f;
		}
	}
}
