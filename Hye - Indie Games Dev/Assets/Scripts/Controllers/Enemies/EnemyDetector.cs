using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyDetector : MonoBehaviour
{
    public float influence;
    public float tickRate;

    HashSet<GameObject> enemies;

    public UnityAction<Vector3> onDirectionChanged;

    void Start()
    {
        InvokeRepeating("Tick", 0, tickRate);
        enemies = new HashSet<GameObject>();
    }

    void Tick()
    {
        if (enemies.Count == 0) { onDirectionChanged?.Invoke(Vector3.zero); return; }
        enemies.Remove(null);
        Vector3 dir = Vector3.zero;
        foreach (GameObject enemy in enemies)
        {
            Vector3 temp = (transform.position - enemy.transform.position);
            dir += temp.normalized * (1 / temp.magnitude); //Increases influence the closer an enemy is the to transform of the existing player
        }
        dir = new Vector3(dir.x, 0, dir.z);
        dir = dir.normalized * influence;
        onDirectionChanged.Invoke(dir);

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Enemy>(out Enemy e))
        {
            enemies.Add(other.gameObject);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (!enemies.Contains(other.gameObject)) { return; }
        enemies.Remove(other.gameObject);
    }
}
