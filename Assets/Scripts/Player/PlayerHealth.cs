using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int currentHealth { get; private set; }
    public int maxHealth;
    
    
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    public void RecoverHealth(int healthToRecover)
    {
        currentHealth = Mathf.Clamp(currentHealth + healthToRecover, 0, maxHealth);
        
    }
}
