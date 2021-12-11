using UnityEngine;

public class Kickable : MonoBehaviour
{
    public float travelTime;
    public float velocityWhenKicked;

    public ContactTrigger landingTrigger;

    private float _travelTimeLeft;

    private int _direction;

    private Rigidbody2D _rb;
    private RigidbodyConstraints2D _initialCons;

    private void Awake()
    {
        landingTrigger.StartedContactEvent += () =>
        {
            /*if (_travelTimeLeft < 0)
            {
                _rb.constraints = _initialCons;
            }*/
        };
        landingTrigger.StoppedContactEvent += () => { };
    }

    private void OnEnable()
    {
        landingTrigger.enabled = true;
    }

    private void OnDisable()
    {
        landingTrigger.enabled = false;
    }

    // Start is called before the first frame update
    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _initialCons = _rb.constraints;
    }

    private void FixedUpdate()
    {
        _travelTimeLeft -= Time.deltaTime;
        if (_travelTimeLeft > 0)
        {
            _rb.velocity = new Vector2(_direction * velocityWhenKicked, _rb.velocity.y);
        }
        else
        {
            if (landingTrigger.isInContact)
            {
                _rb.constraints = _initialCons;
            }
        }
    }

    public void Kick()
    {
        if (_travelTimeLeft >= 0) return;
        _rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        if (PlayerEntity.Instance.facingRight)
        {
            _direction = 1;
        }
        else
        {
            _direction = -1;
        }

        _travelTimeLeft = travelTime;
    }
}