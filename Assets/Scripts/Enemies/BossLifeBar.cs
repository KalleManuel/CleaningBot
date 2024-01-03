using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossLifeBar : MonoBehaviour
{
    private EnemyHealth enemyHealth;
    private float maxHealth;

    [SerializeField]
    private GameObject lifeBar;

    private float updateTimer =.5f;

    // Start is called before the first frame update
    void Start()
    {
        enemyHealth = GetComponent<EnemyHealth>();
        maxHealth = enemyHealth.health;
    }

    // Update is called once per frame
    void Update()
    {   
        if (updateTimer > 0)
        {
            updateTimer -= Time.deltaTime;
        }
        else
        {
            UpdateLifeBar();
            updateTimer = .5f;
        }
    }
    public void UpdateLifeBar()
    {
        float currentHealth = enemyHealth.health / maxHealth;
        lifeBar.transform.localScale = new Vector3(currentHealth,1f, 1f);
    }
}
