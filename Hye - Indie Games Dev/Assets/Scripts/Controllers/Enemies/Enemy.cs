using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
using System.Linq;

[RequireComponent(typeof(Rigidbody))]
public abstract class Enemy : MonoBehaviour, IStatusable
{
    public float health { get; protected set; }
    public delegate void FloatEvent(float f);
    public event FloatEvent OnHealthChange;

    public Transform groundPoint;
    public LayerMask groundMask;

    [Header("Enemy Stats")]
    public float speed;
    public float maxHealth;
    public int damage;
    public float senseRange;
    public float distanceToPlayer {get; private set;}

    // Getters and setters
    public List<EnemyWeapon> usableWeapons { get; private set; }

    public Status[] statuses { get; private set; }
    public PlayerController player { get; private set; }
    public Rigidbody rb { get; private set; }
    public Animator an { get; private set; }
    public StateMachine<EnemyBaseState> stateMachine { get; private set; }
    [HideInInspector] public NavMeshPath path;

    public EnemyWeapon activeWeapon { get; set; }
    public UnityAction<float> OnHit;
    public Vector3 enemyInfluence { get; private set; }

    [HideInInspector]
    public bool isGrounded;

    // Start is called before the first frame update
    protected virtual void Awake()
    {
        EnemyDetector ed = GetComponent<EnemyDetector>();
        if (ed != null) { ed.onDirectionChanged += dir => { enemyInfluence = dir; }; }

        activeWeapon = null;

        path = new NavMeshPath();
        // Initialises status
        statuses = new Status[4];
        for (int i = 0; i < 4; i++)
        {
            statuses[i] = new Status(50);
        }

        health = maxHealth;

        List<EnemyWeapon> weapons = new List<EnemyWeapon>();

        weapons.AddRange(GetComponentsInChildren<EnemyWeapon>());
        weapons.Select(x => x.OnConditionsMet += SetUsableWeapons);

        rb = GetComponent<Rigidbody>();
        player = Object.FindObjectOfType<PlayerController>();
        stateMachine = new StateMachine<EnemyBaseState>();
        an = GetComponentInChildren<Animator>();

        InitialiseStateMachine();
    }

    protected void SetUsableWeapons(EnemyWeapon weapon, bool isUsable)
    {
        if (isUsable)
        {
            usableWeapons.Add(weapon);
        }
        else
        {
            usableWeapons.Remove(weapon);
        }
    }

    protected abstract void InitialiseStateMachine();

    // Update is called once per frame
    protected virtual void Update()
    {
        //important variable updates
        distanceToPlayer = Vector3.Distance(player.transform.position, transform.position);

        

        // Ticks owned Classes
        stateMachine.Tick();

        foreach (Status status in statuses)
        {
            status.UpdateStatus();
        }

        //Checks if grounded
        Vector3 temp = (player.transform.position - transform.position).normalized;
        temp.y = 0;

        isGrounded = Physics.CheckSphere(groundPoint.position, .4f, groundMask);
    }

    protected virtual void FixedUpdate()
    {
        stateMachine.currentState.FixedTick();
        if (isGrounded) { return; }
        rb.AddForce(Vector3.down * (rb.drag) * 9.8f, ForceMode.Acceleration);
    }

    public virtual void TakeDamage(float damageAmount, StatusType damageType, Vector3 damagePos)
    {
        health -= damageAmount;
        lutils.SpawnDamageNumber(damagePos, damageAmount);
        if (Mathf.Floor(health) <= 0) { Death(); }

        OnHealthChange?.Invoke(health);
        OnHit?.Invoke(damageAmount);
    }

    protected virtual void Death()
    {
        Destroy(gameObject);
    }

    public virtual void ApplyStatus(GameObject obj, float statusAmount, StatusType statusRecieved)
    {
        if (statusRecieved == StatusType.none) { return; }
        statuses[(int)statusRecieved - 1].AddToStatus(statusAmount);
    }


}
