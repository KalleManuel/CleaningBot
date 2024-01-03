
using UnityEngine;

public class CamFollowPlayer : MonoBehaviour
{
    [SerializeField]
    private Transform target;

    [SerializeField]
    private bool followSmooth = true;
    
    [SerializeField]
    private float smoothSpeed = 0.125f;

    [SerializeField]
    private Vector3 offset;

    void FixedUpdate()
    {
        if (target != null)
        {
            if (followSmooth)
            {
                Vector3 desiredPosition = target.position + offset;
                Vector3 smothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
                transform.position = smothedPosition;
            }

            else transform.position = target.position;
        }
        
             
    }
}
