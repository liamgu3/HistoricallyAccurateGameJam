using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectPlayer : MonoBehaviour
{
	private float timer;

    // Start is called before the first frame update
    void Start()
    {
		timer = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
		Debug.Log(timer);
    }

	private void OnTriggerStay2D(Collider2D collision)
	{
		if (collision.tag == "Player")
		{
			timer += Time.deltaTime;
			if (timer >= 3.0f)
			{
				Debug.Log("Player Caught");
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
