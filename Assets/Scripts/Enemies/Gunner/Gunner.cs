using System;
using UnityEngine;

public class Gunner : EnemyBase<Gunner>
{
    public int numberOfShots;
    public float fireRate;

    public float moveSpeed;
    public float sightDistance;
    public float movementSmoothing = 0.05f;
    public float holdPositionTime;
    public float horizontalRange;
    public float horizontalAttackRange;
    public float verticalAttackRange;
    public float attackCooldown;

    public bool facingRight = true;
    private bool _touchingGroundLeft;
    private bool _touchingGroundRight;
    private bool _touchingLeftWall;
    private bool _touchingRightWall;
    
    public GameObject bulletPrefab;

    [HideInInspector] public SpriteRenderer capsuleSprite;

    public Transform bulletSpawn;

    public ContactTrigger leftGroundTrigger;
    public ContactTrigger rightGroundTrigger;
    public ContactTrigger leftWallTrigger;
    public ContactTrigger rightWallTrigger;
    
    [HideInInspector] public Vector2 currentPatrolAnchor;
    [HideInInspector] public Vector2 velocity;

    private void Awake()
    {
        leftGroundTrigger.StartedContactEvent += () => { _touchingGroundLeft = true; };
        leftGroundTrigger.StoppedContactEvent += () => { _touchingGroundLeft = false; };
        rightGroundTrigger.StartedContactEvent += () => { _touchingGroundRight = true; };
        rightGroundTrigger.StoppedContactEvent += () => { _touchingGroundRight = false; };
        leftWallTrigger.StartedContactEvent += () => { _touchingLeftWall = true; };
        leftWallTrigger.StoppedContactEvent += () => { _touchingLeftWall = false; };
        rightWallTrigger.StartedContactEvent += () => { _touchingRightWall = true; };
        rightWallTrigger.StoppedContactEvent += () => { _touchingRightWall = false; };
    }

    protected override void Start()
    {
        base.Start();
        if (!started)
        {
            state = GunnerIdle.Create(this);
            capsuleSprite = GetComponent<SpriteRenderer>();
            currentPatrolAnchor = transform.position;
            started = true;
            _touchingGroundLeft = leftGroundTrigger.isInContact;
            _touchingGroundRight = rightGroundTrigger.isInContact;
            _touchingRightWall = rightWallTrigger.isInContact;
            _touchingLeftWall = leftWallTrigger.isInContact;
        }
    }

    protected override void OnEnable()
    {
        rightGroundTrigger.enabled = true;
        rightWallTrigger.enabled = true;
        base.OnEnable();
        if (started)
        {
            state = GunnerIdle.Create(this);
            currentPatrolAnchor = transform.position;
        }
    }

    protected override void OnDisable()
    {
        rightGroundTrigger.enabled = false;
        rightWallTrigger.enabled = false;
        base.OnDisable();
    }

    public bool CheckIfFlip()
    {
        GameObject player = PlayerEntity.Instance.gameObject;
        if (facingRight && player.transform.position.x < transform.position.x) return true;
        if (!facingRight && player.transform.position.x > transform.position.x) return true;
        return false;
    }

    public bool InAttackRange()
    {
        Vector3 player = PlayerEntity.Instance.gameObject.transform.position;
        Vector3 pos = transform.position;
        return Math.Abs(player.x - pos.x) <= horizontalAttackRange &&
               ((facingRight && player.x > pos.x) ||
                (!facingRight && player.x < pos.x)) &&
               Math.Abs(player.y - pos.y) <= verticalAttackRange;
    }

    public bool TouchingWall()
    {
        return (_touchingLeftWall && !facingRight) || (_touchingRightWall && facingRight);
    }
    
    public bool TouchingGround()
    {
        return (_touchingGroundLeft && !facingRight) || (_touchingGroundRight && facingRight);
    }
}