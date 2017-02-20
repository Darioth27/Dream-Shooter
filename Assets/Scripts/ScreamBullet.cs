using UnityEngine;
using System.Collections;

public class ScreamBullet : MonoBehaviour
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
        speed = Random.Range(3f, 5f);
        BulletPosition = gameObject.transform.position;
        PlayerX = BulletPosition.x + Random.Range(-3f, 3f);
        PlayerY = BulletPosition.y - 1f;
        Trajectory = new Vector2(PlayerX, PlayerY).normalized;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameObject.Find("GameController").GetComponent<GameController>().PlayerRespawning())
        {
            Destroy(gameObject);
        }
        transform.Translate(Trajectory * Time.deltaTime * -speed);
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
