using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiritJar : MonoBehaviour
{

    public List<Enemy> enemies; //Enemies that count toward stuff
    public GameObject powerableObject;
    private IPowerable powerable; // Target that requires enemies
    private bool isComplete = false;

    private void Start() {
        powerable = powerableObject.GetComponent<IPowerable>();    
    }
    // Update is called once per frame
    void Update()
    {
        enemies.RemoveAll(x => x == null);
        if (enemies.Count == 0 && !isComplete)
        {
            powerable.SetPower(true);
            isComplete = true;
        }
    }
}
