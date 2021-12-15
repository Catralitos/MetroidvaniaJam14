using System;
using Enemies.Base;
using Extensions;
using Player;
using UnityEngine;

namespace Enemies.Swordfighter
{
    public class Swordfighter : EnemyBase<Swordfighter>
    {
        [Header("Movement")] public float moveSpeed;
        public float movementSmoothing = 0.05f;

        [Header("Detection")] public float sightDistance;

        [Header("Patrol")] public float holdPositionTime;
        public float horizontalRange;

        [Header("Attacks")] public Transform attackPoint;
        public int attackDamage;
        public float attackRange;
        public float attackCooldown;

        public bool facingRight = true;
        private bool _touchingGroundLeft;
        private bool _touchingGroundRight;
        private bool _touchingLeftWall;
        private bool _touchingRightWall;

        public Sprite regular;
        public Sprite attacking;

        [HideInInspector] public SpriteRenderer spriteRenderer;
        
        [Header("Collision")] public ContactTrigger leftGroundTrigger;
        public ContactTrigger rightGroundTrigger;
        public ContactTrigger leftWallTrigger;
        public ContactTrigger rightWallTrigger;

        [HideInInspector] public Vector2 currentPatrolAnchor;
        [HideInInspector] public Vector2 velocity;

        [HideInInspector] public bool colisionDetected;
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
                spriteRenderer = GetComponent<SpriteRenderer>();
                rb = GetComponent<Rigidbody2D>();
                state = SwordfighterPatrol.Create(this);
                currentPatrolAnchor = transform.position;
                started = true;
                _touchingGroundLeft = leftGroundTrigger.isInContact;
                _touchingGroundRight = rightGroundTrigger.isInContact;
                _touchingRightWall = rightWallTrigger.isInContact;
                _touchingLeftWall = leftWallTrigger.isInContact;
                colisionDetected = false;
            }
        }

        protected override void Update()
        {
            base.Update();
            if (!colisionDetected)
            {
                if (TouchingGround()) colisionDetected = true;
            }
        }

        protected override void OnEnable()
        {
            leftGroundTrigger.enabled = true;
            rightGroundTrigger.enabled = true;
            leftWallTrigger.enabled = true;
            rightWallTrigger.enabled = true;
            base.OnEnable();
            if (started)
            {
                state = SwordfighterPatrol.Create(this);
                currentPatrolAnchor = transform.position;
            }
        }

        protected override void OnDisable()
        {
            leftGroundTrigger.enabled = false;
            rightGroundTrigger.enabled = false;
            leftWallTrigger.enabled = false;
            rightWallTrigger.enabled = false;
            base.OnDisable();
        }

        public bool CheckForPlayer()
        {
            //se deteta o player persegue-o
            //primeiro ve se está em range horizontal
            float distance = PlayerEntity.Instance.transform.position.x - transform.position.x;


            if (facingRight && distance < 0)
            {
                return false;
            }

            if (!facingRight && distance > 0)
            {
                return false;
            }


            if (Math.Abs(distance) <= horizontalRange)
            {
                //depois ve a posiçao do player e manda raycast (nao queremos um raycast horizontal por causa de slopes
                Vector3 direction = PlayerEntity.Instance.transform.position - transform.position;
                //TODO ver se isto das layermasks assim não fode (quero que ele detete só para o player e o chão
                //e ignore os outros inimigos)
                RaycastHit2D forward =
                    Physics2D.Raycast(transform.position, direction, sightDistance,
                        groundMask + playerMask);

                if (forward.collider == null) return false;
                //se bater no player persegue-o
                return playerMask.HasLayer(forward.collider.gameObject.layer);
            }

            return false;
        }

        public void MoveInDirection(Vector3 point)
        {
            float move = point.x > 0 ? 1 : Math.Abs(point.x - 0) <= 0.01 ? 0 : -1;
            move *= moveSpeed * Time.deltaTime;
            if (Math.Abs(move - 0) <= 0.01)
            {
                rb.velocity = Vector2.zero;
                return;
            }

            // Move the character by finding the target _velocity
            Vector3 targetVelocity = new Vector2(move * 10f, rb.velocity.y);
            // And then smoothing it out and applying it to the character
            rb.velocity = Vector2.SmoothDamp(rb.velocity, targetVelocity, ref velocity,
                movementSmoothing);
        }

        public void Flip()
        {
            // Switch the way the player is labelled as facing.
            facingRight = !facingRight;

            // Multiply the player's x local scale by -1.
            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
        }

        public bool InAttackRange()
        {
            GameObject player = PlayerEntity.Instance.gameObject;
            Vector3 pos = player.transform.position;
            return Math.Abs(pos.x - transform.position.x) <= attackRange &&
                   ((facingRight && pos.x > transform.position.x) ||
                    (!facingRight && pos.x < transform.position.x)) &&
                   Math.Abs(pos.y - transform.position.y) <= 1;
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
}