using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCompanions : MonoBehaviour
{
    public List <GameObject> humanCompanions;
    private PlayerHud playerHud;
    public EndGame endTrigger;
    

    private void Start()
    {
        playerHud = GameObject.FindGameObjectWithTag("GameController").GetComponent<PlayerHud>();
        
    }

    public void AddCompanion(GameObject _human)
    {
        humanCompanions.Add(_human);
        playerHud.humansWithPlayer = humanCompanions.Count;
        playerHud.UpdateHumanCollected();

        if (humanCompanions.Count == 6)
        {
            endTrigger.ShowPickUpPoint();
        }

    }
    public void UpdateHumanCompanions()
    {
        if (humanCompanions.Count > 0)
        {
            for (int i = 0; i < humanCompanions.Count; i++)
            {
                if (humanCompanions[i] == null)
                {
                    humanCompanions.RemoveAt(i);
                }

            }
        }
        playerHud.humansWithPlayer = humanCompanions.Count;
        playerHud.UpdateHumanCollected();

    }



}