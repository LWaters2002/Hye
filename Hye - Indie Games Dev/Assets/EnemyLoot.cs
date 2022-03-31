using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLoot : MonoBehaviour
{
    //Item Drops
    public Item[] lootDrops;
    public LootDrop dropPrefab;

    public int ID;

    public void SpawnLoot(int idOfLootScript)
    {
        if (idOfLootScript != ID) { return; }

        Instantiate(dropPrefab, transform.position, Quaternion.identity);
    }

}
