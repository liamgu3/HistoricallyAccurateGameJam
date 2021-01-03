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
	private int direction;

	private Animator animator;

	public GameObject flashlight;
	private AudioSource footsteps;
	public bool deathStrip; //true if a deathstrip guard

	public bool disableNewConversation = false;

    // Start is called before the first frame update
    void Start()
    {
		inTrigger = false;

		currentTarget = 1;
		pathDirection = true;
		speed = .0175f;

		if (moving)
		{
			animator = gameObject.GetComponent<Animator>();
			footsteps = GetComponent<AudioSource>();
		}

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
		if (inTrigger && !disableNewConversation)
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
		bool vertMovement = true;
		bool horzMovement = true;

		if (targets[currentTarget].transform.position.y < transform.position.y - .1f)    //walking down
		{
			transform.position = new Vector2(transform.position.x, transform.position.y - speed);
			direction = 0;
		}
		else if (targets[currentTarget].transform.position.y > transform.position.y + .1f)   //walking up
		{
			transform.position = new Vector2(transform.position.x, transform.position.y + speed);
			direction = 1;
		}
		else
		{
			vertMovement = false;
		}

		if (targets[currentTarget].transform.position.x > transform.position.x + .1f)    //walking right
		{
			transform.position = new Vector2(transform.position.x + speed, transform.position.y);
			direction = 2;
			transform.localScale = new Vector3(-1, 1, 1);
		}
		else if (targets[currentTarget].transform.position.x < transform.position.x - .1f)   //walking left
		{
			transform.position = new Vector2(transform.position.x - speed, transform.position.y);
			direction = 3;
			animator.SetBool("Mirror", false);
			transform.localScale = new Vector3(1, 1, 1);
		}
		else
		{
			horzMovement = false;
		}

		if (direction == 0)
			flashlight.transform.eulerAngles = new Vector3(0f, 0f, 180f);
		if (direction == 1)
			flashlight.transform.eulerAngles = new Vector3(0f, 0f, 0f);
		if (direction == 2)
			flashlight.transform.eulerAngles = new Vector3(0f, 0f, 270f);
		if (direction == 3)
			flashlight.transform.eulerAngles = new Vector3(0f, 0f, 90f);

		if (vertMovement || horzMovement)
		{
			if (!footsteps.isPlaying)
			{
				footsteps.Play();
			}
		}


		animator.SetInteger("Direction", direction);
		animator.SetBool("Moving", vertMovement || horzMovement);

		UpdateTarget();
	}

	private void UpdateTarget()
	{
		

		if (Vector2.Distance(transform.position, targets[currentTarget].transform.position) < .15f)
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
