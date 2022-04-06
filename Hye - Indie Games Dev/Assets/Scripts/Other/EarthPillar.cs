using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthPillar : StatusSpawnable, IStatusable
{
    public float launchForce;
    public float maxHealth = 20.0f;
    private bool isMoving = true;
    public float initForce;
    public float pullForce;
    public float waitTime;
    public GameObject destroyParticlePrefab;

    [Header("Elemental")]
    public ParticleSystem fireParticles;
    public float fireForce;
    public float fireDrag;
    
    public EarthEarth earthEarthPrefab;

    private float waitTimer;

    public float health { get; private set; }
    public float damage;

    private Vector3 forceDir;
    private Vector3 startPos;
    private List<GameObject> launchedObjects = new List<GameObject>();
    private Rigidbody rb;
    private bool isOnFire;

    private void Start()
    {
        waitTimer = waitTime;
        health = maxHealth;
        startPos = transform.position;

        rb = GetComponent<Rigidbody>();
        rb.AddForce(forceDir * initForce, ForceMode.VelocityChange);
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
        if (waitTimer < 0 && isOnFire == false)
        {
            rb.AddForce(-forceDir * pullForce, ForceMode.Acceleration); //Returns to the earth through pullforce

            if (Vector3.Distance(transform.position, startPos) < .5f)
            {
                Destroy(gameObject);
            }
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (isOnFire)
        {

            IDamagable dg = other.gameObject.GetComponentInParent<IDamagable>();
            if (dg != null)
            {
                dg.TakeDamage(damage, StatusType.fire, transform.position);
            }

            if (other.gameObject.layer == LayerMask.NameToLayer("EnemyProjectile"))
            {
                Destroy(other.gameObject);
            }

            Destroy(gameObject);
        }
        else
        {
            float dotDir = Vector3.Dot((other.transform.position - transform.position).normalized, forceDir.normalized); //Gets Direction of encounter.

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

    public void ApplyStatus(GameObject obj, float statusAmount, StatusType statusRecieved)
    {
        switch (statusRecieved)
        {
            case StatusType.none:
                break;
            case StatusType.earth:
                SpawnEarthEarth();
                Destroy(gameObject);
                break;
            case StatusType.water:
                break;
            case StatusType.air:
                break;
            case StatusType.fire:
                isOnFire = true;
                fireParticles.Play();

                rb.useGravity = true;
                rb.drag = fireDrag;

                foreach (Transform t in transform) //Sets the layer to default to prevent people jumping on it and so it can hit static enemies. :D 
                {
                    t.gameObject.layer = 0;
                }

                rb.AddForce(obj.transform.forward * fireForce, ForceMode.VelocityChange);
                Destroy(obj);
                break;
            default:
                break;
        }

    }

    void SpawnEarthEarth() 
    {
        EarthEarth e = Instantiate(earthEarthPrefab, transform.position, Quaternion.identity);

        e.Init();
    }

    void OnDestroy()
    {
        Instantiate(destroyParticlePrefab, transform.position, Quaternion.identity);
    }
}
