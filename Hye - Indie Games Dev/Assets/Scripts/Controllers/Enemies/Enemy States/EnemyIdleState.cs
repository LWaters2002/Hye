using UnityEngine;

public class EnemyIdleState : EnemyBaseState
{
    float dirCooldown =2f;
    float dirTimer = 2f;

    Vector3 randDir;
    Vector3 moveDir;

    public override void OnSwitch()
    {

    }

    public EnemyIdleState(Enemy enemy) : base(enemy) { }

    public override System.Type Tick()
    {
        dirTimer += Time.deltaTime;
        if (dirTimer > dirCooldown)
        {
            dirTimer = dirCooldown;
            randDir = Random.insideUnitSphere;
            randDir = new Vector3(randDir.x,0,randDir.z);
        }

        moveDir = Vector3.Lerp(moveDir,randDir, Time.deltaTime*.5f);

        if (Mathf.Abs(Vector3.Distance(enemy.player.transform.position, enemy.transform.position)) < enemy.senseRange) { return typeof(EnemyFollowState); }
        return null;
    }

    public override void FixedTick()
    {
        rb.AddForce(enemy.speed * moveDir.normalized, ForceMode.Acceleration);
    }

    public override void OnCollisionEnter(Collision collision) { }
}
