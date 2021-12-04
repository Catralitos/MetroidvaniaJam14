using UnityEngine;

public class PlayerHealth : MonoBehaviour
{

    public float knockbackLength;
    public float knockbackHorizontalStrength;
    public float knockbackVerticalStrength;

    private Rigidbody2D _rb;
    private RigidbodyConstraints2D _initialCons;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _initialCons = _rb.constraints;
    }
    
    private void FixedUpdate()
    {
       
    }

    public void Hit(int damage)
    {
        //fazer dano
        PlayerEntity.Instance.frozeControls = true;
        _rb.velocity = Vector2.zero;
        int direction = PlayerEntity.Instance.facingRight ? -1 : 1;
        _rb.velocity = new Vector2(direction * knockbackHorizontalStrength, knockbackVerticalStrength);
        Invoke(nameof(RestoreControls), knockbackLength);
    }

    private void RestoreControls()
    {
        PlayerEntity.Instance.frozeControls = false;

    }
}