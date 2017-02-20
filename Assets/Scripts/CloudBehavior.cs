using UnityEngine;
using System.Collections;

public class CloudBehavior : MonoBehaviour {

    private float cloudSpeed;
    private float speedMultiplier;
    public Sprite[] images;
    private SpriteRenderer theSprite;
    private GameManager gameManager;

	void Awake ()
    {
        cloudSpeed = Random.Range(0.008f, 0.025f);
        theSprite = gameObject.GetComponent<SpriteRenderer>();
        theSprite.sprite = images[Random.Range(0, 4)];
        transform.localScale = transform.localScale * Random.Range(1.5f, 3.0f);
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        if (gameManager.getTimer() < 15)
        {
            speedMultiplier = 1f;
        }
        else if (gameManager.getTimer() < 25)
        {
            speedMultiplier = 1.5f;
        }
        else if (gameManager.getTimer() < 35)
        {
            speedMultiplier = 2.0f;
        }
        else if (gameManager.getTimer() < 40)
        {
            speedMultiplier = 3.5f;
        }
        else
        {
            speedMultiplier = 5.0f;
        }

    }

    void Start ()
    {
        subtractColor(0f, 1f - gameManager.getGreen(), 1f - gameManager.getBlue());
    }
	
	// Update is called once per frame
	void Update ()
    {
        transform.Translate(Vector2.down * cloudSpeed * speedMultiplier);
        if (transform.position.y < -8.0f)
        {
            Destroy(gameObject);
        }
    }

    public void subtractColor(float r, float g, float b)
    {
        theSprite.color = new Color(theSprite.color.r - r, theSprite.color.g - g, theSprite.color.b - b);
    }
}
