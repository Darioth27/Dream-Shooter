using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour
{
    private float gameTime;
    public float waveNumber;
    public float enemy1SpawnTime;
    public float enemy1SpawnCount;
    public float enemy2SpawnTime;
    public float enemy2SpawnCount;
    public GameObject[] enemies;
    public GameObject player;
    private Vector2 spawnPosition;
    private bool playerisDead;
    private float respawnTimer;
    private PlayerMovement script;
    private GameObject currentPlayer;
    private bool isRespawning;
    private int lifeCounter;
    private float rand;
    public AudioClip[] music;

    void Start()
    {
        playerisDead = false;
        gameTime = 0f;
        waveNumber = 1f;
        enemy1SpawnTime = 1f;
        enemy1SpawnCount = 0f;
        enemy2SpawnTime = 1f;
        enemy2SpawnCount = 0f;
        respawnTimer = 1f;
        spawnPosition = new Vector2(0, -4);
        currentPlayer = (GameObject) Instantiate(player, spawnPosition, gameObject.transform.rotation);
        isRespawning = false;
        lifeCounter = 99;
    }
    void Update()
    {
        if (playerisDead && lifeCounter >= 0f)
        {
            respawnTimer -= Time.deltaTime;
            if (respawnTimer <= 0)
            {
                respawnTimer = 1f;
                currentPlayer = (GameObject)Instantiate(player, new Vector2(0f, -6f), gameObject.transform.rotation);
                script = currentPlayer.GetComponent<PlayerMovement>();
                script.enabled = false;
                playerisDead = false;
                isRespawning = true;
                lifeCounter -= 1;
            }
        }

        if (isRespawning && currentPlayer.transform.position.y < spawnPosition.y)
        {
            currentPlayer.transform.Translate(Vector2.up * Time.deltaTime * 2.5f);
        }
        else if (isRespawning && currentPlayer.transform.position.y >= spawnPosition.y)
        {
            script.enabled = true;
            isRespawning = false;
        }

        gameTime = gameTime + Time.deltaTime;
        Debug.Log("" + gameTime);
        if (gameTime < 30f)
        {
            enemy1SpawnTime -= Time.deltaTime;
            enemy2SpawnTime -= Time.deltaTime;
        }
        if (gameTime > 30f/* && gameTime <= 60f*/)
        {
            waveNumber = 2f;
            enemy1SpawnTime -= Time.deltaTime;
            enemy2SpawnTime -= Time.deltaTime;
        }
        /*if (gameTime > 60f && gameTime <= 90f)
        {
            waveNumber = 3f;
        }*/
        if (waveNumber == 1f && enemy1SpawnTime <= 0 && enemy1SpawnCount <= 5f && gameTime <= 5f)
        {
            Instantiate(enemies[0], new Vector2(-4f, 4f), transform.rotation);
            enemy1SpawnTime = .3f;
            enemy1SpawnCount += 1f;
        }
        if (waveNumber == 1f && enemy2SpawnTime <= 0 && enemy2SpawnCount <= 8f && gameTime > 5f && gameTime <= 9f)
        {
            rand = Random.Range(0f, 10f);
            if (rand > 5f)
            {
                Instantiate(enemies[1], new Vector2(Random.Range(-3f, -4f), Random.Range(4f, 5.5f)), transform.rotation);
            }
            if (rand <= 5f)
            {
                Instantiate(enemies[1], new Vector2(Random.Range(3f, 4f), Random.Range(4f, 5.5f)), transform.rotation);
            }
            enemy2SpawnTime = .2f;
            enemy2SpawnCount += 1f;
            enemy1SpawnCount = 0f;
        }
        if (waveNumber == 1f && enemy1SpawnTime <= 0 && enemy1SpawnCount == 0f && gameTime > 9f && gameTime <= 17)
        {
            Instantiate(enemies[2], new Vector2(0, 6), transform.rotation);
            enemy1SpawnTime = .2f;
            enemy1SpawnCount += 1f;
            enemy2SpawnCount = 0f;
        }
        if (waveNumber == 1f && enemy2SpawnTime <= 0 && enemy2SpawnCount <= 1f && gameTime > 17f && gameTime <= 19)
        {
            Instantiate(enemies[3], new Vector2(-4f, 5f), transform.rotation);
            enemy2SpawnTime = .2f;
            enemy2SpawnCount += 1f;
            enemy1SpawnCount = 0f;
        }
        if (waveNumber == 1f && enemy1SpawnTime <= 0 && enemy1SpawnCount <= 1f && gameTime > 19f && gameTime <= 21)
        {
            Instantiate(enemies[3], new Vector2(4f, 5f), transform.rotation);
            enemy1SpawnTime = .2f;
            enemy1SpawnCount += 1f;
            enemy2SpawnCount = 0f;
        }
        if (waveNumber == 1f && enemy2SpawnTime <= 0 && enemy2SpawnCount <= 10f && gameTime > 21f && gameTime <= 27f)
        {
            Instantiate(enemies[0], new Vector2(-4f, 4f), transform.rotation);
            if (Random.Range(0f, 10f) > 5f)
            {
                 Instantiate(enemies[1], new Vector2(Random.Range(-3f, -4f), Random.Range(0f, 5.5f)), transform.rotation);
            }
            if (Random.Range(0f, 10f) <= 5f)
            {
                Instantiate(enemies[1], new Vector2(Random.Range(3f, 4f), Random.Range(0f, 5.5f)), transform.rotation);
            }
            enemy2SpawnTime = .3f;
            enemy2SpawnCount += 1f;
            enemy1SpawnCount = 0f;
        }
        if (waveNumber == 2f && enemy1SpawnTime <= 0 && enemy1SpawnCount == 0f && gameTime > 30f && gameTime <= 37f)
        {
            Instantiate(enemies[4], new Vector2(0, 6), transform.rotation);
            enemy1SpawnTime = .2f;
            enemy1SpawnCount += 1f;
            enemy2SpawnCount = 0f;
            GetComponent<AudioSource>().Stop();
            GetComponent<AudioSource>().clip = music[3];
            GetComponent<AudioSource>().Play();
        }
        if (waveNumber == 2f && enemy2SpawnTime <= 0 && enemy2SpawnCount <= 5 && gameTime > 37f && gameTime <= 43f)
        {
            Instantiate(enemies[0], new Vector2(-4f, 4f), transform.rotation);
            if (Random.Range(0f, 10f) > 5f)
            {
                Instantiate(enemies[1], new Vector2(Random.Range(-3f, -4f), Random.Range(0f, 5.5f)), transform.rotation);
            }
            if (Random.Range(0f, 10f) <= 5f)
            {
                Instantiate(enemies[1], new Vector2(Random.Range(3f, 4f), Random.Range(0f, 5.5f)), transform.rotation);
            }
            enemy2SpawnTime = .5f;
            enemy2SpawnCount += 1f;
            enemy1SpawnCount = 0f;
        }
        if (waveNumber == 2f && enemy2SpawnTime <= 0 && enemy2SpawnCount <= 100f && gameTime > 43f)
        {
            Instantiate(enemies[0], new Vector2(-4f, 4f), transform.rotation);
            if (Random.Range(0f, 10f) > 5f)
            {
                Instantiate(enemies[1], new Vector2(Random.Range(-3f, -4f), Random.Range(0f, 5.5f)), transform.rotation);
            }
            if (Random.Range(0f, 10f) <= 5f)
            {
                Instantiate(enemies[1], new Vector2(Random.Range(3f, 4f), Random.Range(0f, 5.5f)), transform.rotation);
            }
            enemy2SpawnTime = 1.5f;
            enemy2SpawnCount += 1f;
            enemy1SpawnCount = 0f;
        }
    }

    public float getTime()
    {
        return gameTime;
    }
    public bool playerDead()
    {
        return playerisDead;
    }
    public void setPlayerisDead(bool b)
    {
        playerisDead = b;
    }
    public bool PlayerRespawning()
    {
        return isRespawning;
    }
    public int lives()
    {
        return lifeCounter;
    }
}
