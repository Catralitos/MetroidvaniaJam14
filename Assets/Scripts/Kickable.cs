using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kickable : MonoBehaviour
{
    public float travelTime;
    public float velocityWhenKicked;

    public ContactTrigger feetTrigger;

    private float _travelTimeLeft;

    private int _direction;

    private Rigidbody2D _rb;
    private RigidbodyConstraints2D _initialCons;

    private void Awake()
    {
        feetTrigger.StartedContactEvent += () =>
        {
            if (_travelTimeLeft < 0)
            {
                _rb.constraints = _initialCons;
            }
        };
        feetTrigger.StoppedContactEvent += () => { };
    }

    private void OnEnable()
    {
        feetTrigger.enabled = true;
    }

    private void OnDisable()
    {
        feetTrigger.enabled = false;
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
        if (_travelTimeLeft >= 0)
        {
            _rb.velocity = new Vector2(_direction * velocityWhenKicked, _rb.velocity.y);
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