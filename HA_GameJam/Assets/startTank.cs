using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class startTank : MonoBehaviour
{
	private bool inTrigger;
	public bool disableE;
	public GameObject interactIcon;

    // Start is called before the first frame update
    void Start()
    {
		inTrigger = disableE = false;
		interactIcon.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
		if (inTrigger && !disableE && EventManager.hasTankKey)
		{
			if (Input.GetKeyDown(KeyCode.E))
			{
				GameObject.Find("EventManager").GetComponent<EventManager>().MoveTank();
				GetComponent<AudioSource>().Play();
			}
		}
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.tag == "Player")
		{
			inTrigger = true;
			if(EventManager.hasTankKey)
				interactIcon.SetActive(true);
		}
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.tag == "Player")
		{
			inTrigger = false;
			if(EventManager.hasTankKey)
				interactIcon.SetActive(false);
		}
	}
}
