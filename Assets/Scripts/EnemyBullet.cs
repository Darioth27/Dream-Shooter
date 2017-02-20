using UnityEngine;
using System.Collections;

public class EnemyBullet : MonoBehaviour {
    private Vector2 PlayerPosition;
    private Vector2 BulletPosition;
    private float PlayerX;
    private float PlayerY;
    private Vector2 Trajectory;
    public float speed;
	// Use this for initialization
	void Awake ()
    {
        speed = Random.Range(4f, 6f);
        BulletPosition = gameObject.transform.position;
        if (!GameObject.Find("GameController").GetComponent<GameController>().playerDead() || GameObject.Find("GameController").GetComponent<GameController>().PlayerRespawning())
        {
            PlayerPosition = GameObject.FindGameObjectWithTag("Player").transform.position;
            PlayerX = PlayerPosition.x;
            PlayerY = PlayerPosition.y;
            Trajectory = new Vector2(PlayerPosition.x - transform.position.x, PlayerPosition.y - transform.position.y).normalized;
        }
        else
        {
            Trajectory = Vector2.down;
        }
        
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (GameObject.Find("GameController").GetComponent<GameController>().PlayerRespawning())
        {
            Destroy(gameObject);
        }
        transform.Translate(Trajectory * Time.deltaTime * speed);
        if (transform.position.y > 5f || transform.position.y < -5f)
        {
            Destroy(gameObject);
        }
        if (transform.position.x > 5f || transform.position.x < -5f)
        {
            Destroy(gameObject);
        }
    }
}
