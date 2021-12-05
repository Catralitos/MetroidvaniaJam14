using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBuff : Buff
{
    public int healthToRecover;

    protected override void Pickup()
    {
        PlayerHealth health = PlayerEntity.Instance.Health;
        if (health.currentHealth == health.maxHealth)
        {
            health.RecoverHealth(healthToRecover);
            Destroy(gameObject);
        }
    }
}