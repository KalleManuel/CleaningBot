using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerkSpawnEffect : MonoBehaviour
{
    public PerkSpawner perkSpawn;
    public void SpawnEffect()
    {
        perkSpawn.SpawnItem();
    }
}
