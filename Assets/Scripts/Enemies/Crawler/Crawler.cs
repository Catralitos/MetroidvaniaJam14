using Enemies.Base;
using Player;
using UnityEngine;

namespace Enemies.Crawler
{
    public class Crawler : EnemyBase<Crawler>
    {
        public float moveSpeed;
        public float rayDistance = 0.1f;

        public bool movingRight = true;
        
        public ContactTrigger backTrigger;
        public Transform head;
        public Transform wallFinder;

        [HideInInspector] public bool lyingOnBack;
        [HideInInspector] public BoxCollider2D boxCollider;
        private void Awake()
        {
            backTrigger.StartedContactEvent += () => { lyingOnBack = true; };
            backTrigger.StoppedContactEvent += () => { lyingOnBack = false; };
        }

        protected override void Start()
        {
            base.Start();
            if (!started)
            {
                if (!movingRight) transform.localScale = transform.localScale * -1;Debug.DrawRay(transform.position, Vector3.left, Color.black, 1f);
                                                                                               Debug.DrawRay(transform.position, Vector3.right, Color.black, 1f);
                state = CrawlerCrawling.Create(this);
                boxCollider = GetComponent<BoxCollider2D>();
                started = true;
            }            
        }

        protected override void OnEnable()
        {
            backTrigger.enabled = true;
            base.OnEnable();
            if (started) state = CrawlerCrawling.Create(this);
        }

        protected override void OnDisable()
        {
            backTrigger.enabled = false;
            base.OnDisable();
        }

        public override void SetStunned()
        {
            SetState(CrawlerFalling.Create(this));
        }
    }
}