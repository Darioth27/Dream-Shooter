using UnityEngine;
using System.Collections;

public class PlayerBullets : MonoBehaviour {
    public float speed;
    // Use this for initialization
    void Start () {
       
    }
	
	// Update is called once per frame
	void Update () {
        transform.Translate(Vector2.up * speed);
        if (transform.position.y > 5f)
            {
            Destroy(gameObject);
        }
    }
}
