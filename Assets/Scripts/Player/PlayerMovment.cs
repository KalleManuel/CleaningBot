using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerMovment : MonoBehaviour
{

    //public float speed = 5;
    public Vector2 direction;
    private Animator anim;
    private Rigidbody2D rb;

    public float acceleration = 5f;
    public float maxSpeed = 10f;
    public float glideFactor = 0.95f;
    private bool glide;
    public float directionX;
    public float directionY;

    private Pause pause;
    SavedStats savedStats;

    // Start is called before the first frame update
    void Awake()
    {
        pause = GameObject.FindGameObjectWithTag("GameController").GetComponent<Pause>();
        savedStats = GameObject.FindGameObjectWithTag("CoinStash").GetComponent<SavedStats>();

        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
 
    }

    // Update is called once per frame
    void Update()
    {

        if (!pause.gamePaused)
        {
            TiltPlayer();

            if (!savedStats.touchControls)
            {
                 directionX = Input.GetAxisRaw("Horizontal");
                 directionY = Input.GetAxisRaw("Vertical");
            }
           

            direction = new Vector2(directionX, directionY);

            if (directionX == 0 && directionY == 0)
            {

                return;
            }

            AnimateCharacter();
        }
       

    }
    private void FixedUpdate()
    {

        Vector2 accelerationForce = direction * acceleration;
        rb.AddForce(accelerationForce);

        if (rb.velocity.magnitude > maxSpeed)
        {
            rb.velocity = rb.velocity.normalized * maxSpeed;
        }

        if (direction == Vector2.zero)
        {
            rb.velocity *= glideFactor;
            
        }
    }
    public void AnimateCharacter()
    {
        anim.SetFloat("MoveX", direction.x);
        anim.SetFloat("MoveY", direction.y);
    }

    public void TiltPlayer()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            transform.localRotation = Quaternion.Euler(0, 0, 5f);
        }
        else if (Input.GetKeyUp(KeyCode.A))
        {
            transform.localRotation = Quaternion.Euler(0, 0, 0);
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            transform.localRotation = Quaternion.Euler(0, 0, -5f);
        }
        else if (Input.GetKeyUp(KeyCode.D))
        {
            transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
    }
}
