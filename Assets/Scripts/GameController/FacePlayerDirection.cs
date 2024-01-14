using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FacePlayerDirection : MonoBehaviour
{

    private PlayerMovment playerMovement;
    private Vector2 direction;
    public float rotateAngle;

    [SerializeField]
    private Item_Whip whip;
    

    // Start is called before the first frame update
    void Start()
    {
        playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovment>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (whip.activated)
        {
            MapPlayerDirection();
        }
    }

    public void MapPlayerDirection()
    {
        direction = playerMovement.direction;

        if (direction.x > .1f && direction.y == 0)
        {
            rotateAngle = 0;
        }

        else if (direction.x > .1f && direction.y > .1f)
        {
            rotateAngle = 45;
        }
        else if (direction.x == 0 && direction.y > .1f)
        {
            rotateAngle = 90;
        }
        else if (direction.x < -.1f && direction.y > .1f)
        {
            rotateAngle = 135;
        }
        else if (direction.x < -.1f && direction.y == 0)
        {
            rotateAngle = 180;
        }
        else if (direction.x < -.1f && direction.y < -.1f)
        {
            rotateAngle = 225;
        }
        else if (direction.x == 0 && direction.y < -.1f)
        {
            rotateAngle = 270;
        }
        else if (direction.x > .1f && direction.y < -.1f)
        {
            rotateAngle = 315;
        }

        
    }
    public void AimWhip()
    {
        transform.rotation = Quaternion.Euler(0, 0, rotateAngle);
    }

}
