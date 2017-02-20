using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    private float cloudSpawnTimer;
    private float skyTimer;
    public GameObject cloud;
    private SpriteRenderer background;
    private float red;
    private float green;
    private float blue;
    private float gameTimer;
	// Use this for initialization
	void Awake ()
    {
        cloudSpawnTimer = 0f;
        gameTimer = 0f;
        skyTimer = 0.0007f;
        background = GameObject.Find("background").GetComponent<SpriteRenderer>();
        red = background.color.r;
        green = background.color.g;
        blue = background.color.b;
	}
	
	// Update is called once per frame
	void Update ()
    {
	    if (cloudSpawnTimer <= 0)
        {
            Instantiate(cloud, new Vector2(transform.position.x + Random.Range(-3f, 3f), transform.position.y + 1.8f), 
                transform.rotation);
            cloudSpawnTimer = Random.Range(0.8f, 1.5f);
        }
        else
        {
            if (gameTimer < 15)
            {
                cloudSpawnTimer -= Time.deltaTime * 2f;
            }
            else if (gameTimer < 25)
            {
                cloudSpawnTimer -= Time.deltaTime * 2.5f;
            }
            else if (gameTimer < 35)
            {
                cloudSpawnTimer -= Time.deltaTime * 3.2f;
            }
            else if (gameTimer < 40)
            {
                cloudSpawnTimer -= Time.deltaTime * 8f;
            }
            else
            {
                cloudSpawnTimer -= Time.deltaTime * 15f;
            }
        }
        if (red * 255 > 35f)
        {
            red = red - skyTimer;
        }
        if (green * 255 > 2f)
        {
            green = green - skyTimer;
        }
        if (blue * 255 > 25f)
        {
            blue = blue - skyTimer;
        }
        background.color = new Color(red, green, blue);
        gameTimer += Time.deltaTime;
        //Debug.Log("" + gameTimer);
	}

    public float getRed()
    {
        return red;
    }

    public float getGreen()
    {
        return green;
    }

    public float getBlue()
    {
        return blue;
    }

    public float getTimer()
    {
        return gameTimer;
    }
}
