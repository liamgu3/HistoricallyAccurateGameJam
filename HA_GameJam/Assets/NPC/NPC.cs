using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogueEditor;
using UnityEngine.SceneManagement;

public class NPC : MonoBehaviour
{
	public NPCConversation myConversation;
	private bool inTrigger; //detects if player is next to npc or not

	public bool moving;     //if true, than npc can move
	public bool movementPaused;
	public GameObject[] targets;    //positions for npcs to move to
	private int currentTarget;
	private bool pathDirection;		//whether path is being walked 0 to finish or in reverse, true is forward, false is reverse
	private float speed;

    // Start is called before the first frame update
    void Start()
    {
		inTrigger = false;

		currentTarget = 1;
		pathDirection = true;
		speed = .01f;

		if (SceneManager.GetActiveScene().name == "MainScene")
		{
			if (gameObject.name == "Kurt")
			{
				if (!EventManager.hasMoney) //Kurt will offer money if player didn't get it
				{
					myConversation = transform.Find("conversationWithNoMoney").GetComponent<DialogueEditor.NPCConversation>();
				}
			}
			if (gameObject.name == "Markus")
			{
				if (EventManager.liedToMarkus)  //Markus will confront player if they lied to him earlier
				{
					if (EventManager.angeredMother)
					{
						myConversation = transform.Find("conversationAngeredMother").GetComponent<DialogueEditor.NPCConversation>();
					}
					else
					{
						myConversation = transform.Find("conversationNormalMother").GetComponent<DialogueEditor.NPCConversation>();
					}
				}
				else
				{
					gameObject.SetActive(false);
				}
			}
		}
    }

    // Update is called once per frame
    void Update()
    {
		if (inTrigger)
		{
			if (Input.GetKeyDown(KeyCode.E))
			{
				ConversationManager.Instance.StartConversation(myConversation, this);
			}
		}

		if (moving && !movementPaused)
		{
			Movement();
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

	private void Movement()
	{
		if (targets[currentTarget].transform.position.y < transform.position.y)    //walking down
		{
			transform.position = new Vector2(transform.position.x, transform.position.y - speed);
			//direction = 0;
		}
		else if (targets[currentTarget].transform.position.y > transform.position.y)   //walking up
		{
			transform.position = new Vector2(transform.position.x, transform.position.y + speed);
			//direction = 1;
		}

		if (targets[currentTarget].transform.position.x > transform.position.x)    //walking right
		{
			transform.position = new Vector2(transform.position.x + speed, transform.position.y);
			//direction = 2;
		}
		else if (targets[currentTarget].transform.position.x < transform.position.x)   //walking up
		{
			transform.position = new Vector2(transform.position.x - speed, transform.position.y);
			//direction = 3;
		}

		//animator.SetInteger("Direction", direction);

		UpdateTarget();
	}

	private void UpdateTarget()
	{
		Debug.Log(currentTarget);

		if (Vector2.Distance(transform.position, targets[currentTarget].transform.position) < .1f)
		{
			if (pathDirection)
			{
				currentTarget++;
				if (currentTarget + 1 == targets.Length)
				{
					pathDirection = false;
				}
			}
			else
			{
				currentTarget--;
				if (currentTarget == 0)
				{
					pathDirection = true;
				}
			}
		}
	}
}
