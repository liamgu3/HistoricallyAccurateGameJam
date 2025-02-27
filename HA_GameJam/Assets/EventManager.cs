﻿using System.Collections;
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
	public static bool hasTankKey;

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
	private static float timer;
	public Text timerObject;

	public Sprite deadGuard;
	public Sprite deadMarkus;

	public GameObject tank;
	public GameObject tankTarget;

	public GameObject uniformButton;
	public GameObject restartButton;
	public GameObject exitButton;

	public GameObject cigUI;
	public GameObject gunUI;
	public GameObject keyUI;
	public GameObject moneyUI;
	public GameObject uniformUI;

	// Start is called before the first frame update
	void Start()
	{
		//hasMoney = hasUniform = hasGun = hasCigarettes = hasTankKey = angeredMother = liedToMarkus = gotDistraction = false;
		startTimer = false;
		timer = 300.0f;
		CorrectUI();
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
			moneyUI.SetActive(hasMoney);

			if (SceneManager.GetActiveScene().name == "MainScene")
			{
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
	}

	public bool HasUniform
	{
		get { return hasUniform; }
		set
		{
			hasUniform = value;
			uniformButton.SetActive(true);
			uniformUI.SetActive(hasUniform);
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
			gunUI.SetActive(HasGun);

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

	public bool HasTankKey
	{
		get { return hasTankKey; }
		set 
		{ 
			hasTankKey = value;
			keyUI.SetActive(hasTankKey);
		}
	}

	public bool HasCigarettes
	{
		get { return hasCigarettes; }
		set
		{
			hasCigarettes = value;
			cigUI.SetActive(HasCigarettes);
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
		restartButton.SetActive(true);
		exitButton.SetActive(true);
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

	public void MoveTank()
	{
		GameObject player = GameObject.Find("Player");
		player.GetComponent<Player>().movementPause = true;
		tank.GetComponent<startTank>().disableE = true;
		player.transform.position = tank.transform.position;
		player.transform.parent = tank.gameObject.transform;
		player.layer = 9;
		StartCoroutine(MoveFromTo(tank.transform, tank.transform.position, tankTarget.transform.position, 3.0f));
		Invoke("UnParentPlayer", 10f);
	}

	private void UnParentPlayer()
	{
		GameObject.Find("Tank").GetComponent<AudioSource>().Stop();
		GameObject player = GameObject.Find("Player");
		player.transform.parent = null;
		player.GetComponent<Player>().movementPause = false;
		player.layer = 0;
	}

	public IEnumerator FadeToBlack(float time)
	{
		float alpha = blackScreen.GetComponent<Image>().color.a;

		for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / time)
		{
			Color newColor = new Color(1, 1, 1, Mathf.Lerp(alpha, 1.0f, t));
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

	IEnumerator MoveFromTo(Transform objectToMove, Vector3 a, Vector3 b, float speed)
	{
		float step = (speed / (a - b).magnitude) * Time.fixedDeltaTime;
		float t = 0;
		while (t <= 1.0f)
		{
			t += step; // Goes from 0 to 1, incrementing by step each time
			objectToMove.position = Vector3.Lerp(a, b, t); // Move objectToMove closer to b
			yield return new WaitForFixedUpdate();         // Leave the routine and return here in the next frame
		}
		objectToMove.position = b;
	}

	private void CorrectUI()
	{
		cigUI.SetActive(hasCigarettes);
		gunUI.SetActive(hasGun);
		keyUI.SetActive(hasTankKey);
		moneyUI.SetActive(hasMoney);
		uniformUI.SetActive(HasUniform);
	}
}
