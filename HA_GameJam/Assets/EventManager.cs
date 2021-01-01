using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EventManager : MonoBehaviour
{
	public static bool hasMoney;
	public static bool hasUniform;
	public static bool hasGun;
	public static bool hasCigarettes;

	public static bool angeredMother;
	public static bool liedToMarkus;
	public static bool gotDistraction;

    // Start is called before the first frame update
    void Start()
    {
		hasMoney = hasUniform = hasGun = hasCigarettes = angeredMother = liedToMarkus = gotDistraction = false;
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

	public void GuardShot()
	{ 
		//needs to be filled in
	}
}
