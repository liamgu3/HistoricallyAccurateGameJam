using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;

public class EventManager : MonoBehaviour
{
	public static bool hasMoney;
	public static bool hasUniform;
	public static bool hasGun;
	public static bool hasCigarettes;

	public static bool angeredMother;
	public static bool liedToMarkus;
	public static bool gotDistraction;

	//UI elements
	public GameObject blackScreen;
	public GameObject gameOverText;
	public GameObject reasonText;
	public GameObject closedGate;
	public GameObject openGate;

	private bool startTimer;
	private float timer;
	public Text timerObject;

	public Sprite deadGuard;
	public Sprite deadMarkus;

	// Start is called before the first frame update
	void Start()
	{
		hasMoney = hasUniform = hasGun = hasCigarettes = angeredMother = liedToMarkus = gotDistraction = false;
		startTimer = false;
		timer = 300.0f;
	}

	// Update is called once per frame
	void Update()
	{
		if (Input.GetKeyDown(KeyCode.P))
		{
			PrintValues();
		}

		if (startTimer)
		{
			timer -= Time.deltaTime;
			var ts = TimeSpan.FromSeconds(timer);
			timerObject.text = string.Format("{0:00}:{1:00}", ts.Minutes, ts.Seconds);
			if (timer <= 0.0f)
			{
				reasonText.GetComponent<Text>().text = "Anja was caught by the Stasi and arrested.";
				StartCoroutine(FadeToBlack(2.0f));
				StartCoroutine(FadeInText(gameOverText, 2.0f));
				StartCoroutine(FadeInText(reasonText, 2.0f));
			}
		}
	}

	public static void LoadScene(string scene)
	{
		SceneManager.LoadScene(scene);
	}

	public void PrintValues()
	{
		Debug.Log("Money: " + hasMoney);
		Debug.Log("Uniform: " + hasUniform);
		Debug.Log("Gun: " + hasGun);
		Debug.Log("Cigarettes: " + hasCigarettes);
		Debug.Log("AngeredMother: " + angeredMother);
		Debug.Log("LiedToMarkus: " + liedToMarkus);
		Debug.Log("GotDistraction: " + gotDistraction);
	}

	public bool HasMoney
	{
		get { return hasMoney; }
		set
		{
			hasMoney = value;

			if (hasMoney)
			{
				GameObject.Find("StoreOwner").GetComponent<NPC>().myConversation = GameObject.Find("StoreOwner").transform.Find("conversationBuyCigs").GetComponent<DialogueEditor.NPCConversation>();
			}
			else
			{
				GameObject.Find("StoreOwner").GetComponent<NPC>().myConversation = GameObject.Find("StoreOwner").transform.Find("conversationNoBuyCigs").GetComponent<DialogueEditor.NPCConversation>();
			}
		}
	}

	public bool HasUniform
	{
		get { return hasUniform; }
		set
		{
			hasUniform = value;
			if (liedToMarkus)
			{
				if (EventManager.angeredMother)
				{
					GameObject.Find("Markus").GetComponent<NPC>().myConversation = GameObject.Find("Markus").transform.Find("conversationAngeredMotherContraband").GetComponent<DialogueEditor.NPCConversation>();
				}
				else
				{
					GameObject.Find("Markus").GetComponent<NPC>().myConversation = GameObject.Find("Markus").transform.Find("conversationNormalMotherContraband").GetComponent<DialogueEditor.NPCConversation>();
				}
			}
		}
	}

	public bool HasGun
	{
		get { return hasGun; }
		set
		{
			hasGun = value;

			if (liedToMarkus)
			{
				if (EventManager.angeredMother)
				{
					GameObject.Find("Markus").GetComponent<NPC>().myConversation = GameObject.Find("Markus").transform.Find("conversationAngeredMotherContraband").GetComponent<DialogueEditor.NPCConversation>();
				}
				else
				{
					GameObject.Find("Markus").GetComponent<NPC>().myConversation = GameObject.Find("Markus").transform.Find("conversationNormalMotherContraband").GetComponent<DialogueEditor.NPCConversation>();
				}
			}

			GameObject.Find("BorderGuard").GetComponent<NPC>().myConversation = GameObject.Find("BorderGuard").transform.Find("conversationNoCigs").GetComponent<DialogueEditor.NPCConversation>();
		}
	}
	public bool AngeredMother
	{
		get { return angeredMother; }
		set { angeredMother = value; }
	}

	public bool LiedToMarkus
	{
		get { return liedToMarkus; }
		set { liedToMarkus = value; }
	}

	public bool GotDistraction
	{
		get { return gotDistraction; }
		set { gotDistraction = value; }
	}

	public bool HasCigarettes
	{
		get { return hasCigarettes; }
		set
		{
			hasCigarettes = value;
			if (EventManager.hasGun)
			{
				if (EventManager.hasCigarettes)
				{
					GameObject.Find("BorderGuard").GetComponent<NPC>().myConversation = GameObject.Find("BorderGuard").transform.Find("conversationBoth").GetComponent<DialogueEditor.NPCConversation>();
				}
				else
				{
					GameObject.Find("BorderGuard").GetComponent<NPC>().myConversation = GameObject.Find("BorderGuard").transform.Find("conversationNoCigs").GetComponent<DialogueEditor.NPCConversation>();
				}
			}
			else
			{
				if (EventManager.hasCigarettes)
				{
					GameObject.Find("BorderGuard").GetComponent<NPC>().myConversation = GameObject.Find("BorderGuard").transform.Find("conversationNoGun").GetComponent<DialogueEditor.NPCConversation>();
				}
				else
				{
					GameObject.Find("BorderGuard").GetComponent<NPC>().myConversation = GameObject.Find("BorderGuard").transform.Find("conversationNoGunNoCigs").GetComponent<DialogueEditor.NPCConversation>();
				}
			}
		}
	}

	public void DistractionMade()
	{
		GameObject.Find("Guard1").SetActive(false);
		GameObject.Find("Guard3").SetActive(false);
		GameObject.Find("Guard5").SetActive(false);
		GameObject.Find("Guard7").SetActive(false);
	}

	public void MarkusShot()
	{
		GetComponent<AudioSource>().Play();
		GameObject.Find("Markus").GetComponent<SpriteRenderer>().sprite = deadMarkus;
	}

	public void AnjaArrested()
	{
		reasonText.GetComponent<Text>().text = "Anja was arrested.";

		StartCoroutine(FadeToBlack(2.0f));
		StartCoroutine(FadeInText(gameOverText, 2.0f));
		StartCoroutine(FadeInText(reasonText, 2.0f));
	}

	public void AnjaRuns()
	{
		startTimer = true;
		timerObject.gameObject.SetActive(true);
	}

	public void GuardShot()
	{
		GetComponent<AudioSource>().Play();
		GameObject.Find("BorderGuard").GetComponent<SpriteRenderer>().sprite = deadGuard;
	}

	public void OpenGate()
	{
		closedGate.SetActive(false);
		openGate.SetActive(true);
	}

	public IEnumerator FadeToBlack(float time)
	{
		float alpha = blackScreen.GetComponent<Image>().color.a;

		for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / time)
		{
			Color newColor = new Color(0, 0, 0, Mathf.Lerp(alpha, 1.0f, t));
			blackScreen.GetComponent<Image>().color = newColor;
			yield return null;
		}
	}

	public IEnumerator FadeInText(GameObject text, float time)
	{
		float alpha = text.GetComponent<Text>().color.a;

		for (float t = 0.0f; t< 1.0f; t += Time.deltaTime / time)
		{
			Color newColor = new Color(1, 1, 1, Mathf.Lerp(alpha, 1.0f, t));
			text.GetComponent<Text>().color = newColor;
			yield return null;
		}
	}
}
