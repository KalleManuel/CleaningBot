using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectebles : MonoBehaviour
{
    public static Collectebles collectibles;

    public List<GameObject> bloodClots;
    public List<GameObject> blood;
    public GameObject player;

    private void Awake()

    {
        collectibles = this;
    }

    // Start is called before the first frame update
    

    // Update is called once per frame
    void Update()
    {
        if (blood.Count > 0)
        {

        }

        if (bloodClots.Count > 0)
        {
            for (int i = 0; i < bloodClots.Count; i++)
            {
                if (bloodClots[i].GetComponent<GemPickUp>().pickedUp)
                {
                    GemPickUp collectlebleObj = bloodClots[i].GetComponent<GemPickUp>();

                    bloodClots[i].transform.position = Vector3.MoveTowards(bloodClots[i].transform.position, player.transform.position, collectlebleObj.speed * Time.deltaTime);

                    if (Vector3.Distance(bloodClots[i].transform.position, player.transform.position) < 0.1)
                    {
                        if (collectlebleObj.type == GemPickUp.Type.experience)
                        {
                            Player.playerXP.GainExperience(collectlebleObj.value);
                            Destroy(bloodClots[i]);
                            bloodClots.RemoveAt(i);
                        }
                        else if (collectlebleObj.type == GemPickUp.Type.coin)
                        {
                            Player.playerInventory.AddCoin(collectlebleObj.value);
                            Destroy(bloodClots[i]);
                            bloodClots.RemoveAt(i);
                        }
                    }
                }
            }
        }
    }
}
