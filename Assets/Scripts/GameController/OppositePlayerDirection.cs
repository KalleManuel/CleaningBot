using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OppositePlayerDirection : MonoBehaviour
{

    private PlayerMovment playerMovement;
    private Vector2 direction;
    public float rotateAngle;
    public float offset;

    

    // Start is called before the first frame update
    void Start()
    {
        playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovment>();
        
    }

    // Update is called once per frame
    void Update()
    {
        MapPlayerDirection();
        Turn();

        
    }

    public void MapPlayerDirection()
    {
        direction = playerMovement.direction;

        if (direction.x == 1 && direction.y == 0)
        {
            rotateAngle = 0;
        }

        else if (direction.x == 1 && direction.y == 1)
        {
            rotateAngle = 45;
        }
        else if (direction.x == 0 && direction.y == 1)
        {
            rotateAngle = 90;
        }
        else if (direction.x == -1 && direction.y == 1)
        {
            rotateAngle = 135;
        }
        else if (direction.x == -1 && direction.y == 0)
        {
            rotateAngle = 180;
        }
        else if (direction.x == -1 && direction.y == -1)
        {
            rotateAngle = 225;
        }
        else if (direction.x == 0 && direction.y == -1)
        {
            rotateAngle = 270;
        }
        else if (direction.x == 1 && direction.y == -1)
        {
            rotateAngle = 315;
        }
        
    }
    public void Turn()
    {
        transform.rotation = Quaternion.Euler(0, 0, rotateAngle + offset);
    }

}
