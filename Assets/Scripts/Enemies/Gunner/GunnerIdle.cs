using UnityEngine;

namespace Enemies.Gunner
{
    public class GunnerIdle : GunnerState
    {
        private float _cooldownLeft;

        private Animator _animator;
        
        public static GunnerIdle Create(Gunner target)
        {
            GunnerIdle state = GunnerState.Create<GunnerIdle>(target);
            return state;
        }

        public override void StateStart()
        {
            base.StateStart();
            _cooldownLeft = target.holdPositionTime;
            target.rb.velocity = Vector2.zero;
            _animator = GetComponent<Animator>();
            _animator.SetBool("Shooting", false);
            _animator.SetBool("Walking", false);
            _animator.SetBool("Idle", true);

        }

        public override void StateUpdate()
        {
            base.StateUpdate();
            if (target.CheckForPlayer())
            {
                SetState(GunnerChase.Create(target));
            }

            _cooldownLeft -= Time.deltaTime;
            if (_cooldownLeft <= 0)
            {
                SetState(GunnerPatrol.Create(target));
            }
        }
    }
}
