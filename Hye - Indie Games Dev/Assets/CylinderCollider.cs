using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class CylinderCollider : MonoBehaviour
{
    public float radius;
    [Range(0, 1)]
    public float radiusThickness;
    public float height;

    public LayerMask mask;

    public float pollRate;

    int maxColliders = 16;

    public Action<Collider> overlapEvent;

    void Start()
    {
        StartCoroutine("PollPhysics");
    }

    IEnumerator PollPhysics()
    {
        Collider[] overlapColliders = new Collider[maxColliders];

        while (true)
        {
            int colliderCount = Physics.OverlapSphereNonAlloc(transform.position, radius, overlapColliders, mask);

            for (int i = 0; i < colliderCount; i++)
            {
                Vector3 cPos = overlapColliders[i].transform.position;

                if (Vector3.Distance(transform.position, cPos) < radius * radiusThickness || (cPos.y > transform.position.y + height || cPos.y < transform.position.y)) { continue; }

                overlapEvent?.Invoke(overlapColliders[i]);
            }

            yield return new WaitForSeconds(pollRate);
        }
    }

    void OnDrawGizmos()
    {
        Handles.DrawWireDisc(transform.position, transform.up, radius);
        Handles.DrawWireDisc(transform.position + Vector3.up * height, transform.up, radius);

        Handles.DrawWireDisc(transform.position, transform.up, radius * radiusThickness);
        Handles.DrawWireDisc(transform.position + Vector3.up * height, transform.up, radius * radiusThickness);
    }
}
