using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class mController : MonoBehaviour
{
    public Transform t;
    public Material m;

    void Update()
    {
        Vector3 pos = t.position - transform.position;

        m.SetVector("_ObjectPosition", new Vector4(pos.x, pos.y, pos.z, 0));
    }
}
