using UnityEngine;
using System.Collections;
[System.Serializable]
public class Boundary
{
    public float xMin, xMax, yMin, yMax;
}
public class PlayerMovement : MonoBehaviour
{

    public Animator anim;
    private float speed;
    private float diagMultiplier = 0.707106f;
    private bool isFocused; // the player is holding shift
    private bool isShooting;
    private Direction direction;        //current direction the character is moving in
    private bool left, right, up, down; //booleans that determine if a directional key is being held down or not
    private bool directionChanged;      //boolean that represents if direction of movement has changed
    private Direction previousDirection;    //represents the direction the character was traveling during the last update
    public Boundary boundary;
    public GameObject shot;
    public GameObject Hitbox;
    public Transform shotSpawn;
    public float fireRate;
    private float nextFire;
    private bool isDead;
    public ParticleSystem explosion;
  

    //Enums that represent the direction the character is currently traveling in
    public enum Direction
    {
        Up, UpRight, Right, DownRight, Down, DownLeft, Left, UpLeft, Idle
    }
    
    void Awake()
    {
        // anim = GetComponent<Animator>();
        direction = Direction.Idle;     //Initializes direction as Idle
        previousDirection = direction;  //sets the previousDirection equal to the direction because there is none yet.
        directionChanged = false;       //Initializes directionChanged
        left = false;                   //Initialize directional booleans
        right = false;                  //Their purpose is to effectively call "getKey" without actually calling
        up = false;                  //"GetKey".  Primarily here for code readability.
        down = false;
        isDead = false;
        speed = 5f;

    }

    void Update()
    {
        transform.position = new Vector2(
            Mathf.Clamp(transform.position.x, boundary.xMin, boundary.xMax),
            Mathf.Clamp(transform.position.y, boundary.yMin, boundary.yMax)
        );

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            speed = 1f;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            speed = 5f;
        }
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            left = true;                    //Sets 'left' to true and left stays true until the key is lifted.
            // anim.SetBool("Idle", false);    //Sets "Idle" to false in the anim to unlock movement.
        }
        if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.LeftArrow))
        {
            left = false;                   //Sets 'left' to false.  Basically says the key is no longer being pressed.
        }
        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            // anim.SetBool("Idle", false);
            right = true;
        }
        if (Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.RightArrow))
        {
            right = false;
        }
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            // anim.SetBool("Idle", false);
            up = true;
        }
        if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.UpArrow))
        {
            up = false;
        }
        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            // anim.SetBool("Idle", false);
            down = true;
        }
        if (Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.DownArrow))
        {
            down = false;
        }
        if (Input.GetKeyDown(KeyCode.Z))
        {
            isShooting = true;
        }
        if (Input.GetKeyUp(KeyCode.Z))
        {
            isShooting = false;
        }

        previousDirection = direction;  //This is used to record the direction of movement last frame.  Useful later.

        //This giant chain of if-statements determines the new current direction.
        if (up)
        {
            if (left)
            {
                direction = Direction.UpLeft;
            }
            else if (right)
            {
                direction = Direction.UpRight;
            }
            else if (down)                          //This deals with pesky opposite directional presses.
            {
                return;
            }
            else
            {
                direction = Direction.Up;
            }
        }
        else if (right)
        {
            if (up)
            {
                direction = Direction.UpRight;
            }
            else if (down)
            {
                direction = Direction.DownRight;
            }
            else if (left)
            {
                return;
            }
            else
            {
                direction = Direction.Right;
            }
        }
        else if (left)
        {
            if (up)
            {
                direction = Direction.UpLeft;
            }
            else if (down)
            {
                direction = Direction.DownLeft;
            }
            else if (right)
            {
                return;
            }
            else
            {
                direction = Direction.Left;
            }
        }
        else if (down)
        {
            if (left)
            {
                direction = Direction.DownLeft;
            }
            else if (right)
            {
                direction = Direction.DownRight;
            }
            else if (up)
            {
                return;
            }
            else
            {
                direction = Direction.Down;
            }
        }
        else
        {
            direction = Direction.Idle;         //Sets direction to "Idle" if no keys are being pressed
        }

        if (previousDirection != direction)     //Determines if the direction has changed since the last update.
        {
            directionChanged = true;
        }
        else
        {
            directionChanged = false;
        }

        //Where Everything is done.
        switch (direction)
        {
            case Direction.Up:                               //If (direction == Up)...
                transform.Translate(Vector2.up * speed * Time.deltaTime);        //Move up.
                if (directionChanged)                           //If the direction has changed since last update,
                {
                    //anim.SetTrigger("Up");                   //Trigger the animation once.
                }                                               //This prevents repeated calling of the animation trigger
                break;
            case Direction.UpLeft:
                transform.Translate(Vector2.up * speed * Time.deltaTime);
                transform.Translate(-Vector2.right * speed * Time.deltaTime);
                if (directionChanged)
                {
                    // anim.SetTrigger("Left");                   //For the diagonal directions, create new "Triggers" in
                }                                               //the animator.  Temporarily using up and down.
                break;
            case Direction.Left:
                transform.Translate(Vector2.left * speed * Time.deltaTime);
                if (directionChanged)
                {
                    // anim.SetTrigger("Left");
                }
                break;
            case Direction.DownLeft:
                transform.Translate(Vector2.down * speed * Time.deltaTime);
                transform.Translate(Vector2.left * speed * Time.deltaTime);
                if (directionChanged)
                {
                    //anim.SetTrigger("Left");
                }
                break;
            case Direction.Down:
                transform.Translate(Vector2.down * speed * Time.deltaTime);
                if (directionChanged)
                {
                    //anim.SetTrigger("Down");
                }
                break;
            case Direction.DownRight:
                transform.Translate(Vector2.down * speed * Time.deltaTime);
                transform.Translate(Vector2.right * speed * Time.deltaTime);
                if (directionChanged)
                {
                    //anim.SetTrigger("Right");
                }
                break;
            case Direction.Right:
                transform.Translate(Vector2.right * speed * Time.deltaTime);
                if (directionChanged)
                {
                    //anim.SetTrigger("Right");
                }
                break;
            case Direction.UpRight:
                transform.Translate(Vector2.up * speed * Time.deltaTime);
                transform.Translate(Vector2.right * speed * Time.deltaTime);
                if (directionChanged)
                {
                    //anim.SetTrigger("Right");
                }
                break;
            default:
                // anim.SetBool("Idle", true);                     //If the character isn't moving, then set Idle to true.
                break;                                              //Character will stop moving and face the direction
        }                                                       //he stopped in.
        if (isShooting && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            Instantiate(shot, new Vector2(shotSpawn.transform.position.x, shotSpawn.transform.position.y), shotSpawn.rotation);
        }
    }

    public void stopMovement()
    {
        left = false;
        right = false;
        up = false;
        down = false;
        anim.SetBool("Idle", true);
    }

    public void moveRight()
    {
        right = true;
        transform.Translate(Vector2.right * speed * diagMultiplier);
        // if (directionChanged)
        {
            anim.SetTrigger("Right");
        }
    }

    public void moveLeft()
    {
        left = true;
        transform.Translate(-Vector2.right * speed * diagMultiplier);
        // if (directionChanged)
        {
            anim.SetTrigger("Left");
        }
    }

    public void moveUp()
    {
        up = true;
        transform.Translate(Vector2.up * speed * diagMultiplier);
        // if (directionChanged)
        {
            anim.SetTrigger("Up");
        }
    }

    public void moveDown()
    {
        down = true;
        transform.Translate(-Vector2.up * speed * diagMultiplier);
        // if (directionChanged)
        {
            anim.SetTrigger("Down");
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if ((other.tag == "EnemyBullet" || other.tag == "Enemy") && !isDead)
        {
            if (other.tag == "EnemyBullet")
            {
                Destroy(other.gameObject);
            }

            isDead = true;
            GameObject.Find("GameController").GetComponent<GameController>().setPlayerisDead(true);
            Instantiate(explosion, new Vector2(gameObject.transform.position.x, gameObject.transform.position.y), gameObject.transform.rotation);
            Destroy(gameObject);

        }

    }
}
