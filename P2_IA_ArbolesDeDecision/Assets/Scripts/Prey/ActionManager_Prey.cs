// Miguel Rodríguez Gallego
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
///     Manages action methods of action tree logic of Prey
/// </summary>
public class ActionManager_Prey : MonoBehaviour
{
    EntityStats stats;
    TargetDetector_Prey targetDetector;
    [HideInInspector] public Rigidbody rb;

    [SerializeField] GameObject target;

    [Header("Visual state feedback")]
    [SerializeField] Image stateImage_escaping;
    [SerializeField] Image stateImage_chilling;
    [SerializeField] Image stateImage_lookingOut;
    [SerializeField] Image stateImage_resting;

    /// <summary>
    ///     Movement values towards Citizen - Zombie => Chase - Escape
    /// </summary>
    [Header("Movement values")]
    public float rotationSpeed = 500;
    public float movementSpeed = 500;
    public float escapeSpeed = 500;
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
        targetDetector = GetComponent<TargetDetector_Prey>();
    }
    void Update()
    {
        target = targetDetector.target;
    }
    /// <summary>
    ///     Escape opposite direction of target logic
    /// </summary>
    void EscapeOpositeDirection(GameObject target, float speed)
    {
        Vector3 direction = -(target.transform.position - gameObject.transform.position).normalized;
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
    ///     If prey sees hunter, it runs away from him on opposite direction
    /// </summary>
    public void Escape()
    {
        EscapeOpositeDirection(target, escapeSpeed);

        stateImage_escaping.gameObject.SetActive(true);
        stateImage_chilling.gameObject.SetActive(false);
        stateImage_lookingOut.gameObject.SetActive(false);
        stateImage_resting.gameObject.SetActive(false);
    }
    /// <summary>
    ///     While there's no hunter in the area, it doesn't do anything
    /// </summary>
    public void Chill()
    {
        ChillRoutineLogic();

        stateImage_escaping.gameObject.SetActive(false);
        stateImage_chilling.gameObject.SetActive(true);
        stateImage_lookingOut.gameObject.SetActive(false);
        stateImage_resting.gameObject.SetActive(false);
    }
    /// <summary>
    ///     Lookout when hunter is very near, so prey stops and looks towards it
    /// </summary>
    public void LookOut()
    {
        Vector3 direction = -(target.transform.position - gameObject.transform.position).normalized;
        direction.y = 0;
        gameObject.transform.LookAt(direction * rotationSpeed * Time.fixedDeltaTime);

        rb.velocity = Vector3.zero;
        rb.constraints = RigidbodyConstraints.FreezeRotation;

        stateImage_escaping.gameObject.SetActive(false);
        stateImage_chilling.gameObject.SetActive(false);
        stateImage_lookingOut.gameObject.SetActive(true);
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

        stateImage_escaping.gameObject.SetActive(false);
        stateImage_chilling.gameObject.SetActive(false);
        stateImage_lookingOut.gameObject.SetActive(false);
        stateImage_resting.gameObject.SetActive(true);
    }
}
