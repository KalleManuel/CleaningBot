using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemPickUp : MonoBehaviour
{
    public enum Type { experience, coin }
    public Type type = Type.experience;
    
    public float value = 0.2f;

    private PlayerHud playerHud;
    private PlayerExperience playerXP;
    public bool spawned, pickedUp;
    public GameObject player;
   
    [SerializeField]
    private float speed = 20;
    private Rigidbody2D rb;

    public float maxfloat = 10;

    public float timer = 0.5f;
    private GameOver playerStatus;
    private PlayerInventory inventory;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerStatus = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameOver>();
        playerHud = GameObject.FindGameObjectWithTag("GameController").GetComponent<PlayerHud>();
       
        if (!playerStatus.dead)
        {
            player = GameObject.FindGameObjectWithTag("Player");
            playerXP = player.GetComponent<PlayerExperience>();
            inventory = player.GetComponent<PlayerInventory>();
        }
       
        pickedUp = false;

        Vector2 direction = new Vector2((float)Random.Range(-100, 100), (float)Random.Range(-100, 100));
        float force = Random.Range(0.1f, maxfloat);



        rb.AddForce(direction * force);
        spawned = true;

       TryMoveOutOfNonWalkableArea();


    }
    private void Update()
    {
        if (spawned)
        {
            if (timer> 0)
            {
                timer -= Time.deltaTime;
            }
            else
            {
                spawned = false;
                rb.velocity = Vector2.zero;
            }
        }
        if (pickedUp)
        {
            if (player!= null)
            {
                transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);

                if (Vector3.Distance(transform.position, player.transform.position) < 0.1)
                {
                    if (type == Type.experience)
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
           
            
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Vacum")
        {
           
           pickedUp = true;
        }

       
    }

    private void TryMoveOutOfNonWalkableArea()
    {
        Collider2D[] colliders = Physics2D.OverlapBoxAll(transform.position, transform.localScale, 0f);
       
        if (player != null)
        {
            foreach (Collider2D collider in colliders)
            {
                if (collider.CompareTag("NonWalkable"))
                {
                    Vector2 direction = (transform.position - player.transform.position).normalized;

                    float force = Random.Range(0.1f, maxfloat);
                    rb.AddForce(direction * force);
                }
            }
        }
    }
}
