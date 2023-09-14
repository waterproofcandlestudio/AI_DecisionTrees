// Miguel Rodríguez Gallego
using UnityEngine;

/// <summary>
///     Detects close targets
/// </summary>
public class TargetDetector_Hunter : MonoBehaviour
{
    /// <summary>
    ///     Target & direction to follow
    /// </summary>
    [HideInInspector] public GameObject target;
    [HideInInspector] public Vector3 lastPositionSeen;

    [Header("Detection parameters")]
    [SerializeField] LayerMask targetLayerMask;
    [SerializeField] int seeTargetRange = 20;
    [SerializeField] Collider[] targetsCollider;
    Transform closestTarget;

    [Header("Can attack parameters")]
    [SerializeField] Collider[] targetsCanBeAttackedCollider;
    [SerializeField] int canAttackTargetRange = 15;
    [HideInInspector] public GameObject attackableTarget;
    Transform closestAttackableTarget;

    /// <summary>
    ///     Constantly try to find if there's a citizen nearby
    /// </summary>
    void Update()
    {
        FindTarget();
    }
    /// <summary>
    ///     If there's a citizen nearby, this method instantly:
    ///         - Gets closer entity
    ///         - Adds him to targets list of citizens
    /// </summary>
    public void FindTarget()
    {
        FindTargets_CanSee();
        FindTargets_CanAttack();
    }
    /// <summary>
    ///     When prey is near, detect it to walk towards it
    /// </summary>
    void FindTargets_CanSee()
    {
        targetsCollider = Physics.OverlapSphere(transform.position, seeTargetRange, targetLayerMask);

        if (targetsCollider.Length != 0)
        {
            closestTarget = GetClosestEntity(targetsCollider);
            target = closestTarget.gameObject;
            lastPositionSeen = closestTarget.gameObject.transform.position; // Gets last position of the entitytoZ
        }
        else
        {
            targetsCollider = null;   // No citizen around in sight
            target = null;
        }
    }
    /// <summary>
    ///     When prey is extremely near, detect it and attack it
    /// </summary>
    void FindTargets_CanAttack()
    {
        targetsCanBeAttackedCollider = Physics.OverlapSphere(transform.position, canAttackTargetRange, targetLayerMask);

        if (targetsCanBeAttackedCollider.Length != 0)
        {
            closestAttackableTarget = GetClosestEntity(targetsCanBeAttackedCollider);
            attackableTarget = closestAttackableTarget.gameObject;
            lastPositionSeen = closestAttackableTarget.gameObject.transform.position; // Gets last position of the entitytoZ
        }
        else
        {
            targetsCanBeAttackedCollider = null;   // No citizen around in sight
            attackableTarget = null;
        }
    }

    /// <summary>
    ///     Gets closer entity in checking collider
    /// </summary>
    Transform GetClosestEntity(Collider[] entity)
    {
        Collider tMin = null;
        float minDist = Mathf.Infinity;
        Vector3 currentPos = transform.position;
        foreach (Collider t in entity)
        {
            float dist = Vector3.Distance(t.transform.position, currentPos);
            if (dist < minDist)
            {
                tMin = t;
                minDist = dist;
            }
        }
        return tMin.transform;
    }
}
