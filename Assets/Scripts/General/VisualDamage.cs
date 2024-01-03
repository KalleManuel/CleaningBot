using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisualDamage : MonoBehaviour
{
    public bool visualDmg;
    public float visualEffectTime = 0.5f;
    public float visualDmgTimer = 0.3f;
    public Color orgCol, red;
    private SpriteRenderer tintColor;

    // Start is called before the first frame update
    void Start()
    {
        tintColor = GetComponent<SpriteRenderer>();
        visualDmg = false;
        orgCol = tintColor.color;
        visualDmgTimer = visualEffectTime;

    }

    // Update is called once per frame
    void Update()
    {
        if (visualDmg)
        {
            tintColor.color = red;

            if (visualDmgTimer > 0)
            {
                visualDmgTimer -= Time.deltaTime;
            }
            else
            {
                visualDmgTimer = visualEffectTime;
                tintColor.color = orgCol;
                visualDmg = false;
            }

        }
    }
}
