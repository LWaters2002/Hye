using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TestNav : MonoBehaviour
{

    public float pathUpdateRate;
    public float speed;
    PlayerController pc;
    Rigidbody rb;
    NavMeshAgent nma;
    NavMeshPath path;

    // Start is called before the first frame update
    void Start()
    {
        pc = Object.FindObjectOfType<PlayerController>();
        rb = GetComponent<Rigidbody>();
        path = new NavMeshPath();
        InvokeRepeating("UpdatePath", 0f, pathUpdateRate);
    }

    void UpdatePath()
    {
        NavMesh.CalculatePath(transform.position,pc.gameObject.transform.position,NavMesh.AllAreas, path);
    }

    private void FixedUpdate()
    {
        if (path != null)
        {
            if (path.corners.Length > 1)
            {
                Vector3 dir = (path.corners[1] - transform.position).normalized;
                rb.AddForce(dir * speed, ForceMode.Acceleration);
            }
        }
    }

    private void OnDrawGizmos()
    {
        if (path != null)
        {
            foreach (Vector3 corner in path.corners)
            {
                Gizmos.color = Color.blue;
                Gizmos.DrawSphere(corner, 1f);
            }

        }
    }
}
