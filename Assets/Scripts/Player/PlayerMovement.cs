using System;
using System.Data;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Run and Jump")] public float jumpForce;
    public float jumpTime;
    public float moveSpeed;
    public int numberOfMidairJumps;

    [Header("Dash")] public float dashCooldown;
    public float dashSpeed;
    public float startDashTime;

    [Header("Physics Checks (Triggers)")] public float wallJumpWindow;
    public ContactTrigger feetTrigger;
    public ContactTrigger leftTrigger;
    public ContactTrigger rightTrigger;

    [Header("Physics Checks (Raycasts)")] public float detectionRange;
    public float ledgeOffsetX1;
    public float ledgeOffestY1;
    public float ledgeOffsetX2;
    public float ledgeOffestY2;
    public LayerMask whatIsLedge;
    public Transform wallRayOrigin;
    public Transform bottomLedgeRayOrigin;
    public Transform topLedgeRayOrigin;

    private bool _canClimbLedge;
    private bool _canClimbLedgeMorph;
    private bool _canWallJump;
    private bool _dashingLeft;
    private bool _dashingRight;
    private bool _detectedWall;
    private bool _detectedLedgeBottom;
    private bool _detectedLedgeTop;
    private bool _facingRight;
    private bool _isGrounded;
    private bool _isHuggingWallLeft;
    private bool _isHuggingWallRight;
    private bool _isJumping;
    private bool _isSomersaulting;
    private bool _ledgeDetected;
    private bool _ledgeDetectedMorph;

    private float _dashCooldownLeft;
    private float _dashTime;
    private float _jumpTimeCounter;

    private int _jumpPresses;
    private int _midairJumps;

    private Rigidbody2D _rb;
    private Vector2 _ledgePosBottom;
    private Vector2 _ledgePos1;
    private Vector2 _ledgePos2;

    private void Awake()
    {
        feetTrigger.StartedContactEvent += () =>
        {
            _isGrounded = true;
            _midairJumps = 0;
            _isJumping = false;
            _isSomersaulting = false;
        };
        feetTrigger.StoppedContactEvent += () => { _isGrounded = false; };
        leftTrigger.StartedContactEvent += () =>
        {
            _isHuggingWallLeft = true;
            _dashingLeft = false;
            _dashingRight = false;
            _rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        };
        leftTrigger.StoppedContactEvent += () => { _isHuggingWallLeft = false; };
        rightTrigger.StartedContactEvent += () =>
        {
            _isHuggingWallRight = true;
            _dashingLeft = false;
            _dashingRight = false;
            _rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        };
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
        _rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        _dashCooldownLeft = 0;
        _dashTime = startDashTime;
        _facingRight = true;
        _midairJumps = numberOfMidairJumps;
    }

    private void Update()
    {
        CheckSurroundings();
        CheckLedgeClimb();

        _dashCooldownLeft -= Time.deltaTime;

        if (_dashingLeft)
        {
            PlayerEntity.Instance.testCylinder.color = Color.blue;
        }
        else if (_dashingRight)
        {
            PlayerEntity.Instance.testCylinder.color = Color.magenta;
        }
        else
        {
            PlayerEntity.Instance.testCylinder.color = Color.white;
        }
    }

    //This function is called every FixedUpdate on PlayerControls
    public void Move(float xInput, bool jump, bool dash)
    {
        if (HasToFlip(xInput))
        {
            Flip();
        }

        if (Dashing() && _dashTime < 0)
        {
            Debug.Log("Dash time over");
            _dashingRight = false;
            _dashingLeft = false;
            _dashTime = startDashTime;
            _rb.constraints = RigidbodyConstraints2D.FreezeRotation;
            _dashCooldownLeft = dashCooldown;
        }

        if (CanJump())
        {
            _midairJumps = numberOfMidairJumps;
            _jumpPresses = 0;
        }

        if (!HangingToLedge())
        {
            if (_isSomersaulting && ((_facingRight && _isHuggingWallRight) || (!_facingRight && _isHuggingWallLeft)))
            {
                _canWallJump = true;
            }
            else
            {
                if (_canWallJump) Invoke(nameof(StopWallJump), wallJumpWindow);
            }
        }

        if (dash && _dashCooldownLeft < 0)
        {
            if (Math.Abs(xInput) <= 0.1f)
            {
                //do nothing
            }
            else if (xInput > 0)
            {
                _dashingRight = true;
                _rb.constraints = RigidbodyConstraints2D.FreezeRotation | RigidbodyConstraints2D.FreezePositionY;
            }
            else if (xInput < 0)
            {
                _dashingLeft = true;
                _rb.constraints = RigidbodyConstraints2D.FreezeRotation | RigidbodyConstraints2D.FreezePositionY;
            }
        }
        
        if (_dashingRight)
        {
            _dashTime -= Time.deltaTime;
            _rb.velocity = Vector2.right * dashSpeed;
        }
        else if (_dashingLeft)
        {
            _dashTime -= Time.deltaTime;
            _rb.velocity = Vector2.left * dashSpeed;
        }

        if (jump && !Dashing())
        {
            if (HangingToLedge())
            {
                if ((_facingRight && xInput < 0) || (!_facingRight && xInput > 0))
                {
                    _canClimbLedge = false;
                    _canClimbLedgeMorph = false;
                    _ledgeDetected = false;
                    _ledgeDetectedMorph = false;
                    _isSomersaulting = true;
                    _isJumping = true;
                    _jumpTimeCounter = jumpTime;
                    _rb.velocity = Vector2.up * jumpForce;
                }
                else if ((_facingRight && xInput > 0) || (!_facingRight && xInput < 0))
                {
                    FinishLedgeClimb();
                }
                else
                {
                    _canClimbLedge = false;
                    _canClimbLedgeMorph = false;
                    _ledgeDetected = false;
                    _ledgeDetectedMorph = false;
                    _isSomersaulting = false;
                    _isJumping = true;
                    _jumpTimeCounter = jumpTime;
                    _rb.velocity = Vector2.up * jumpForce;
                }
            }

            //salto no chão/parede
            if (CanJump())
            {
                if (Math.Abs(xInput) >= 0.1)
                {
                    _isSomersaulting = true;
                }

                _isJumping = true;
                _jumpTimeCounter = jumpTime;
                _rb.velocity = Vector2.up * jumpForce;
            }
            //salto no ar
            else if (_midairJumps > 0 && _jumpPresses > 0)
            {
                if (Math.Abs(xInput) >= 0.1)
                {
                    _isSomersaulting = true;
                }

                _midairJumps--;
                _isJumping = true;
                _jumpTimeCounter = jumpTime;
                _rb.velocity = Vector2.up * jumpForce;
            }
            //fazer o salto mais alto conforme o input
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
        }
        else
        {
            _isJumping = false;
            _jumpPresses++;
        }

        if (!Dashing() && !HangingToLedge())
            _rb.velocity = new Vector2(moveSpeed * xInput, _rb.velocity.y);
    }

    public void FinishLedgeClimb()
    {
        _canClimbLedge = false;
        _canClimbLedgeMorph = false;
        transform.position = _ledgePos2;
        _ledgeDetected = false;
        _ledgeDetectedMorph = false;
    }

    private bool HasToFlip(float xInput)
    {
        return !_canClimbLedgeMorph && !_canClimbLedge && ((_facingRight && (xInput < 0 || _dashingLeft)
                                                            || (!_facingRight && (xInput > 0 || _dashingRight))));
    }

    private void Flip()
    {
        _facingRight = !_facingRight;
        Vector3 scaler = transform.localScale;
        scaler.x *= -1;
        transform.localScale = scaler;
    }

    private bool CanJump()
    {
        return _isGrounded || ReadyToWallJump();
    }

    private bool ReadyToWallJump()
    {
        return (_canWallJump && ((_facingRight && _isHuggingWallLeft) || (!_facingRight && _isHuggingWallRight)));
    }

    private bool Dashing()
    {
        return _dashingLeft || _dashingRight;
    }

    private bool HangingToLedge()
    {
        return _canClimbLedge || _canClimbLedgeMorph;
    }

    private void CheckSurroundings()
    {
        _detectedWall = Physics2D.Raycast(wallRayOrigin.position, transform.right, detectionRange, whatIsLedge);
        _detectedLedgeBottom =
            Physics2D.Raycast(bottomLedgeRayOrigin.position, transform.right, detectionRange, whatIsLedge);
        _detectedLedgeTop = Physics2D.Raycast(topLedgeRayOrigin.position, transform.right, detectionRange, whatIsLedge);

        if (_detectedWall && !_detectedLedgeBottom && !_detectedLedgeTop && !_ledgeDetected)
        {
            _ledgeDetected = true;
            _ledgePosBottom = wallRayOrigin.position;
        }
        else if (_detectedWall && !_detectedLedgeBottom && _detectedLedgeTop && !_ledgeDetectedMorph)
        {
            _ledgeDetectedMorph = true;
            _ledgePosBottom = wallRayOrigin.position;
        }
    }

    private void CheckLedgeClimb()
    {
        if (_ledgeDetected && !_canClimbLedge)
        {
            _canClimbLedge = true;

            if (_facingRight)
            {
                _ledgePos1 = new Vector2(Mathf.Floor(_ledgePosBottom.x + detectionRange) - ledgeOffsetX1,
                    Mathf.Floor(_ledgePosBottom.y + detectionRange) + ledgeOffestY1);
                _ledgePos2 = new Vector2(Mathf.Floor(_ledgePosBottom.x + detectionRange) + ledgeOffsetX2,
                    Mathf.Floor(_ledgePosBottom.y + detectionRange) + ledgeOffestY2);
            }
            else
            {
                _ledgePos1 = new Vector2(Mathf.Floor(_ledgePosBottom.x - detectionRange) + ledgeOffsetX1,
                    Mathf.Floor(_ledgePosBottom.y + detectionRange) + ledgeOffestY1);
                _ledgePos2 = new Vector2(Mathf.Floor(_ledgePosBottom.x - detectionRange) - ledgeOffsetX2,
                    Mathf.Floor(_ledgePosBottom.y + detectionRange) + ledgeOffestY2);
            }
        }

        if (_ledgeDetectedMorph && !_canClimbLedgeMorph)
        {
            _canClimbLedgeMorph = true;

            if (_facingRight)
            {
                _ledgePos1 = new Vector2(Mathf.Floor(_ledgePosBottom.x + detectionRange) - ledgeOffsetX1,
                    Mathf.Floor(_ledgePosBottom.y + detectionRange) + ledgeOffestY1);
                _ledgePos2 = new Vector2(Mathf.Floor(_ledgePosBottom.x + detectionRange) + ledgeOffsetX2,
                    Mathf.Floor(_ledgePosBottom.y + detectionRange) + ledgeOffestY2);
            }
            else
            {
                _ledgePos1 = new Vector2(Mathf.Floor(_ledgePosBottom.x - detectionRange) + ledgeOffsetX1,
                    Mathf.Floor(_ledgePosBottom.y + detectionRange) + ledgeOffestY1);
                _ledgePos2 = new Vector2(Mathf.Floor(_ledgePosBottom.x - detectionRange) - ledgeOffsetX2,
                    Mathf.Floor(_ledgePosBottom.y + detectionRange) + ledgeOffestY2);
            }
        }

        if (_canClimbLedge || _canClimbLedgeMorph)
        {
            transform.position = _ledgePos1;
        }
    }

    private void StopWallJump()
    {
        _canWallJump = false;
    }
}