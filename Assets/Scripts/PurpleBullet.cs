using UnityEngine;
using System.Collections;

public class PurpleBullet : MonoBehaviour
{
    private Vector2 PlayerPosition;
    private Vector2 BulletPosition;
    private float PlayerX;
    private float PlayerY;
    private Vector2 Trajectory;
    public float speed;
    // Use this for initialization
    void Awake()
    {
        speed = 3f;
        BulletPosition = gameObject.transform.position;
        PlayerX = BulletPosition.x;
        PlayerY = BulletPosition.y;
        Trajectory = new Vector2(PlayerX, PlayerY).normalized;
    }

    // Update is called once per frame
    void Update()
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
    public void changeTrajectory(Vector2 angle)
    {
        Trajectory = angle;
    }
}
