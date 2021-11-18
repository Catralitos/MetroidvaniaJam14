using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")] public float jumpForce;
    public float jumpTime;
    public float moveSpeed;
    public int numberOfMidairJumps;

    [Header("Physics Checks")] public float checkRadius;
    public LayerMask whatIsGround;
    public Transform feetPos;

    private bool _facingRight;
    private bool _isGrounded;
    private bool _isJumping;
    private bool _isSomersaulting;

    private float _jumpTimeCounter;
    private int _midairJumps;

    private Rigidbody2D _rb;

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _facingRight = true;
    }

    private void Update()
    {
        _isGrounded = Physics2D.OverlapCircle(feetPos.position, checkRadius, whatIsGround);
    }

    //This function is called every FixedUpdate on PlayerControls
    public void Move(float xInput, bool jump)
    {
        if (_facingRight && xInput < 0)
        {
            Flip();
        }
        
        if (jump)
        {
            if (_isGrounded)
            {
                if (Math.Abs(xInput) <= 0.1)
                {
                    _isSomersaulting = true;
                }

                _isJumping = true;
                _jumpTimeCounter = jumpTime;
                _rb.velocity = Vector2.up * jumpForce;
            }
            else if (_isJumping)
            {
                if (_jumpTimeCounter > 0)
                {
                    _rb.velocity = Vector2.up * jumpForce;
                    _jumpTimeCounter -= Time.deltaTime;
                }
                else
                {
                    _isJumping = false;
                }
            }
            else if (_midairJumps < numberOfMidairJumps)
            {
                _midairJumps++;
                if (Math.Abs(xInput) <= 0.1)
                {
                    _isSomersaulting = true;
                }

                _isJumping = true;
                _jumpTimeCounter = jumpTime;
                _rb.velocity = Vector2.up * jumpForce;
            }
        }
        else
        {
            _midairJumps = 0;
            _isSomersaulting = false;
            _isJumping = false;
        }
        
        _rb.velocity = new Vector2(moveSpeed * xInput, _rb.velocity.y);
    }

    private void Flip()
    {
        _facingRight = !_facingRight;
        Vector3 scaler = transform.localScale;
        scaler.x *= -1;
        transform.localScale = scaler;
    }
}