using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{

    public float knockbackLength;
    public float knockbackHorizontalStrength;
    public float knockbackVerticalStrength;

    private Rigidbody2D _rb;
    private RigidbodyConstraints2D _initialCons;

    [HideInInspector] public int currentHealth;
    public int maxHealth;
    public int healthPerMaxIncrement;
    
    // Start is called before the first frame update
    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _initialCons = _rb.constraints;
        currentHealth = maxHealth;
    }
    
    public void RecoverHealth(int healthToRecover)
    {
        currentHealth = Mathf.Clamp(currentHealth + healthToRecover, 0, maxHealth);

    }
    
    public void Hit(int damage)
    {
        //fazer dano
        PlayerEntity.Instance.frozeControls = true;
        _rb.velocity = Vector2.zero;
        int direction = PlayerEntity.Instance.facingRight ? -1 : 1;
        _rb.velocity = new Vector2(direction * knockbackHorizontalStrength, knockbackVerticalStrength);
        Invoke(nameof(RestoreControls), knockbackLength);
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        PlayerEntity.Instance.frozeControls = true;
        Invoke(nameof(ReloadLevel), 3f);
    }

    private void ReloadLevel()
    {
        //recarregar o nivel
    }
    
    private void RestoreControls()
    {
        PlayerEntity.Instance.frozeControls = false;
    }

    public void IncreaseMaxHealth()
    {
        maxHealth += healthPerMaxIncrement;
        currentHealth += healthPerMaxIncrement;
    }
}