using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    public Transform target;
    NavMeshAgent agent;
    public bool WaitFollowPlayer;
    
    private GameOver playerStatus;

    public Vector2 aVelocity;
    public bool animated;
    public Animator anim;

    private void Start()
    {
        playerStatus = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameOver>();
        
        if (!playerStatus.dead)
        {
            if (!WaitFollowPlayer)
            {
                target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
            }

        }

        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;

        float tempSpeed = agent.speed;
        agent.speed = tempSpeed*Random.Range(0.8f, 1.2f);
    }

    private void Update()
    {
        if (!playerStatus.dead)
        {
            if (target != null)
            {
                agent.SetDestination(target.position);
            }
            else agent.ResetPath();
        }
        else agent.ResetPath();

    }

    private void FixedUpdate()
    {
        if (animated)
        {

            aVelocity = agent.velocity.normalized;

            if (aVelocity.x == 0 && aVelocity.y == 0)
            {
                return;
            }
            anim.SetFloat("moveX", aVelocity.x);
            anim.SetFloat("moveY", aVelocity.y);
        }

    }
}
