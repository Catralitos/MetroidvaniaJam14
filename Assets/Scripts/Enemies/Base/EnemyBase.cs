using System;
using System.Collections.Generic;
using Extensions;
using Player;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Enemies.Base
{
    public abstract class EnemyBase : MonoBehaviour
    {
        public bool isAlive
        {
            get => currentHealth > 0;
        }

        public virtual bool IsAlive
        {
            get => currentHealth > 0;
        }

        public LayerMask groundMask;
        public LayerMask playerMask;

        //public LayerMask playerAttacks;

        public int currentHealth;
        public int maxHealth;

        public int contactDamage;

        protected bool started = false;

        [HideInInspector] public Vector2 startPosition;
        [HideInInspector] public Rigidbody2D rb;
        [HideInInspector] public Quaternion startRotation;

        public float randomDropChance = 0.5f;
        public List<GameObject> pickUps;

        public GameObject explosionPrefab;

        private float _initialMass;
        private RigidbodyConstraints2D _initialConstraints;

        protected virtual void Start()
        {
            startPosition = transform.position;
            startRotation = transform.rotation;
            rb = GetComponent<Rigidbody2D>();
            _initialMass = rb.mass;
            _initialConstraints = rb.constraints;
        }

        protected virtual void OnEnable()
        {
            currentHealth = maxHealth;
            if (started)
            {
                transform.position = startPosition;
                transform.rotation = startRotation;
            }
        }

        public void Call(string messageName)
        {
            SendMessage(messageName);
        }

        public virtual void Hit(int damage)
        {
            Debug.Log("Entrou no hit");
            if (!IsAlive) return;
            currentHealth = Mathf.Max(currentHealth - damage, 0);
            if (!IsAlive) Die();
        }

        protected virtual void Die()
        {
            var spawnPos = transform.position;
            if (explosionPrefab != null) Instantiate(explosionPrefab, spawnPos, transform.rotation);
            if (pickUps.Count > 0)
            {
                if (Random.Range(0.0f, 1.0f) <= randomDropChance)
                {
                    Instantiate(pickUps[Random.Range(0, pickUps.Count)], spawnPos, Quaternion.identity);
                }
            }

            gameObject.SetActive(false);
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (playerMask.HasLayer(other.gameObject.layer))
            {
                PlayerEntity.Instance.Health.Hit(contactDamage);
                rb.velocity = Vector2.zero;
                rb.mass = 100000000000;
                rb.constraints = RigidbodyConstraints2D.FreezeAll;
            }
        }

        private void OnCollisionExit2D(Collision2D other)
        {
            if (playerMask.HasLayer(other.gameObject.layer))
            {
                rb.constraints = _initialConstraints;
                rb.mass = _initialMass;
            }
        }
    }

    public abstract class EnemyBase<EnemyType> : EnemyBase where EnemyType : EnemyBase<EnemyType>
    {
        protected EnemyState<EnemyType> state;

        public void SetState(EnemyState<EnemyType> state)
        {
            Destroy(this.state);
            this.state = state;
        }

        public virtual void SetStunned()
        {
            //Do nothing, each enemy will need a different one
        }

        public override void Hit(int damage)
        {
            if (isAlive)
            {
                currentHealth = Mathf.Max(currentHealth - damage, 0);
                state.OnGetHit();
                if (!IsAlive) Die();
            }
        }

        protected virtual void Update()
        {
            if (IsAlive)
            {
                if (!state.Initialized)
                {
                    state.StateStart();
                }

                state.StateUpdate();
            }
        }

        protected virtual void FixedUpdate()
        {
            if (IsAlive)
            {
                if (!state.Initialized)
                {
                    state.StateStart();
                }

                state.StateFixedUpdate();
            }
        }

        protected virtual void OnDisable()
        {
            Destroy(state);
        }
    }
}