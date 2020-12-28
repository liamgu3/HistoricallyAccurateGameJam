using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogueEditor;

public class NPC : MonoBehaviour
{
	public NPCConversation myConversation;

	private bool inTrigger;	//detects if player is next to npc or not

    // Start is called before the first frame update
    void Start()
    {
		inTrigger = false;
    }

    // Update is called once per frame
    void Update()
    {
		if (inTrigger)
		{
			if (Input.GetKeyDown(KeyCode.E))
			{
				ConversationManager.Instance.StartConversation(myConversation);
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
