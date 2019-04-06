using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;
using UnityEngine.SceneManagement;

//this is the player manager which primarily holds basic movement controls and hit detection. 
//this script does one interecting thing where it raises the platform it is standing on if the player executes the required input (spinning their joystick in a circle)

public class PlayerManager : MonoBehaviour
{
    
    private Rigidbody2D rBody;
    private Animator anim;
    private SpriteRenderer spriteRenderer;
    
    private bool isGrounded;
    
    [SerializeField]
    private float speed;
    
    public float verticaljumpForce;
    public float horizontalJumpForce;

    private GameObject currentTile;

    public GameObject winTile;

    [SerializeField]
    private GameObject deadUI;
    
    public bool isDead, isPaused;

    public static float movementSpeed;

    public GameObject soundSource;

    public bool cameraMove;

    public static PlayerManager instance;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        rBody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    
    // Update is called once per frame
    void Update()
    {
        float jumpInput = Input.GetAxis("Jump");
        float xMovement = InputManager.instance.MovementInput();
        movementSpeed = xMovement;
        
        transform.Translate(Vector3.right * speed * xMovement *Time.deltaTime);
        
        anim.SetFloat("speed",Mathf.Abs(xMovement));
        
        RotatePlayer(spriteRenderer,xMovement);
        
        if (jumpInput > 0 && isGrounded)
        {
            rBody.AddForce(new Vector2(0,verticaljumpForce), ForceMode2D.Impulse);
            isGrounded = false;
            anim.SetBool("isGround",false);
            
        }

        if (currentTile != null)
        {
            print("Got it");

            Vector3 localScale = currentTile.transform.localScale;
            //float scaleMultiplier = InputManager.instance.PatternDictionary[InputManager.keyMashStr];
            localScale.y += InputManager.instance.IncreaseBarWithMash()* 10 * Time.deltaTime;
            print(InputManager.instance.IncreaseBarWithMash());
            currentTile.transform.localScale = localScale;

        }
        
        anim.SetBool("isGround",isGrounded);
        anim.SetFloat("vSpeed",rBody.velocity.y);
//        if (transform.position.y < deadLine.transform.position.y)
//        {
//            isDead = true;
//        }
//
        if (isDead)
        {
            deadUI.SetActive(true);
            //SceneManager.LoadScene("Demo1");
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("Home");
        }

        if (transform.position.x > -4.5f && !cameraMove)
        {
            cameraMove = true;
            soundSource.SetActive(true);
            //soundSource.GetComponent<AudioSource>().Play();
           // other.gameObject.SetActive(false);   
        }
      
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.transform.tag == "Ground")
        {
            isGrounded = true;
            currentTile = other.gameObject;
            if(other.transform.gameObject.GetComponent<GrowOnBeat>()!=null)
                other.transform.gameObject.GetComponent<GrowOnBeat>().stop = true;
        }
        
        else if (other.transform.name == "DeadZone")
        {
            isDead = true;
        }
        
        else if (other.transform.name == "Win")
        {
            winTile.gameObject.SetActive(true);
        }
        
    }

    private void RotatePlayer(SpriteRenderer spr,float direction)
    {
        if (direction > 0)
        {
            spr.flipX = false;
        }
        else if(direction<0)
        {
            spr.flipX = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.name == "Note")
        {
            Debug.Log("NOTE");
            other.transform.gameObject.GetComponent<GrowOnBeat>().stop = true;
        }

      
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.name == "Note")
        {
            other.transform.gameObject.GetComponent<GrowOnBeat>().stop = false;
        }
    }
}
