using UnityEngine;

public class Crawler : EnemyBase<Crawler>
{
    public float moveSpeed;
    public float rayDistance = 0.1f;

    public ContactTrigger backTrigger;
    public LayerMask groundMask;
    public Transform head;
    public Transform wallFinder;

    [HideInInspector] public bool lyingOnBack;
    [HideInInspector] public BoxCollider2D boxCollider;
    [HideInInspector] public Rigidbody2D rb;

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
            state = CrawlerCrawling.Create(this);
            boxCollider = GetComponent<BoxCollider2D>();
            rb = GetComponent<Rigidbody2D>();
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
}