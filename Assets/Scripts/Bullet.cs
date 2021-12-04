using Extensions;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class Bullet : MonoBehaviour
{
    [SerializeField] protected LayerMask HitMask;
    [SerializeField] protected LayerMask DestroyMask;
    [SerializeField] protected float Speed;
    new protected Rigidbody2D rigidbody;
    new protected BoxCollider2D collider;
    new protected SpriteRenderer renderer;
    protected Vector2 direction;

    public virtual void Init(Vector2 dir)
    {
        this.direction = dir;
    }

    protected virtual void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        collider = GetComponent<BoxCollider2D>();
        renderer = GetComponent<SpriteRenderer>();
    }

    protected virtual void OnTriggerEnter2D(Collider2D collider)
    {
        if (HitMask.HasLayer(collider.gameObject.layer))
        {
            Hit(collider.gameObject);
        }
        if (DestroyMask.HasLayer(collider.gameObject.layer))
        {
            Destroy(gameObject);
        }
    }

    protected virtual void Hit(GameObject target) { }

    protected virtual void Update()
    {
        renderer.flipX = direction.x < 0;
        renderer.flipY = direction.y < 0;
    }

    protected virtual void FixedUpdate()
    {
        rigidbody.MovePosition(rigidbody.position + direction.normalized * Speed * Time.deltaTime);
    }
}