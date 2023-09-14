// Miguel Rodríguez Gallego
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
///     Manages action methods of action tree logic of Hunter
/// </summary>
public class ActionManager_Hunter : MonoBehaviour
{
    EntityStats stats;
    TargetDetector_Hunter targetDetector;
    [HideInInspector] public Rigidbody rb;
    [SerializeField] Vector3 preyLastPosition;

    [SerializeField] GameObject target;
    [SerializeField] GameObject attackableTarget;

    //[Header("Parameters (spawn another entity after killing it)")]
    //[SerializeField] GameObject preyPrefab;
    //[SerializeField] Transform[] spawnPoints;
    //[SerializeField] GameObject entityTransformParent;

    [Header("Visual state feedback")]
    [SerializeField] Image stateImage_attacking;
    [SerializeField] Image stateImage_following;
    [SerializeField] Image stateImage_searching;
    [SerializeField] Image stateImage_resting;

    /// <summary>
    ///     Movement values towards Citizen - Zombie => Chase - Escape
    /// </summary>
    [Header("Movement values")]
    public float rotationSpeed = 500;
    public float movementSpeed = 500;
    public float attackSpeed = 500;
    public float followSpeed = 500;
    public float maxSpeed = 10f;
    [SerializeField] bool randmRotateOnStart = false;

    /// <summary>
    ///     Controls what part of the routine must be played
    /// </summary>
    float chillingTimer = 0;
    /// <summary>
    ///     Routine randomly generated values
    /// </summary>
    int walkWait;
    int walkTime;
    int rotateWait;
    int rotateOrNot;
    int rotationTime;
    float rotationQuantity;


    void Awake()
    {
        stats = GetComponent<EntityStats>();
        rb = GetComponent<Rigidbody>();
        targetDetector = GetComponent<TargetDetector_Hunter>();
    }
    void Update()
    {
        target = targetDetector.target;
        attackableTarget = targetDetector.attackableTarget;
        preyLastPosition = targetDetector.lastPositionSeen;
    }

    /// <summary>
    ///     When hunter collides prey, he attacks/kills it!
    /// </summary>
    void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Prey")
        {
            Kill();
            rb.velocity = Vector3.zero;
            //Transform spawn = spawnPoints[Random.Range(0, spawnPoints.Length)];
            //Instantiate(preyPrefab, spawn).transform.parent = entityTransformParent.transform;            
        }
    }

    /// <summary>
    ///     Regens stamina automatically with time
    /// </summary>
    void ChillRoutineLogic()
    {
        if (chillingTimer <= 0)
        {
            walkWait = Random.Range(1, 3);
            walkTime = walkWait + Random.Range(0, 3);
            rotateWait = walkTime + Random.Range(0, 3);
            rotateOrNot = Random.Range(1, 2);
            rotationTime = rotateWait + Random.Range(0, 3);
            rotationQuantity = Random.Range(-180, 180);

            rb.constraints = RigidbodyConstraints.FreezeRotation;
        }

        chillingTimer += Time.deltaTime;
        Wander();
    }
    /// <summary>
    ///     Move around logic
    /// </summary>
    void Wander()
    {
        if (chillingTimer <= walkWait)
            return;

        if (chillingTimer <= walkTime)
        {
            Vector3 acceleration = transform.forward * movementSpeed * Time.fixedDeltaTime;
            acceleration.y = 0;
            rb.velocity = acceleration;
            return;
        }
        if (chillingTimer <= rotateWait)
            return;

        if (rotateOrNot == 1)
        {
            if (chillingTimer <= rotationTime)
            {
                transform.Rotate(transform.up * Time.deltaTime * rotationQuantity);
                return;
            }
        }
        chillingTimer = 0;
    }
    /// <summary>
    ///     Regens stamina automatically with time
    /// </summary>
    void MoveTowards(GameObject target, float speed)
    {
        Vector3 direction = (target.transform.position - gameObject.transform.position).normalized;
        direction.y = 0;
        Vector3 acceleration = direction * speed * Time.fixedDeltaTime;

        stats.UseStamina();

        if (acceleration.magnitude > maxSpeed)
        {
            acceleration.Normalize();
            acceleration *= maxSpeed;
        }

        gameObject.transform.LookAt(direction * rotationSpeed * Time.fixedDeltaTime);
        rb.velocity = acceleration;
    }

    /// <summary>
    ///     Attack prey / destroy it
    /// </summary>
    public void Attack()
    {
        MoveTowards(attackableTarget, attackSpeed);
        stateImage_attacking.gameObject.SetActive(true);
        stateImage_following.gameObject.SetActive(false);
        stateImage_searching.gameObject.SetActive(false);
        stateImage_resting.gameObject.SetActive(false);
    }
    public void Kill()
    {
        if (target == null) return;

        target.SetActive(false);
    }
    /// <summary>
    ///     When prey is on sight, hunter follows it till it gets to its position
    /// </summary>
    public void Follow()
    {
        MoveTowards(target, followSpeed);
        stateImage_attacking.gameObject.SetActive(false);
        stateImage_following.gameObject.SetActive(true);
        stateImage_searching.gameObject.SetActive(false);
        stateImage_resting.gameObject.SetActive(false);
    }
    /// <summary>
    ///     Search for target while isn't on sight
    /// </summary>
    public void Search()
    {
        ChillRoutineLogic();
        stateImage_attacking.gameObject.SetActive(false);
        stateImage_following.gameObject.SetActive(false);
        stateImage_searching.gameObject.SetActive(true);
        stateImage_resting.gameObject.SetActive(false);
    }
    /// <summary>
    ///     Hunter doesn't have stamina to follow prey
    ///     Played when (stamina == 0) and till it is fully recovered
    /// </summary>
    public void Rest()
    {
        rb.velocity = Vector3.zero;
        stats.RegenStamina();
        stateImage_attacking.gameObject.SetActive(false);
        stateImage_following.gameObject.SetActive(false);
        stateImage_searching.gameObject.SetActive(false);
        stateImage_resting.gameObject.SetActive(true);
    }
}
