using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FlameAim : MonoBehaviour
{
    public List<Transform> enemies;

    public NavMeshAgent agent;
    public float speedWhileShooting;
    private float currentSpeed;

    public Transform target;
    public float turnSpeed = 100;

    public float flameTime;
    public float timer;
    public float reloadTime = 2;

    public bool flameOn, readyToFlame;

    private Pause pause;

    [SerializeField]
    private ParticleSystem flame;

    private void Start()
    {
        pause = GameObject.FindGameObjectWithTag("GameController").GetComponent<Pause>();

        timer = flameTime;
        flameOn = false;
        readyToFlame = false;
    }

    private void Update()
    {
        if (!pause.gamePaused)
        {
            // reloading
            if (!flameOn && !readyToFlame)
            {
                if (reloadTime > 0)
                {
                    reloadTime -= Time.deltaTime;
                }
                else
                {
                    if (enemies.Count >= 1)
                    {
                        Shoot();
                    }
                    
                    readyToFlame = true;
                      
                }

            }

            if (readyToFlame)
            {
                // reloaded - looking for target
                if (target == null && enemies.Count >= 1)
                {
                    target = enemies[Random.Range(0, enemies.Count)];
                    if (!flameOn)
                    {
                        Shoot();
                    }
                }
                else if (target != null)
                {
                    // target found - aiming
                    Vector2 targetDirection = target.position - transform.position;
                    float angle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg - 180;
                    Quaternion q = Quaternion.Euler(new Vector3(angle, -90, -90));

                    transform.localRotation = Quaternion.Slerp(transform.localRotation, q, turnSpeed);
                }

                if (flameOn)
                {
                    // burning fuel
                    if (timer > 0)
                    {
                        timer -= Time.deltaTime;
                    }
                    else
                    {
                        // out of fuel - back to reloading   
                        flame.Stop();
                        reloadTime = 2;
                        flameOn = false;
                        agent.speed = currentSpeed;
                        readyToFlame = false;
                        timer = flameTime;
                        
                    }
                }
            }
        }
    }

    private void Shoot()
    {
        flameOn = true;
        currentSpeed = agent.speed;
        agent.speed = speedWhileShooting;
        flame.Play();

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