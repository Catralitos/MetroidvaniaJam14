using Hazard;
using UnityEngine;

namespace Enemies.Gunner
{
    public class GunnerStunned : GunnerState
    {
        public static GunnerStunned Create(Gunner target)
        {
            GunnerStunned state = GunnerState.Create<GunnerStunned>(target);
            return state;
        }
    
        public override void StateStart()
        {
            base.StateStart();
            Debug.Log("stunned");
            Invoke(nameof(StopStun), target.GetComponent<Kickable>().travelTime);
            //animator.SetBool("Stopped", true);
            //animator.SetBool("Patrolling", false);
            //animator.SetBool("Chasing", false);
        }

        private void StopStun()
        {
            if (target.CheckIfFlip()) target.Flip();
            //o player escapou
            if (!target.TouchingGround() || target.TouchingWall())
            {
                target.rb.velocity = Vector2.zero;
                if (target.InAttackRange())
                {
                    Debug.Log("if");
                    SetState(GunnerAttack.Create(target));
                }
                else
                {
                    target.currentPatrolAnchor = transform.position;
                    SetState(GunnerPatrol.Create(target));
                }
            }
            else
            {
                SetState(GunnerChase.Create(target));
            }
        }
    }
}
