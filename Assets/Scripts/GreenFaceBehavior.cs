using UnityEngine;
using System.Collections;

public class GreenFaceBehavior : MonoBehaviour
{
    private bool isDead;
    private GameController controller;
    private Vector2 position;
    public float speed;
    private float spawnWave;
    public GameObject explosion;
    public GameObject shot1;
    private float shot1Time;
    private Vector2 sine;
    private float pi;
    private bool flip;
    private float health;
    // Use this for initialization
    void Awake()
    {
        controller = GameObject.Find("GameController").GetComponent<GameController>();
        spawnWave = controller.waveNumber;
        isDead = false;
        position = gameObject.transform.position;
        speed = 4f;
        shot1Time = .5f;
        if (transform.position.x > 0)
        {
            flip = true;
        }
        pi = 0f;
        health = 3f;
    }

    // Update is called once per frame
    void Update()
    {
        shot1Time -= Time.deltaTime;
        pi += 6.28f * Time.deltaTime;
        if (pi >= 6.28f)
        {
            pi = 0f;
        }
        DestroyBoundary();
        if (controller.waveNumber == 1 && !flip)
        {
            sine = new Vector2(Mathf.Sin(pi)* .75f, Mathf.Sin(pi) * 0.5f);
            transform.Translate(new Vector2(sine.x, -sine.y));
        
                if (shot1Time <= 0)
            {
                Instantiate(shot1, new Vector2(gameObject.transform.position.x, gameObject.transform.position.y), gameObject.transform.rotation);
                shot1Time = .5f;
            }
        }
        if (controller.waveNumber == 1 && flip)
        {
            sine = new Vector2(Mathf.Sin(pi) *0.75f, Mathf.Sin(pi) * 0.5f);
            transform.Translate(new Vector2(-sine.x, -sine.y));

            if (shot1Time <= 0)
            {
                Instantiate(shot1, new Vector2(gameObject.transform.position.x, gameObject.transform.position.y), gameObject.transform.rotation);
                shot1Time = .5f;
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
        Debug.Log("help me");

        if (transform.position.y > 5.5f || transform.position.y < -5.5f)
        {
            Destroy(gameObject);
        }
        if (transform.position.x > 5f || transform.position.x < -5f)
        {
            Destroy(gameObject);
        }
    }
    void Die()
    {
        Destroy(gameObject);
    }
}
