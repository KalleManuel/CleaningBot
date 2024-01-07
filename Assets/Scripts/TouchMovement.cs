using Microsoft.Unity.VisualStudio.Editor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchMovement : MonoBehaviour
{
    PlayerMovment player;

    public bool toutchStart;
    public Vector2 pointA;
    public Vector2 pointB;
    public Vector2 offset;
    public Vector2 direction;

    public Transform circle;
    public Transform outerCircle;

    Pause pause;

    // Start is called before the first frame update
    void Start()
    {
    
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovment>();
        pause = GameObject.FindGameObjectWithTag("GameController").GetComponent<Pause>();
    
    }

    // Update is called once per frame
    void Update()
    {
        if (!pause.gamePaused)
        {
            if (Input.GetMouseButtonDown(0))
            {
                pointA = Input.mousePosition;

                circle.transform.position = pointA;
                outerCircle.transform.position = pointA;
                circle.gameObject.SetActive(true);
                outerCircle.gameObject.SetActive(true);
            }
            if (Input.GetMouseButton(0))
            {
                toutchStart = true;
                pointB = Input.mousePosition;
            }
            else
            {
                toutchStart = false;
            }
        }
        
    }
    private void FixedUpdate()
    {
        if (toutchStart)
        {
             offset = pointB - pointA;
             direction = Vector2.ClampMagnitude(offset, 1.0f);
            player.directionX = direction.x;
            player.directionY = direction.y;

            circle.transform.position = new Vector2(pointA.x + direction.x,pointA.y + direction.y);
        }
        else
        {
           player.directionX = 0.0f;
            player.directionY = 0.0f;
            circle.gameObject.SetActive(false);
            outerCircle.gameObject.SetActive(false);
        }
    }
}
