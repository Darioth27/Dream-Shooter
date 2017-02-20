using UnityEngine;
using System.Collections;

public class SkullfaceBehavior : MonoBehaviour
{
    private bool isDead;
    private GameController controller;
    private Vector2 position;
    public float speed;
    private float spawnWave;
    public GameObject explosion;
    public GameObject shot1;
    public AudioClip[] scream;
    private float shot1Time;
    // Use this for initialization
    void Awake()
    {
        controller = GameObject.Find("GameController").GetComponent<GameController>();
        spawnWave = controller.waveNumber;
        isDead = false;
        position = gameObject.transform.position;
        speed = 4f;
        shot1Time = .5f;
    }

    // Update is called once per frame
    void Update()
    {
        shot1Time -= Time.deltaTime;
        DestroyBoundary();
        if (controller.waveNumber == 1)
        {
            transform.Translate(Vector2.right * speed * Time.deltaTime);
            if (shot1Time <= 0)
            {
                Instantiate(shot1, new Vector2(gameObject.transform.position.x, gameObject.transform.position.y), gameObject.transform.rotation);
                shot1Time = .5f;
            }
        }
        if (controller.waveNumber == 2)
        {
            transform.Translate(Vector2.right * speed * Time.deltaTime);
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
        if (other.tag == "Bullet" && !isDead)
        {
            Destroy(other.gameObject);
            GetComponent<AudioSource>().PlayOneShot(scream[Random.Range(0, 5)]);
            gameObject.GetComponent<Animator>().SetTrigger("DeathTrigger");
            Instantiate(explosion, new Vector2(gameObject.transform.position.x, gameObject.transform.position.y), gameObject.transform.rotation);
            isDead = true;
            
        }
    }
    void DestroyBoundary()
    {

        if (transform.position.y > 5.5f || transform.position.y < -5.5f)
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
}
