using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class startTank : MonoBehaviour
{
	private bool inTrigger;
	public bool disableE;

    // Start is called before the first frame update
    void Start()
    {
		inTrigger = disableE = false;
    }

    // Update is called once per frame
    void Update()
    {
		if (inTrigger && !disableE && EventManager.hasTankKey)
		{
			if (Input.GetKeyDown(KeyCode.E))
			{
				GameObject.Find("EventManager").GetComponent<EventManager>().MoveTank();
			}
		}
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.tag == "Player")
		{
			inTrigger = true;
		}
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.tag == "Player")
		{
			inTrigger = false;
		}
	}
}
