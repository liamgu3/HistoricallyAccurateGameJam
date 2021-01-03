using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GiveTankKey : MonoBehaviour
{
	private bool inTrigger;
	private bool disableE;

	public GameObject tankKeyText;
	public GameObject interactIcon;

	// Start is called before the first frame update
	void Start()
    {
		inTrigger = disableE = false;
    }

    // Update is called once per frame
    void Update()
    {
		if (inTrigger && !disableE)
		{
			if (Input.GetKeyDown(KeyCode.E))
			{
				disableE = true;
				EventManager.hasTankKey = true;
				StartCoroutine(FadeInText(tankKeyText, 4.0f));
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

	IEnumerator FadeInText(GameObject text, float time)
	{
		float alpha = text.GetComponent<Text>().color.a;

		for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / time)
		{
			Color newColor = new Color(0.4244997f, 0.2710039f, 0.5471698f, Mathf.Lerp(alpha, 1.0f, t));
			text.GetComponent<Text>().color = newColor;
			yield return null;
		}

		for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / time)
		{
			Color newColor = new Color(0, 0, 0, Mathf.Lerp(alpha, 0.0f, t));
			text.GetComponent<Text>().color = newColor;
			yield return null;
		}
	}
}
