using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class Objectives : MonoBehaviour
{
    [System.Serializable]

    public class Objective
    {
        public string description;
        public bool completed;
        public Sprite awardSprite;
        public string awardDescription;
        public ObjectiveStats objectiveStats;

    }

    public Objective[] availebleObjectives;

    public GameObject objectivesPanel;

    public bool hideCompleted, objectivesDisplayed;
    public GameObject objectiveItem;
    public Transform objectiveGrid;

    private float  amountCompleted;
    private float percentCompleted;
    public TextMeshProUGUI percentDisplay;

    public Image hideButtonImage;
    public Sprite lit;
    public Sprite unLit;
    private SavedStats stats;

 
    void Start()
    {
        objectivesPanel.SetActive(false);
        stats = GameObject.FindGameObjectWithTag("CoinStash").GetComponent<SavedStats>();
        
        
    }
    public void CheckIfObjectivesAreCompleted()
    {
        if (stats.enemiesKilled100)
        {
            availebleObjectives[0].completed = true;
        }
        else availebleObjectives[0].completed = false;

        if (stats.enemiesKilled1000)
        {
            availebleObjectives[1].completed = true;
        }
        else availebleObjectives[1].completed = false;

        if (stats.hankSavedLevel1)
        {
            availebleObjectives[2].completed = true;
        }
        else availebleObjectives[2].completed = false;

        if (stats.allHumansSavedLevel1)
        {
            availebleObjectives[3].completed = true;
        }
        else availebleObjectives[3].completed = false;

        if (stats.allKeysFound)
        {
            availebleObjectives[4].completed = true;
        }
        else availebleObjectives[4].completed = false;

        if (stats.stayAliveFor20min)
        {
            availebleObjectives[5].completed = true;
        }
        else availebleObjectives[5].completed = false;

    }

    public void DisplayObjectives()
    {  
        amountCompleted = 0;
        percentCompleted = 0;

       
        for (int i = 0; i < availebleObjectives.Length; i++)
        {

            if (hideCompleted)
            {
                if (availebleObjectives[i].completed)
                {
                    amountCompleted++;
                }
                else
                {
                    GameObject objectiveListItem = Instantiate(objectiveItem);
                    objectiveListItem.TryGetComponent<ObjectiveStats>(out ObjectiveStats objStat);
                    availebleObjectives[i].objectiveStats = objStat;

                    objectiveListItem.transform.SetParent(objectiveGrid);
                    objectiveListItem.gameObject.transform.localScale = new Vector3(1, 1, 1);

                    objStat.description.text = availebleObjectives[i].description;
                    objStat.awardSprite.sprite = availebleObjectives[i].awardSprite;
                    objStat.awardText.text = availebleObjectives[i].awardDescription;
                    objStat.indicator.sprite = objStat.unlit;
                }
            }
            else
            {
                
                GameObject objectiveListItem = Instantiate(objectiveItem);
                objectiveListItem.TryGetComponent<ObjectiveStats>(out ObjectiveStats objStat);
                availebleObjectives[i].objectiveStats = objStat;

                objectiveListItem.transform.SetParent(objectiveGrid);
                objectiveListItem.gameObject.transform.localScale = new Vector3(1, 1, 1);

                objStat.description.text = availebleObjectives[i].description;
                objStat.awardSprite.sprite = availebleObjectives[i].awardSprite;
                objStat.awardText.text = availebleObjectives[i].awardDescription;

                if (availebleObjectives[i].completed)
                {
                    amountCompleted++;
                    objStat.indicator.sprite = objStat.lit;
                }
                else objStat.indicator.sprite = objStat.unlit;
            }
            
        }

        percentCompleted = (amountCompleted / availebleObjectives.Length) * 100;
        if (percentCompleted < 10)
        {
            percentDisplay.text = "" + percentCompleted.ToString("0") + "%";
        }
        else percentDisplay.text = "" + percentCompleted.ToString("00") + "%";
        
        objectivesDisplayed = true;

    }

    public void ToggleHideShowCompleted()
    {
        ResetObjectives();

        if (hideCompleted)
        {
            hideCompleted = false;
            hideButtonImage.sprite = unLit;
            DisplayObjectives();
        }
        else
        {
            hideCompleted = true;
            hideButtonImage.sprite = lit;
            DisplayObjectives();
        }
    }
    public void ToggleObjectivePanel()
    {
        if (!objectivesPanel.activeSelf)
        {
            ResetObjectives();
            CheckIfObjectivesAreCompleted();
            DisplayObjectives();
            objectivesPanel.SetActive(true);
        }

        else objectivesPanel.SetActive(false);
       
    }

    public void ResetObjectives()
    {
        if (objectivesDisplayed)
        {
            if (hideCompleted)
            {
                for (int i = 0; i < availebleObjectives.Length; i++)
                {
                    if (!availebleObjectives[i].completed)
                    {
                        Destroy(availebleObjectives[i].objectiveStats.gameObject);
                    }
                }
            }
            else
            {
                for (int i = 0; i < availebleObjectives.Length; i++)
                {
                    Destroy(availebleObjectives[i].objectiveStats.gameObject);

                }
            }
        }
    }
}
