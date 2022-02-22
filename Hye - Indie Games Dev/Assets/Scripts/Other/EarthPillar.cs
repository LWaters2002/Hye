using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthPillar : StatusSpawnable
{
    public float launchForce;
    public float maxHealth = 20.0f;
    private bool isMoving = true;
    public float initForce;
    public float pullForce;
    public float waitTime;
    private float waitTimer;

    public float health { get; private set; }

    private Vector3 forceDir;
    private Vector3 startPos;
    private List<GameObject> launchedObjects = new List<GameObject>();
    private Rigidbody rb;

    private void Start()
    {
        waitTimer = waitTime;
        health = maxHealth;
        startPos = transform.position;

        rb = GetComponent<Rigidbody>();
        rb.AddForce(forceDir*initForce, ForceMode.VelocityChange);
    }
    // Update is called once per frame
    protected override void Update()
    {
        base.Update();

        isMoving = (rb.velocity.magnitude > .1f);

        if (health < 0)
        {
            Destroy(gameObject);
        }

        if (waitTimer > 0) { waitTimer -= Time.deltaTime; }
    }

    private void FixedUpdate()
    {
        if (waitTimer < 0)
        {
            rb.AddForce(-forceDir * pullForce, ForceMode.Acceleration);

            if (Vector3.Distance(transform.position,startPos) < .5f)
            {
                Destroy(gameObject);
            }
        }
    }


    private void OnTriggerEnter(Collider other)
    {

        float dotDir = Vector3.Dot((other.transform.position - transform.position).normalized, forceDir.normalized);

        if (dotDir > .2 && !launchedObjects.Contains(other.gameObject))
        {
            if (other.gameObject.TryGetComponent(out Rigidbody otherRb) && isMoving)
            {
                otherRb.AddForce(forceDir * launchForce, ForceMode.VelocityChange);
                launchedObjects.Add(other.gameObject);
                Debug.Log("launched");
            }
            else if (isMoving)
            {
                Rigidbody parentRb = other.gameObject.GetComponentInParent<Rigidbody>();
                if (parentRb != null)
                {
                    parentRb.AddForce(forceDir * launchForce, ForceMode.VelocityChange);
                    launchedObjects.Add(other.gameObject);
                }
            }
        }
    }

    public override void SetupSpawnable(GameObject spawner, RaycastHit hitInfo)
    {
        Collider[] tColliders = GetComponentsInChildren<Collider>();
        foreach (Collider col in tColliders)
        {
            Physics.IgnoreCollision(col, hitInfo.collider, true);
        }

        launchedObjects.Add(hitInfo.collider.gameObject);

        forceDir = hitInfo.normal;
    }
}
