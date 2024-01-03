using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Whip : Item
{
    [System.Serializable]
    public class ItemTier
    {
        public string tierName;
        public float size;
        public float dmg;
        public bool whipReverse;
        public float coolDown;  

    }
    public ItemTier[] itemTier;

    public FacePlayerDirection faceDir;
    private PlayerExperience playerXP;
    private Animator anim;
    

    public bool coolingDown, whipping;
    public float timer;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        playerXP = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerExperience>();
        timer = itemTier[itemLevel].coolDown;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (activated)
        {
            if (coolingDown)
            {
                if (timer > 0)
                {
                    timer -= Time.deltaTime;
                }
                else
                {
                    coolingDown = false;
                    timer = itemTier[itemLevel].coolDown;
                }
            }
            else if (!whipping)
            {
                faceDir.AimWhip();
                whipping = true;
                StartCoroutine(Whipping());
                
            }              
        }
    }

    IEnumerator Whipping()
    {
        anim.SetTrigger("whipTriggerRight");
       


        if (itemTier[itemLevel].whipReverse)
        {
            yield return new WaitForSeconds(1);
            anim.SetTrigger("whipTriggerLeft");
        }

        yield return new WaitForSeconds(1);
        coolingDown = true;
        whipping = false;
  
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            EnemyHealth enemyHealth = collision.gameObject.GetComponent<EnemyHealth>();
            enemyHealth.health -= itemTier[itemLevel].dmg;
            enemyHealth.positionOfInflictor = new Vector3(transform.position.x, transform.position.y, 0);

            EnemyDrop damageDrop = collision.gameObject.GetComponent<EnemyDrop>();
            damageDrop.DamageDrop();

            VisualDamage visualDmg = collision.gameObject.GetComponent<VisualDamage>();
            visualDmg.visualDmg = true;

            if (collision.gameObject.TryGetComponent<Knockback>(out Knockback knockback))
            {
                knockback.ApplyKnockback(this.gameObject.transform);
            }

        }
    }

    public void UpgradeWhip()
    {
        if (!activated)
        {
            activated = true;
            playerXP.CloseUpgradeScreen(this);
        }
        else
        {
            itemLevel++;

            if (itemLevel == itemMaxLevel - 1)
            {
                maxLevelReached = true;
            }

            playerXP.CloseUpgradeScreen(this);
        }               
    }
}
