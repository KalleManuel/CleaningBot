using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float health = 1;

    public bool outOfRange = true;
    public float outOfRangeTimer = 3;
    private bool stained;

    
    public Vector3 positionOfInflictor;
    

   // public GameObject[] bloodClots;

    public bool stayInGameUntilKilled;


    private SpriteRenderer tintColor;

    private Statistic stats;

    // Start is called before the first frame update
    void Start()
    {
        stats = GameObject.FindGameObjectWithTag("GameController").GetComponent<Statistic>();
    }

    // Update is called once per frame
    void Update()
    {     
        if (health <= 0)
        {
            if (!stained)
            {
                GetComponent<EnemyDrop>().DropStain(positionOfInflictor);
                stained = true;
            }
            
            VisualDamage visualDmg = GetComponent<VisualDamage>();

                if (visualDmg.visualDmgTimer < visualDmg.visualEffectTime / 2)
                {
                GetComponent<EnemyDrop>().Drop();
                
                Destroy(gameObject);
                    stats.enemiesKilled++;
                }     
        }

        if (!stayInGameUntilKilled)
        {
            if (outOfRange)
            {
                if (outOfRangeTimer > 0)
                {
                    outOfRangeTimer -= Time.deltaTime;
                }
                else
                {
                    Destroy(gameObject);
                }
            }
        }          
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
       
        if (collision.gameObject.tag == ("SpawnStopper"))
        {
            outOfRange = false;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        

        if (collision.gameObject.tag == ("SpawnStopper"))
        {
            outOfRange = true;
        }
    }
}
