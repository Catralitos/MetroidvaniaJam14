using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")] public float jumpForce;
    public float jumpTime;
    public float moveSpeed;

    [Header("Physics Checks")] public float checkRadius;
    public LayerMask whatIsGround;
    public Transform feetPos;

    private bool _isGrounded;
    private bool _isJumping;
    
    private float _jumpTimeCounter;
    private Rigidbody2D _rb;
    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        _isGrounded = Physics2D.OverlapCircle(feetPos.position, checkRadius, whatIsGround);
    }

    public void Move(float xInput, bool jump)
    {
        if (jump)
        {
            if (_isGrounded)
            {
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
        }
        else
        {
            _isJumping = false;
        }

        /*if (!jump && !holdJump)
        {
            //Debug.Log("IF 5");

            _isJumping = false;
        }*/

        _rb.velocity = new Vector2(moveSpeed * xInput, _rb.velocity.y);
    }
}
