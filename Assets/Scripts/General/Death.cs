using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Death : MonoBehaviour
{
    public GameObject deadBody;
    public void Dead()
    {
        if (this.gameObject.tag == "Player")
        {
            
             Statistic stats = GameObject.FindGameObjectWithTag("GameController").GetComponent<Statistic>();
            stats.UpdateStats();
           // gameObject.SetActive(false);
            
            GameObject.FindGameObjectWithTag("GameController").GetComponent<GameOver>().GameOverScreen(true,false);
           
        }

        else if (this.gameObject.tag == "BodySoul")
        {
            GetComponentInParent<HumanMovment>().FaceSwap();
            string deathMessage = GetComponent<Health>().characterName + " is dead";
            
            StartCoroutine(PlayerHud.playerHud.SendMessageToHUD(deathMessage, 3, false));
            Instantiate(deadBody, transform.position, transform.rotation);
            Destroy(transform.parent.gameObject);

            PlayerHud.playerHud.UpdateHumansInGame();
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCompanions>().UpdateHumanCompanions();

        }


    }
}
