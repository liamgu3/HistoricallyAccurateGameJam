using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class showUniformButton : MonoBehaviour
{
	public GameObject uniformButton;

    // Start is called before the first frame update
    void Start()
    {
		if (EventManager.hasUniform)
			uniformButton.SetActive(true);
		else
			uniformButton.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
