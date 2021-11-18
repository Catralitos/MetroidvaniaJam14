using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
/*
Can detect when a capsule or box around a gameObject begins an interception with a layer(e.g. floor or wall)
Good for detection jumping on top of enemies, landings and wall slides.

Allows for coyote time as well. And can run on edit mode to set the capsule/box parameters.

Free to use

Usage tip:
    	-capsule cast mode is untested.
        -if using coyotee time for jumps, make sure to put a timer to not allow 2 jumps within a certain time. This is to avoid that a player begins a jump, and then uses the coyotee time to get a second jump in.

script by MiguelSeabra
https://miguelseabra1999.itch.io/
*/
public enum CastType
{
    box,
    capsule
}
public class ContactTrigger : MonoBehaviour
{
    public event UnityAction StartedContactEvent;
    public event UnityAction StoppedContactEvent;
    [Header("General")]
    [Tooltip("Determines what layers to check for")]
    public LayerMask contactLayers;
    [Tooltip("How long after a change in contact happens should the event be triggered")]
    public float coyoteTime;
    [Tooltip("Wont work for capsule mode")]
    [SerializeField]private bool shouldDrawGizmo = true;
    [Header("Cast Shape")]
    public CastType castType;
    public Vector2 castDimensions;
    public Vector2 positionalOffset;
    [Tooltip("for capsules only")]
    public CapsuleDirection2D capsuleDirection2D;
    [Header("Current State")]
    public bool isInContact; //is true even if there is no contact but there is coyote time
    private bool _isReallyInContact;//is only true if actually in contact
    private bool _isInCoyoteTime = false;

    private Coroutine _coyoteTimeRoutine;

    private void Awake() {
        isInContact = false;
    }
    private void FixedUpdate()
    {
        bool wasInContact = _isReallyInContact;
        _isReallyInContact = CheckContact();

        if(wasInContact != _isReallyInContact)
        {
            if(_isReallyInContact)
            {
                SafeRaiseEvent(StartedContactEvent);
                AbortCoyoteTime();
            }
            else
            {
                if(coyoteTime <= 0)
                    SafeRaiseEvent(StoppedContactEvent);
                else if(!_isInCoyoteTime)
                    StartCoyoteTime();
            }
        }

        isInContact = _isReallyInContact || _isInCoyoteTime;
    }

    //Draw the BoxCast as a gizmo to show where it currently is testing. Click the Gizmos button to see this
    void OnDrawGizmos()
    {
        if(!shouldDrawGizmo || castType == CastType.capsule)
            return;

        if(isInContact)
            Gizmos.color = Color.red;
        else
            Gizmos.color = Color.green;
     
        Gizmos.DrawWireCube(transform.position + new Vector3(positionalOffset.x, positionalOffset.y,0), new Vector3(castDimensions.x,castDimensions.y,1));
     
    }
    private bool CheckContact()
    {
        if(castType == CastType.box)
            return CheckContactBox();
        else    
            return CheckContactCapsule();
    }

    private bool CheckContactCapsule()
    {
        Vector2 position = new Vector2(transform.position.x, transform.position.y) + positionalOffset;
        return Physics2D.OverlapCapsule(position, castDimensions,capsuleDirection2D, contactLayers);
    }
    private bool CheckContactBox()
    {
        Vector2 position = new Vector2(transform.position.x, transform.position.y) + positionalOffset;
        List<Collider2D> results = new List<Collider2D>();
        return Physics2D.OverlapBox(position, castDimensions,0f, contactLayers);
      
    }

    private void SafeRaiseEvent( UnityAction myEvent)
    {
        if(myEvent!=null)
            myEvent.Invoke();
    }

    private void StartCoyoteTime()
    {
        AbortCoyoteTime();
        _isInCoyoteTime = true;
        _coyoteTimeRoutine = StartCoroutine(CoyoteTimeRoutine());
    }
    private void AbortCoyoteTime()
    {
        _isInCoyoteTime = false;
        if(_coyoteTimeRoutine != null)
            StopCoroutine(_coyoteTimeRoutine);
    }

    private IEnumerator CoyoteTimeRoutine()
    {
        yield return new WaitForSeconds(coyoteTime);
        AbortCoyoteTime();
        SafeRaiseEvent(StoppedContactEvent);
    }


}
