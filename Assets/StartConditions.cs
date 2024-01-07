using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartConditions : MonoBehaviour
{
    public GameObject freeChoice;
    SavedStats stats;
    // Start is called before the first frame update
    void Start()
    {
        stats = GameObject.FindGameObjectWithTag("CoinStash").GetComponent<SavedStats>();
        freeChoice.SetActive(false);

        if (stats.amountExtraStartItems > 0) 
        {
            GetComponent<Pause>().PauseGame(false);
            freeChoice.SetActive(true);
        }
    }
    public void CloseFreeChoice()
    {
        freeChoice.SetActive(false);
    }

}
