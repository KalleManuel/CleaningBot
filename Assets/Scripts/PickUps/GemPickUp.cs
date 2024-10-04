using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemPickUp : MonoBehaviour
{
    public enum Type { experience, coin }
    public Type type = Type.experience;
    
    public float value = 0.2f;

    
  //  private PlayerExperience playerXP;
    public bool moving, pickedUp;
   // public GameObject player;
   
    [SerializeField]
    public float speed = 20;
    private Rigidbody2D rb;

    public float maxfloat = 10;

    public float timer = 0.5f;
    private GameOver playerStatus;
    // private PlayerInventory inventory;


    // Start is called before the first frame update
    void Start()
    {
        Collectebles.collectibles.bloodClots.Add(this.gameObject);
        rb = GetComponent<Rigidbody2D>();
        playerStatus = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameOver>();
        
       /*
        if (!playerStatus.dead)
        {
            player = GameObject.FindGameObjectWithTag("Player");
            playerXP = player.GetComponent<PlayerExperience>();
            inventory = player.GetComponent<PlayerInventory>();
        }
       */
        pickedUp = false;

        // give the blood clot a small push when spawned to simulate that the alien been mashed, or exploded.

        Vector2 direction = new Vector2((float)Random.Range(-100, 100), (float)Random.Range(-100, 100));
        float force = Random.Range(0.1f, maxfloat);
        rb.AddForce(direction * force);
        moving = true;

       TryMoveOutOfNonWalkableArea(); // if spawned outside of bounderies move them inside

    }
    private void Update()
    {
        if (moving)
        {
            if (timer> 0)   // added a timer here so that the gentle push could appear.
            {
                timer -= Time.deltaTime;
            }
            else
            {
                moving = false;
                rb.velocity = Vector2.zero; // then stopped the movment to make the bloodclots lay still
            }
        }

        /*
        if (pickedUp) // this is made in update because I wanted the item sucked toward the player. I could just destroy the item when in range but I really wanted this effect.
        {
                transform.position = Vector3.MoveTowards(transform.position, Player.playerPosition.position, speed * Time.deltaTime);

                if (Vector3.Distance(transform.position, Player.playerPosition.position) < 0.1)
                {
                    if (type == Type.experience)
                    {
                       Player.playerXP.GainExperience(value);
                       Destroy(gameObject);
                    }
                    else if (type == Type.coin)
                    {
                        Player.playerInventory.AddCoin(value);
                        Destroy(gameObject);
                    }
                    
                }
            
           
            
     
    */
    }
    /*
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Vacum")
        {

            pickedUp = true;


            /* if (type == Type.experience)
            {
                playerXP.GainExperience(value);
                Destroy(gameObject);
            }
            else if (type == Type.coin)
            {
                inventory.AddCoin(value);
                Destroy(gameObject);
            }

            
            

        }
    }
    */
    private void TryMoveOutOfNonWalkableArea()
    {
        Collider2D[] colliders = Physics2D.OverlapBoxAll(transform.position, transform.localScale, 0f);
       
       
            foreach (Collider2D collider in colliders)
            {
                if (collider.CompareTag("NonWalkable"))
                {
                    Vector2 direction = (transform.position - Player.playerPosition.position).normalized;

                    float force = Random.Range(0.1f, maxfloat);
                    rb.AddForce(direction * force);
                }
            }
        }
    
}
