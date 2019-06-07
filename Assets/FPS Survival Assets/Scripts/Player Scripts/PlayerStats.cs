using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{

    [SerializeField] private Image HealthStats, StaminaStats;

    public void DisplayHealthStats(float healthVaule)
    {
        healthVaule /= 100f;
        HealthStats.fillAmount = healthVaule;
    }

    public void DisplayStaminaStats(float staminaVaule)
    {
        staminaVaule /= 100f;
        StaminaStats.fillAmount = staminaVaule;
    }
}
