using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FlameThrower : MonoBehaviour
{
    public float flameDmg;
    public float flameDuration = 2;
    public bool targetLocked, readyToShoot, readyToReload;

    public List <Transform> enemies;

    public Transform target;
    public float turnSpeed= 10;
    public float offsetAngle = 0;

    public float reloadTimer;
    public float reloadTime = 4f;

    public GameObject flame;
    private Flame flameScript; 
    private Animator flameAnim;
    private Pause pause;

    public NavMeshAgent agent;
    public HumanMovment human;
    public float speedWhileShooting;
    private float currentSpeed;

    // Start is called before the first frame update
    void Start()
    {
        reloadTimer = reloadTime;
        readyToReload = true;

        pause = GameObject.FindGameObjectWithTag("GameController").GetComponent<Pause>();
        flameScript = flame.GetComponent<Flame>();
        flameAnim = flame.GetComponent<Animator>();
    }
    void Update()
    {
        if (!pause.gamePaused)
        {
            if (readyToShoot)
            {
                if (enemies.Count >= 1)
                {
                    if (!targetLocked)
                    {
                        target = enemies[Random.Range(0, enemies.Count)];
                        targetLocked = true;
                    }

                    if (target != null)
                    {
                        agent.SetDestination(target.position);
                        agent.stoppingDistance = 2f;

                        StartCoroutine(FlameOn());
                        readyToShoot = false;

                        /*
                        Vector2 targetDirection = target.position - transform.position;
                        float angle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg - offsetAngle;
                        Quaternion q = Quaternion.Euler(new Vector3(0, 0, angle));

                        transform.localRotation = Quaternion.Slerp(transform.localRotation, q, turnSpeed);
                        */
                    }

                    else
                    {
                        targetLocked = false;

                        if (GetComponentInParent<HumanMovment>().beenFound)
                        {
                            agent.SetDestination(GetComponentInParent<HumanMovment>().target.position);
                        }
                    }
                }

            }

            if (readyToReload)
            {
                if (reloadTimer > 0)
                {
                    reloadTimer -= Time.deltaTime;
                }
                else
                {
                    readyToShoot = true;
                    readyToReload = false;
                }
            }
        }
    }
    private IEnumerator FlameOn()
    {
        flame.GetComponent<BoxCollider2D>().enabled = true;
        flameAnim.SetTrigger("FlameOn");
        flameScript.dmg = flameDmg;
        currentSpeed = agent.speed;
        human.targetingEnemy = true;
        agent.speed = speedWhileShooting;

        yield return new WaitForSeconds(flameDuration);

        StartCoroutine(FlameOff());

    }

    IEnumerator FlameOff()
    {
        flameAnim.SetTrigger("FlameOff");
        reloadTimer = reloadTime;
        readyToReload = true;

        yield return new WaitForSeconds(1);
        
        agent.speed = currentSpeed;
        human.targetingEnemy = false;
        flame.GetComponent<BoxCollider2D>().enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            enemies.Add(collision.gameObject.transform);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            enemies.Remove(collision.gameObject.transform);
        }
    }
}
