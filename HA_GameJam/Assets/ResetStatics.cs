using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetStatics : MonoBehaviour
{
	private void Awake()
	{
		//resetting all static variables

		EventManager.hasMoney = EventManager.hasUniform = EventManager.hasGun = EventManager.hasCigarettes = EventManager.hasTankKey = EventManager.liedToMarkus = EventManager.gotDistraction = Player.uniform = false;
		SceneEntrance.mainScenePosition = Vector2.zero;
		Player.twoLoad = 0;
	}

	// Start is called before the first frame update
	void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
