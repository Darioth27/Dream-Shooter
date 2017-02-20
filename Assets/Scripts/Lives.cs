using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Lives : MonoBehaviour {
    private GameController controller;
    private Text text;
	// Use this for initialization
	void Start ()
    {
        controller = GameObject.Find("GameController").GetComponent<GameController>();
        text = GetComponent<Text>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        text.text = "x" + controller.lives();
	}
}
