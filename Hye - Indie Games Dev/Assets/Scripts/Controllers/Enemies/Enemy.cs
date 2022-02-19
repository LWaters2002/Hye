using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody))]
public abstract class Enemy : MonoBehaviour, IDamagable, IStatusable
{
    public float health { get; private set; }
    public delegate void FloatEvent(float f);
    public event FloatEvent OnHealthChange;

    public Transform groundPoint;
    public LayerMask groundMask;

    [Header("Enemy Stats")]
    public float speed;
    public float maxHealth;
    public int damage;
    public float senseRange;
    public float attackChance;
    public float attackCheckRate;
    protected float distanceToPlayer;

    // Getters and setters
    public List<EnemyWeapon> weapons { get; private set; }
    public Status[] statuses { get; private set; }
    public PlayerController player { get; private set; }
    public Rigidbody rb { get; private set; }
    public Animator an { get; private set; }
    public StateMachine<EnemyBaseState> stateMachine { get; private set; }
    [HideInInspector] public NavMeshPath path;

    public EnemyWeapon activeWeapon { get; set; }
    public UnityAction<float> OnHit;

    [HideInInspector]
    public bool isGrounded;

    // Start is called before the first frame update
    protected virtual void Awake()
    {
        activeWeapon = null;

        path = new NavMeshPath();
        // Initialises status
        statuses = new Status[4];
        for (int i = 0; i < 4; i++)
        {
            statuses[i] = new Status(50);
        }

        health = maxHealth;

        weapons = new List<EnemyWeapon>();

        weapons.AddRange(GetComponentsInChildren<EnemyWeapon>());
        rb = GetComponent<Rigidbody>();
        player = Object.FindObjectOfType<PlayerController>();
        stateMachine = new StateMachine<EnemyBaseState>();
        an = GetComponentInChildren<Animator>();

        InvokeRepeating("CheckAttacks", 0f, attackCheckRate);
        InitialiseStateMachine();
    }

    protected void CheckAttacks()
    {
        if (Random.Range(0f, 1f) > attackChance) return;

        foreach (EnemyWeapon weapon in weapons)
        {
            if (!weapon.isReady || (weapon.attackRange < distanceToPlayer)) continue;

            if (activeWeapon == null) { activeWeapon = weapon; continue; }

            //Sorts by weapon range
            if (weapon.attackRange > activeWeapon.attackRange)
            {
                activeWeapon = weapon;
            }
            else if (weapon.attackRange == activeWeapon.attackRange)
            {
                activeWeapon = (Random.Range(0f, 1f) > .5) ? activeWeapon : weapon;
            }

        }
    }

    protected abstract void InitialiseStateMachine();

    // Update is called once per frame
    protected virtual void Update()
    {
        distanceToPlayer = Vector3.Distance(player.transform.position, transform.position);
        stateMachine.Tick();
        foreach (Status status in statuses)
        {
            status.UpdateStatus();
        }
        Vector3 temp = (player.transform.position - transform.position).normalized;
        temp.y = 0;

        isGrounded = Physics.CheckSphere(groundPoint.position,.4f,groundMask);
    }

    protected virtual void FixedUpdate()
    {
        stateMachine.currentState.FixedTick();
        rb.AddForce(Vector3.down * (rb.drag), ForceMode.Acceleration);
    }

    public virtual void TakeDamage(float damageAmount, StatusType damageType)
    {
        health -= damageAmount;

        if (health <= 0) { Death(); }

        OnHealthChange?.Invoke(health);
        OnHit?.Invoke(damageAmount);
    }

    protected virtual void Death()
    {
        Destroy(gameObject);
    }

    public virtual void ApplyStatus(float statusAmount, StatusType statusRecieved)
    {
        if (statusRecieved == StatusType.none) { return; }
        statuses[(int)statusRecieved - 1].AddToStatus(statusAmount);
    }


}
