using UnityEngine;
using UnityEngine.Events;

public class Blazebox : MonoBehaviour
{
    public BlazeBoxSub childPrefab;

    public UnityAction triggered;

    public void Init(FireArrowEffect fae)
    {
        fae.startCyclone += CycloneTriggered;
    }

    void OnTriggerEnter(Collider other)
    {
        BaseArrow arrow = other.gameObject.GetComponent<BaseArrow>();

        if (arrow == null) { return; }
        if (arrow.statusType != StatusType.air) { return; }

        CycloneTriggered();
    }

    public void CycloneTriggered()
    {
        triggered?.Invoke();
        Instantiate(childPrefab, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
