using GameManagement;
using UnityEngine;
using Audio;

namespace Player
{
    public class PlayerHealth : MonoBehaviour
    {

        public float knockbackLength;
        public float knockbackHorizontalStrength;
        public float knockbackVerticalStrength;

        private Rigidbody2D _rb;
        private RigidbodyConstraints2D _initialCons;

        public int currentHealth;
        public int maxHealth;
        public int healthPerMaxIncrement;
    
        private DissolveEffect _dissolve;

        public float dissolveSpeed;
        [ColorUsageAttribute(true, true)] [SerializeField]
        private Color startDissolveColor;

        [ColorUsageAttribute(true, true)] [SerializeField]
        private Color stopDissolveColor;
        private AudioManager _audioManager;
        
        // Start is called before the first frame update
        private void Start()
        {
            _audioManager = GetComponent<AudioManager>();
            _rb = GetComponent<Rigidbody2D>();
            _dissolve = GetComponent<DissolveEffect>();
            _initialCons = _rb.constraints;
            currentHealth = maxHealth;
        }
    
        public void RecoverHealth(int healthToRecover)
        {
            currentHealth = Mathf.Clamp(currentHealth + healthToRecover, 0, maxHealth);

        }
    
        public void Hit(int damage)
        {
            if (PlayerEntity.Instance.dying) return;
            //fazer dano
            _audioManager.Play("Hit");
            PlayerEntity.Instance.animators[0].SetBool("BeingHit", true);
            PlayerEntity.Instance.frozeControls = true;
            _rb.velocity = Vector2.zero;
            int direction = PlayerEntity.Instance.facingRight ? -1 : 1;
            _rb.velocity = new Vector2(direction * knockbackHorizontalStrength, knockbackVerticalStrength);
            Invoke(nameof(RestoreControls), knockbackLength);
            currentHealth = Mathf.RoundToInt(Mathf.Clamp(currentHealth - damage, 0, maxHealth));
            if (currentHealth <= 0)
            {
                Die();
            }
        }

        public void Die()
        {
            _audioManager.Play("Death");
            PlayerEntity.Instance.DisableAllCollisions();
            PlayerEntity.Instance.dying = true;
            PlayerEntity.Instance.frozeControls = true;
            _rb.bodyType = RigidbodyType2D.Static;
            _dissolve.StartDissolve(dissolveSpeed, startDissolveColor);
            Invoke(nameof(ReloadLevel), 3f);
        }

        private void ReloadLevel()
        {
            _dissolve.StopDissolve(dissolveSpeed, stopDissolveColor);        
            GameManager.Instance.ReloadScene();
        }
    
        private void RestoreControls()
        {
            PlayerEntity.Instance.animators[0].SetBool("BeingHit", false);
            PlayerEntity.Instance.frozeControls = false;
        }

        public void IncreaseMaxHealth()
        {
            maxHealth += healthPerMaxIncrement;
            currentHealth += healthPerMaxIncrement;
        }
    }
}