//using Microsoft.Unity.VisualStudio.Editor;
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

    public float circlePosX;
    public float circlePosY;

    Pause pause;
    SavedStats savedStats;

    // Start is called before the first frame update
    void Start()
    {
    
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovment>();
        pause = GameObject.FindGameObjectWithTag("GameController").GetComponent<Pause>();
        savedStats = GameObject.FindGameObjectWithTag("CoinStash").GetComponent<SavedStats>();
    }

    // Update is called once per frame
    void Update()
    {
        if (savedStats.touchControls)
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
        
        
    }
    private void FixedUpdate()
    {
        if (savedStats.touchControls)
        {
            if (!pause.gamePaused)
            {
                if (toutchStart)
                {
                    offset = pointB - pointA;
                    direction = Vector2.ClampMagnitude(offset, 1.0f);
                    player.directionX = direction.x;
                    player.directionY = direction.y;



                    if (direction.x > 0)
                    {
                        circlePosX = Mathf.Lerp(pointA.x, pointA.x + 30, direction.x);
                    }
                    else circlePosX = Mathf.Lerp(pointA.x, pointA.x - 30, direction.x * -1);

                    if (direction.y > 0)
                    {
                        circlePosY = Mathf.Lerp(pointA.y, pointA.y + 30, direction.y);
                    }
                    else circlePosY = Mathf.Lerp(pointA.y, pointA.y - 30, direction.y * -1);


                    circle.transform.position = new Vector2(circlePosX, circlePosY);
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
        
    }
}
