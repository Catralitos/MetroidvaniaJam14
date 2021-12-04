using System;
using Extensions;
using UnityEngine;

public class GunnerState : EnemyState<Gunner>
{
    protected Animator animator;

    protected static new T Create<T>(Gunner target) where T : GunnerState
    {
        var state = EnemyState<Gunner>.Create<T>(target);
        state.animator = target.GetComponentInChildren<Animator>();
        return state;
    }

    protected bool CheckForPlayer()
    {
        //se deteta o player persegue-o
        //primeiro ve se está em range horizontal
        float distance = PlayerEntity.Instance.transform.position.x - transform.position.x;

        if (target.facingRight && distance < 0)
        {
            return false;
        }

        if (!target.facingRight && distance > 0)
        {
            return false;
        }


        if (Math.Abs(distance) <= target.horizontalRange)
        {
            //depois ve a posiçao do player e manda raycast (nao queremos um raycast horizontal por causa de slopes
            Vector3 direction = PlayerEntity.Instance.transform.position - transform.position;

            //TODO ver se isto das layermasks assim não fode (quero que ele detete só para o player e o chão
            //e ignore os outros inimigos)
            RaycastHit2D forward =
                Physics2D.Raycast(transform.position, direction, target.sightDistance,
                    target.groundMask + target.playerMask);

            if (forward.collider == null) return false;
            //se bater no player persegue-o
            return target.playerMask.HasLayer(forward.collider.gameObject.layer);
        }

        return false;
    }

    protected void MoveInDirection(Vector3 point)
    {
        float move = point.x > 0 ? 1 : Math.Abs(point.x - 0) <= 0.01 ? 0 : -1;
        move *= target.moveSpeed * Time.deltaTime;
        if (Math.Abs(move - 0) <= 0.01)
        {
            target.rb.velocity = Vector2.zero;
            return;
        }

        // Move the character by finding the target _velocity
        Vector3 targetVelocity = new Vector2(move * 10f, target.rb.velocity.y);
        // And then smoothing it out and applying it to the character
        target.rb.velocity = Vector2.SmoothDamp(target.rb.velocity, targetVelocity, ref target.velocity,
            target.movementSmoothing);
    }

    protected void Flip()
    {
        // Switch the way the player is labelled as facing.
        target.facingRight = !target.facingRight;

        // Multiply the player's x local scale by -1.
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}