using UnityEngine;
using System.Collections;

public class GameStarter : MonoBehaviour {

	// Update is called once per frame
	void Update () {
	    
	}

    public void startGame ()
    {
        Application.LoadLevel(1);
    }

}
