using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")] public float jumpForce;
    public float jumpTime;
    public float moveSpeed;
    public int numberOfMidairJumps;

    [Header("Physics Checks")] public float wallJumpWindow; 
    public LayerMask whatIsLedge;
    public ContactTrigger feetTrigger;
    public ContactTrigger leftTrigger;
    public ContactTrigger rightTrigger;

    private bool _canWallJump;
    private bool _facingRight;
    private bool _isGrounded;
    private bool _isHuggingWallLeft;
    private bool _isHuggingWallRight;
    private bool _isJumping;
    private bool _isSomersaulting;

    private float _jumpTimeCounter;
    private int _midairJumps;

    private Rigidbody2D _rb;

    private void Awake()
    {
        feetTrigger.StartedContactEvent += () => { _isGrounded = true; };
        feetTrigger.StoppedContactEvent += () => { _isGrounded = false; };
        leftTrigger.StartedContactEvent += () => { _isHuggingWallLeft = true; };
        leftTrigger.StoppedContactEvent += () => { _isHuggingWallLeft = false; };
        rightTrigger.StartedContactEvent += () => { _isHuggingWallRight = true; };
        rightTrigger.StoppedContactEvent += () => { _isHuggingWallRight = false; };
    }

    private void OnEnable()
    {
        feetTrigger.enabled = true;
        leftTrigger.enabled = true;
        rightTrigger.enabled = true;
    }

    private void OnDisable()
    {
        feetTrigger.enabled = false;
        leftTrigger.enabled = false;
        rightTrigger.enabled = false;
    }


    // Start is called before the first frame update
    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _facingRight = true;
    }

    private void Update()
    {

    }

    //This function is called every FixedUpdate on PlayerControls
    public void Move(float xInput, bool jump)
    {
        if ((_facingRight && xInput < 0) || (!_facingRight && xInput > 0))
        {
            Flip();
        }

       

        if (_isGrounded || (_canWallJump &&
                            ((_facingRight && _isHuggingWallLeft) || (!_facingRight && _isHuggingWallRight))))
        {
            PlayerEntity.Instance.testCylinder.color = Color.red;
        }
        else
        {
            PlayerEntity.Instance.testCylinder.color = Color.white;
        }
        
        if (_isSomersaulting && ((_facingRight && _isHuggingWallRight) || (!_facingRight && _isHuggingWallLeft)))
        {
            _canWallJump = true;
        }
        else
        {
            if (_canWallJump) Invoke(nameof(StopWallJump), wallJumpWindow);
        }


        if (jump)
        {
            if (_isGrounded || (_canWallJump &&
                                ((_facingRight && _isHuggingWallLeft) || (!_facingRight && _isHuggingWallRight))))
            {
                if (Math.Abs(xInput) >= 0.1)
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
                    _isSomersaulting = false;
                }
            }
            else if (PlayerEntity.Instance.unlockedDoubleJump && _midairJumps < numberOfMidairJumps)
            {
                _midairJumps++;
                if (Math.Abs(xInput) >= 0.1)
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

        if (_rb.gravityScale > 0) _rb.velocity = new Vector2(moveSpeed * xInput, _rb.velocity.y);
    }

    private void Flip()
    {
        _facingRight = !_facingRight;
        Vector3 scaler = transform.localScale;
        scaler.x *= -1;
        transform.localScale = scaler;
    }

    private void StopWallJump()
    {
        _canWallJump = false;
    }
    
}