using UnityEngine;
using System.Collections;

public class PurpleFaceBehavior : MonoBehaviour
{
    private bool isDead;
    private GameController controller;
    private Vector2 position;
    public float speed;
    private float spawnWave;
    public GameObject explosion;
    public GameObject shot1;
    private float shot1Time;
    private float health;
    private float lifeTime;
    private bool switchShot;
    // Use this for initialization
    void Awake()
    {
        controller = GameObject.Find("GameController").GetComponent<GameController>();
        spawnWave = controller.waveNumber;
        isDead = false;
        position = gameObject.transform.position;
        speed = 4f;
        shot1Time = .5f;
        health = 2000f;
        lifeTime = 6f;
        switchShot = true;
    }

    // Update is called once per frame
    void Update()
    {
        shot1Time -= Time.deltaTime;
        DestroyBoundary();
        if (controller.waveNumber == 2)
        {

            if (transform.position.y > 3 && lifeTime > 0)
            {
                transform.Translate(-Vector2.up * Time.deltaTime * speed);
            }
            /*if (transform.position.y <= 3)
            {
                lifeTime -= Time.deltaTime;
            }
            if (lifeTime <= 0)
            {
                transform.Translate(Vector2.up * Time.deltaTime * speed);
            }*/
            if (shot1Time <= 0 && lifeTime > 0 && !GameObject.Find("GameController").GetComponent<GameController>().PlayerRespawning())
            {
                Vector2 center = transform.position;
                if (switchShot)
                {
                    for (float i = -1f; i < 1f; i += 0.025f)
                    {
                        Vector2 ang = CreateCircle(1f, i * 360f);
                        GameObject phil = (GameObject)Instantiate(shot1, transform.position, gameObject.transform.rotation);
                        phil.GetComponent<PurpleBullet>().changeTrajectory(new Vector2(ang.x, ang.y));
                        switchShot = !switchShot;
                    }
                }
                else
                {
                    for (float i = -0.987f; i < 1.13f; i += 0.025f)
                    {
                        Vector2 ang = CreateCircle(1f, i * 360f);
                        GameObject phil = (GameObject)Instantiate(shot1, transform.position, gameObject.transform.rotation);
                        phil.GetComponent<PurpleBullet>().changeTrajectory(new Vector2(ang.x, ang.y));
                        switchShot = !switchShot;
                    }
                }
                shot1Time = .3f;
            }
        }
        if (controller.waveNumber > spawnWave)
        {
            Destroy(gameObject);
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Bullet" && !isDead && health > 0f)
        {
            health -= 1;
            Destroy(other.gameObject);
        }
        if (other.tag == "Bullet" && !isDead && health <= 0f)
        {
            Destroy(other.gameObject);
            Instantiate(explosion, new Vector2(gameObject.transform.position.x, gameObject.transform.position.y), gameObject.transform.rotation);
            isDead = true;
            Destroy(gameObject);
        }
    }
    void DestroyBoundary()
    {

        if (transform.position.y < -5.5f)
        {
            Destroy(gameObject);
        }
        if (transform.position.x > 4f || transform.position.x < -4f)
        {
            Destroy(gameObject);
        }
    }
    void Die()
    {
        Destroy(gameObject);
    }
    public Vector2 CreateCircle (float radius, float angle)
    {
        Vector2 pos;
        pos.x = radius * Mathf.Sin(angle * Mathf.Deg2Rad);
        pos.y = radius * Mathf.Cos(angle * Mathf.Deg2Rad);
        return pos;
    }
}
