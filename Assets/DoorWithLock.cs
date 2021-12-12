using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorWithLock : MonoBehaviour
{
    /*
    public bool unlocked = false;
    public bool closed;
    public bool startClosed = true;
    public float closeSpeed;
    public LayerMask blockMask;
    private Vector3 startPosition;
    private BoxCollider2D _collider;
    
    void Start()
    {
        startPosition = transform.position;

        _collider = GetComponent<BoxCollider2D>();

        if (!startClosed)
        {
            closed = false;
            transform.position += Vector3.up * 2;
            _collider.enabled = false;
        }
        else
        {
            closed = true;
            _collider.enabled = true;
        }
    }

    public void Update()
    {
        if (!closed)
        {
            if (Physics2D.OverlapCircle(startPosition , 1, blockMask))
            {
                return;
            }
            _collider.enabled = true;
            closed= false;  
        }
        else
        {
            if (unlocked && o player tiver a colidir)
            {
                closed = true;

            }
            _collider.enabled = true;
            
        }
        
        
        
    }
    
    private void FixedUpdate()
    {
        if (closed)
        {
            transform.position = Vector3.MoveTowards(transform.position, startPosition + Vector3.up * 2, closeSpeed);
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, startPosition, closeSpeed);
        }
    }
    
    
    
    public void OpenDoor()
    {
        closed = false;
    }

    public void CloseDoor()
    {
        closed = true;
    }

    public void UnlockDoor()
    {
        unlocked = true;
    }
    */
}
