using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FitColliderToSprite : MonoBehaviour
{
	//this script automatically sets an objects collider to the size of its sprite

	private SpriteRenderer renderer;    //the object's sprite renderer
	private BoxCollider2D collider;		//the object's box collider

    // Start is called before the first frame update
    void Start()
    {
		renderer = GetComponent<SpriteRenderer>();
		collider = GetComponent<BoxCollider2D>();

		collider.size = renderer.bounds.size;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
