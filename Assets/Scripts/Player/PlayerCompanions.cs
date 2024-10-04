using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCompanions : MonoBehaviour
{
    public List <GameObject> humanCompanions;
    
    public EndGame endTrigger;
    

    private void Start()
    {
        
        
    }

    public void AddCompanion(GameObject _human)
    {
        humanCompanions.Add(_human);
        PlayerHud.playerHud.humansWithPlayer = humanCompanions.Count;
        PlayerHud.playerHud.UpdateHumanCollected();

        if (humanCompanions.Count == 6)
        {
            endTrigger.ShowPickUpPoint();
        }

    }
    public void UpdateHumanCompanions() // why doesn't it remove when a human dies?
    {
        if (humanCompanions.Count > 0)
        {
            for (int i = 0; i < humanCompanions.Count; i++)
            {
                if (humanCompanions[i] == null)
                {
                    Debug.Log("empty slot-problem");
                    humanCompanions[i] = null;
                    humanCompanions.RemoveAt(i);
                }

            }
        }
        PlayerHud.playerHud.humansWithPlayer = humanCompanions.Count;
        PlayerHud.playerHud.UpdateHumanCollected();

    }



}