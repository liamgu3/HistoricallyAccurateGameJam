using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	//used for choosing which animation to use
	public Animator animator;
	private int direction;

	private float speed;    //speed that character moves at
	public bool movementPause;	//prevents movement during dialogue

    // Start is called before the first frame update
    void Start()
    {
		animator = GetComponent<Animator>();
		direction = 0;

		speed = .05f;
	}

	// Update is called once per frame
	void Update()
    {
		
	}

	private void FixedUpdate()
	{
		if (!movementPause)
		{
			Movement();
		}
	}

	//moves player and assigns correct animation
	private void Movement()
	{
		bool vertMovement = true;
		bool horzMovement = true;

		if (Input.GetKey(KeyCode.S))    //walking down
		{
			transform.position = new Vector2(transform.position.x, transform.position.y - speed);
			direction = 0;
		}
		else if (Input.GetKey(KeyCode.W))   //walking up
		{
			transform.position = new Vector2(transform.position.x, transform.position.y + speed);
			direction = 1;
		}
		else
		{
			vertMovement = false;
		}

		if (Input.GetKey(KeyCode.D))    //walking right
		{
			transform.position = new Vector2(transform.position.x + speed, transform.position.y);
			direction = 2;
			transform.localScale = new Vector3(-1, 1, 1);
		}
		else if (Input.GetKey(KeyCode.A))   //walking left
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

		//if (Input.GetKeyUp(KeyCode.D))	//prevents brief animation flip when player stops moving right
		//{
		//	transform.localScale = new Vector3(1, 1, 1);
		//}



		animator.SetInteger("Direction", direction);
		animator.SetBool("Moving", vertMovement || horzMovement);
	}
}
