using Audio;
using Player;
using UnityEngine;

namespace Enemies.Gunner
{
    public class GunnerBullet : Bullet
    {
        private AudioManager _audioManager;
        public int bulletDamage = 60;

        private Rigidbody2D _rb;
        private CircleCollider2D _collider;

        private DissolveEffect _dissolve;

        public float dissolveSpeed;

        [ColorUsageAttribute(true, true)] [SerializeField]
        private Color startDissolveColor;

        [ColorUsageAttribute(true, true)] [SerializeField]
        private Color stopDissolveColor;

        public void Start()
        {
            //_audioManager = GetComponent<AudioManager>();
            //_audioManager.Play("Spawn");
            _rb = GetComponent<Rigidbody2D>();
            _collider = GetComponent<CircleCollider2D>();
            _dissolve = GetComponent<DissolveEffect>();
        }

        protected override void Hit(GameObject target)
        {
            base.Hit(target);
            PlayerEntity.Instance.Health.Hit(bulletDamage);
            _rb.bodyType = RigidbodyType2D.Static;
            _dissolve.StartDissolve(dissolveSpeed, startDissolveColor);
            _collider.enabled = false;
            Invoke(nameof(DestroyBullet), 3f);
        }

        private void DestroyBullet()
        {
            _dissolve.StopDissolve(dissolveSpeed, stopDissolveColor);
            Destroy(gameObject);
        }
    }
}