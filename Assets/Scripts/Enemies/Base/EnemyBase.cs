using System;
using System.Collections.Generic;
using Extensions;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

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

    //public LayerMask playerAttacks;

    public int currentHealth;
    public int maxHealth;

    public int contactDamage;

    protected bool started = false;

    [HideInInspector] public Vector2 startPosition;
    [HideInInspector] public Quaternion startRotation;

    public float randomDropChance = 0.5f;
    public List<GameObject> pickUps;

    public GameObject explosionPrefab;

    protected virtual void Start()
    {
        startPosition = transform.position;
        startRotation = transform.rotation;
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
        if (!IsAlive) return;
        currentHealth = Mathf.Max(currentHealth - damage, 0);
        if (!IsAlive) Die();
    }

    protected virtual void Die()
    {
        var spawnPos = transform.position;
        if (explosionPrefab != null) Instantiate(explosionPrefab, spawnPos, transform.rotation);
        if (Random.Range(0.0f, 1.0f) <= randomDropChance)
        {
            Instantiate(pickUps[Random.Range(0, pickUps.Count)], spawnPos, Quaternion.identity);
        }

        gameObject.SetActive(false);
    }
}

public abstract class EnemyBase<EnemyType> : EnemyBase where EnemyType : EnemyBase<EnemyType>
{
    protected EnemyState<EnemyType> state;

    public void SetState(EnemyState<EnemyType> state)
    {
        this.state = state;
    }

    public override void Hit(int damage)
    {
        if (isAlive)
        {
            if (!IsAlive) return;
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