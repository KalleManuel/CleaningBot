using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    public Transform target;
    NavMeshAgent agent;
    public bool WaitFollowPlayer;
    
    private GameOver playerStatus;

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
}
