using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneEntrance : MonoBehaviour
{
	private bool inTrigger;
	public string scene;
	public static Vector2 mainScenePosition;
	private GameObject player;
	public GameObject interactIcon;

    // Start is called before the first frame update
    void Start()
    {
		player = GameObject.Find("Player");
		interactIcon.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
		if (inTrigger)
		{
			if (Input.GetKeyDown(KeyCode.E))
			{
				if (SceneManager.GetActiveScene().name == "MainScene")
					mainScenePosition = player.transform.position;

				EventManager.LoadScene(scene);
			}
		}
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.tag == "Player")
		{
			inTrigger = true;
			interactIcon.SetActive(true);
		}
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.tag == "Player")
		{
			inTrigger = false;
			interactIcon.SetActive(false);
		}
	}
}
