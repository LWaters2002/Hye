using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombPlant : MonoBehaviour
{

    public float spawnTime;
    public GameObject bombPrefab;
    [SerializeField] private GameObject bomb;
    private Transform spawnTransform;
    private float spawnTimer;

    // Start is called before the first frame update
    void Start()
    {
        spawnTimer = spawnTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (bomb == null)
        {
            spawnTimer -= Time.deltaTime;

            if (spawnTimer < 0)
            {
                bomb = Instantiate(bombPrefab,spawnTransform);
                spawnTimer = spawnTime;
            }
        }
    }
}
