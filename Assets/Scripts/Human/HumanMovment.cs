using UnityEngine;
using UnityEngine.AI;

public class HumanMovment : MonoBehaviour
{
    public Transform target;
    NavMeshAgent agent;

    public bool beenFound, targetingEnemy;

    public PortraitController portrait;
    public string Pcode;
    private int checktimer =2;

    public Vector2 aVelocity;
    public bool animated;
    public Animator anim;

    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        portrait = GameObject.FindGameObjectWithTag(Pcode).GetComponent<PortraitController>();

        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;

        if (animated)
        {
            if (anim == null)
            {
                anim = GetComponent<Animator>();
            }
            
        }
    }

    private void Update()
    {
        if (checktimer > 0)
        {
            checktimer--;
        }
        else transform.localRotation = Quaternion.Euler(0, 0, 0);

        if (beenFound)
        {
            if (!targetingEnemy)
            {
                if (target != null)
                {
                    agent.SetDestination(target.position);
                    portrait.FollowPlayer();
                }
            }
            
        }
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

    public void FaceSwap()
    {
        portrait.IsDead();
        portrait.alive = false;
    }
}
