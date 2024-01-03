using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerkSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject[] perks;
    public float timer;

    [SerializeField]
    private Color red, green, yellow;


    public GameObject SpawnedPerk;
    public bool spawnTime, waitForPickUp;
    public bool playerInRange;
    public Animator spawnEffect;
    public Transform parentTransform;

  

    // Start is called before the first frame update
    void Start()
    {
        timer = Random.Range(5,60);
        spawnTime = true;
        playerInRange = false;
        GetComponent<SpriteRenderer>().color = yellow;
    }

    // Update is called once per frame
    void Update()
    {
        if (!playerInRange)
        {
            if (spawnTime)
            {
                if (timer > 0)
                {
                    timer -= Time.deltaTime;

                    if (gameObject.GetComponent<SpriteRenderer>().color != yellow)
                    {
                       gameObject.GetComponent<SpriteRenderer>().color = yellow;
                    }                     
                }
                else
                {
                    spawnTime = false;

                    spawnEffect.SetBool("playSpawnEffect", true);
                    
                }
            }
            else if (waitForPickUp)
            {
                if (SpawnedPerk == null)
                {
                    gameObject.GetComponent<SpriteRenderer>().color = yellow;
                    spawnTime = true;
                    waitForPickUp = false;

                }    
            }
           
        }
        else gameObject.GetComponent<SpriteRenderer>().color = red;

    }

    public void SpawnItem()
    {
        SpawnedPerk = Instantiate(perks[Random.Range(0, perks.Length)], transform.position, transform.rotation,parentTransform);
        timer = Random.Range(20, 40);
        GetComponent<SpriteRenderer>().color = green;
        spawnEffect.SetBool("playSpawnEffect", false);
        waitForPickUp = true;
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playerInRange = true;

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playerInRange = false;
        }
    }
}
