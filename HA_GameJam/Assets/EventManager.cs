using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EventManager : MonoBehaviour
{
	public static bool hasMoney;
	public static bool hasUniform;
	public static bool hasGun;

	public static bool angeredMother;
	public static bool liedToMarkus;
	public static bool gotDistraction;

    // Start is called before the first frame update
    void Start()
    {
		hasMoney = hasUniform = hasGun = angeredMother = liedToMarkus = gotDistraction = false;
    }

    // Update is called once per frame
    void Update()
    {
		if (Input.GetKeyDown(KeyCode.P))
		{
			PrintValues();
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
		Debug.Log("AngeredMother: " + angeredMother);
		Debug.Log("LiedToMarkus: " + liedToMarkus);
		Debug.Log("GotDistraction: " + gotDistraction);
	}

	public bool HasMoney
	{
		get { return hasMoney; }
		set { hasMoney = value; }
	}

	public bool HasUniform
	{
		get { return hasUniform; }
		set 
		{
			hasUniform = value;
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

	public bool HasGun
	{
		get { return hasGun; }
		set 
		{ 
			hasGun = value;
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

	public void MarkusShot()
	{ 
		//needs to be filled in
	}

	public void AnjaArrested()
	{ 
		//needs to be filled in
	}

	public void AnjaRuns()
	{ 
		//needs to be filled in
	}
}
