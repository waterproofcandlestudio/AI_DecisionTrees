// Miguel Rodríguez Gallego
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
///     Stamina state stats 
/// </summary>
public class EntityStats : MonoBehaviour
{
    [Header("Stamina")]
    public float stamina;
    public float maxStamina;
    [SerializeField] float staminaWaste = 15f;
    [SerializeField] float staminaRegen = 10f;
    [SerializeField] float staminaRegenStartTime = 2f;
    [SerializeField] float staminaRegenPerSecond = 20f;
    public static bool staminaResetCooldown;
    public static bool staminaRegenerated = true;
    float staminaCountdown = 0f;
    public bool canRegenStamina;

    [SerializeField] Slider staminaProgressBar; /// Health

    void Start() => UpdateStamina_UISlider();
    void Update() => UpdateStamina();

    void UpdateStamina()
    {
        CheckStaminaState();
    }
    /// <summary>
    ///     Checks stamina state to make an action or another
    /// </summary>
    void CheckStaminaState()
    {
        if (stamina <= 0)
            canRegenStamina = true;

        if (stamina >= maxStamina)
            canRegenStamina = false;
    }

    /// <summary>
    ///     Lower stamina automatically with time
    /// </summary>
    public void UseStamina()
    {
        stamina -= staminaWaste * Time.deltaTime;
        UpdateStamina_UISlider();
    }
    /// <summary>
    ///     Regens stamina automatically with time
    /// </summary>
    public void RegenStamina()
    {
        stamina += staminaRegen * Time.deltaTime;
        UpdateStamina_UISlider();
    }
    /// <summary>
    ///     HUD Methods to show correctly stamina in slider bar
    /// </summary>
    void UpdateStamina_UISlider()
    {
        float fillAmount = stamina / maxStamina;
        staminaProgressBar.value = fillAmount;
    }
    public float GetStamina() => stamina;
    public bool GetCanRegenStamina() => canRegenStamina;
}
